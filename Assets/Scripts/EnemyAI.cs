using UnityEngine;
using System.Collections; // ��Ÿ�� ������ ���� �ڷ�ƾ �ʿ�

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;           // �̵� �ӵ�
    public float sightRange = 8f;          // �÷��̾� �ν� �Ÿ�
    public float attackRange = 1.5f;       // ������ ������ �Ÿ�
    public float attackDamage = 10f;       // ���� ���ݷ�
    public float attackCooldown = 2f;      // ���� ���ݱ����� ��Ÿ��
    public Collider2D attackCollider;       // ���� ���� ���� �ݶ��̴� (Inspector���� ����)

    private Transform player;              // �߰� ��� (�÷��̾�)
    private Rigidbody2D rb;
    private Animator animator;
    private bool canAttack = true;         // ���� ���� ������ �������� Ȯ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // "Player" �±׸� ���� ������Ʈ�� ã�Ƽ� target�� ����
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("������ 'Player' �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�! �� AI�� ����ϴ�.");
        }
    }

    void Update()
    {
        // 1. target�� ������ �ƹ��͵� ���� ����
        if (player == null)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("speed", 0);
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < sightRange)
        {
            // 1-1. ���� ���� ��: ���� ����
            if (distanceToPlayer <= attackRange)
            {
                rb.velocity = Vector2.zero;
                animator.SetFloat("speed", 0);

                // ��Ÿ���� ������ ���� ����
                if (canAttack)
                {
                    animator.SetTrigger("Attack"); // 'Attack' Trigger ����
                    canAttack = false;
                    StartCoroutine(AttackCooldownRoutine()); // ��Ÿ�� �ڷ�ƾ ����
                }
            }
            // 1-2. �ν� ���� �� �÷��̾� �߰�
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            // 2. �ν� ���� ��: ����
            rb.velocity = Vector2.zero;
            animator.SetFloat("speed", 0);
        }
    }

    void ChasePlayer()
    {
        // Ÿ�� �������� �̵�
        Vector3 direction = (player.position - transform.position).normalized;
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

    // === �ִϸ��̼� �̺�Ʈ�� ȣ��Ǵ� �Լ� (�÷��̾�� ���ظ� ��) ===

    // �� ���� �ִϸ��̼��� '���� ����'�� ȣ��˴ϴ�.
    public void DealDamageToPlayer()
    {
        Debug.Log("DealDamageToPlayer �Լ��� ȣ��Ǿ����ϴ�.");

        if (attackCollider == null)
        {
            Debug.LogError("Attack Collider�� Inspector�� ������� �ʾҽ��ϴ�!");
            return;
        }

        // ���� ���� ���� �ִ� ��� �ݶ��̴��� �����մϴ�.
        Collider2D[] hitObjects = Physics2D.OverlapBoxAll(attackCollider.bounds.center, attackCollider.bounds.size, 0);
        Debug.Log("���� ���� ������ " + hitObjects.Length + "���� ������Ʈ�� �����߽��ϴ�.");

        // ������ ��� ������Ʈ�� ��ȸ�մϴ�.
        foreach (Collider2D hit in hitObjects)
        {
            // ������ ������Ʈ�� �̸��� �±׸� �ֿܼ� ����մϴ�.
            Debug.Log("������ ������Ʈ: " + hit.name + ", �±�: " + hit.tag);

            // ������ ������Ʈ�� �±װ� "Player"���� Ȯ���մϴ�.
            if (hit.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                    Debug.Log("����: �÷��̾�� �������� �����߽��ϴ�!");
                }
                else
                {
                    Debug.LogError("����: 'Player' �±׸� ���� ������Ʈ���� PlayerHealth ��ũ��Ʈ�� ã�� �� �����ϴ�.");
                }
            }
        }
    }

    // === ��Ÿ�� ���� �ڷ�ƾ ===
    private IEnumerator AttackCooldownRoutine()
    {
        // 1. Attack �ִϸ��̼��� ������ ���� ������ ��ٸ��ϴ�
        // �� �ڵ�� �ִϸ��̼��� ���� �� ��Ÿ���� �����ϵ��� ����
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // 2. ������ ��Ÿ�� �ð���ŭ ���
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true; // ��Ÿ�� ����, �ٽ� ���� ����
    }
}