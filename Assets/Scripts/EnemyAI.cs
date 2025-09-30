using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    public float moveSpeed = 3f;           // �̵� �ӵ�
    public float sightRange = 7f;          // �÷��̾� �ν� �Ÿ�
    public float attackRange = 1.5f;       // ���� �Ÿ�
    public float attackDamage = 20f;       // ���� ���ݷ�
    public Collider2D attackCollider;       // ���� ���� ���� �ݶ��̴� (Inspector���� ����)

    // === Private Variables ===
    private Transform target;              // �߰� ��� (�÷��̾�)
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // ���� ���� �� "Player" �±׸� ���� ������Ʈ�� ã�Ƽ� target�� ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        
        if (target == null)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("speed", 0);
            return;
        }

      
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget < sightRange)
        {
            // ���� ���� ��: ���� ����
            if (distanceToTarget <= attackRange)
            {
                rb.velocity = Vector2.zero; // ������ ���� ����
                animator.SetFloat("speed", 0);

                // ���� �ִϸ��̼� ���� (�ִϸ����Ϳ� 'Attack' Trigger�� �־�� ��)
                animator.SetTrigger("Attack");
            }
            // 1-2. �ν� ���� ��: �÷��̾� �߰�
            else
            {
                ChaseTarget();
            }
        }
        else
        {
            // 2. �ν� ���� ��: ����
            rb.velocity = Vector2.zero;
            animator.SetFloat("speed", 0);
        }
    }

    void ChaseTarget()
    {
        // Ÿ�� �������� �̵�
        Vector3 direction = (target.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // �ִϸ��̼� �� ���� ��ȯ
        animator.SetFloat("speed", Mathf.Abs(direction.x));
        FlipTowardsTarget(direction.x);
    }

    void FlipTowardsTarget(float directionX)
    {
        if (directionX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (directionX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // === �ִϸ��̼� �̺�Ʈ�� ȣ��Ǵ� �Լ��� (�÷��̾�� ���ظ� ��) ===

    // �ִϸ��̼� �̺�Ʈ: ���� ���� ����
    public void StartAttackHit()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = true;

            // ���� ���� ���� ��� �ݶ��̴� ����
            Collider2D[] hitObjects = Physics2D.OverlapBoxAll(attackCollider.bounds.center, attackCollider.bounds.size, 0);

            foreach (Collider2D hit in hitObjects)
            {
                // �浹 ����� "Player" �±����� Ȯ��
                if (hit.CompareTag("Player"))
                {
                    PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(attackDamage);
                        Debug.Log("�÷��̾ ���ظ� �Ծ����ϴ�!");
                    }
                }
            }
        }
    }

    // �ִϸ��̼� �̺�Ʈ: ���� ���� ����
    public void EndAttackHit()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }
}