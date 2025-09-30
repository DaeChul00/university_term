using UnityEngine;
using System.Collections; // �ڷ�ƾ ����� ���� �����մϴ�.

public class PlayerController : MonoBehaviour
{
    // === Public Variables (Inspector���� ����) ===
    public float moveSpeed = 4f;     // �̵� �ӵ�
    public float jumpForce = 5f;     // ���� ��
    public Collider2D attackCollider; // ���� ������ ���� �ݶ��̴�
    public BoxCollider2D mapBoundary; // �� ��� �ݶ��̴�

    // === Private Variables ===
    private Rigidbody2D rb;          // Rigidbody2D ������Ʈ
    private Animator animator;       // Animator ������Ʈ
    private bool isGrounded;         // ���� ��Ҵ��� Ȯ�� �÷���
    private bool isAttacking = false; // ���� ������ Ȯ���ϴ� �÷��� (���� ���� ���� �ٽ�)

    // ===================================
    //     Unity Life Cycle Methods
    // ===================================

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // ���� �� ���� �ݶ��̴� ��Ȱ��ȭ
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    void Update()
    {
        HandleMovement();

        // === ���� ===
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Animator�� 'IsJumping' �Ķ���� ������Ʈ
        animator.SetBool("IsJumping", !isGrounded);

        // === ���� ===
        // isAttacking�� false�� ���� ���� ���
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true; // ���� ���۰� ���ÿ� true�� �����Ͽ� �߰� �Է� ����
        }

        // === �� ��� ������ �÷��̾� ��ġ ���� ===
        if (mapBoundary != null)
        {
            float minX = mapBoundary.bounds.min.x;
            float maxX = mapBoundary.bounds.max.x;

            Vector3 currentPos = transform.position;
            currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
            transform.position = currentPos;
        }
    }

    // === ĳ���� �̵� �� ���� ��ȯ�� ó���ϴ� �Լ� ===
    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        animator.SetFloat("speed", Mathf.Abs(horizontalInput));

        // ���� ��ȯ (Scale�� �̿��� ��������Ʈ ����)
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // === �ٴ� üũ ===
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // === �ִϸ��̼� �̺�Ʈ�� ȣ��Ǵ� �Լ��� (���� ����) ===

    // ���� �ִϸ��̼� ���� �����ӿ��� ȣ�� (���� ���� ����)
    public void StartAttack()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = true; // �ݶ��̴� Ȱ��ȭ

            // ������ ����
            Collider2D[] hitObjects = Physics2D.OverlapBoxAll(attackCollider.bounds.center, attackCollider.bounds.size, 0);

            foreach (Collider2D hit in hitObjects)
            {
                if (hit.CompareTag("Enemy"))
                {
                    EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(20f);
                        Debug.Log("���� ���ظ� �Ծ����ϴ�!");
                    }
                }
            }
        }
    }

    // ���� �ִϸ��̼� ���� �����ӿ��� ȣ�� (���� ���� ����)
    public void EndAttack()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = false; // �ݶ��̴� ��Ȱ��ȭ
        }

        // isAttacking�� false�� �����Ͽ� ���� ������ ��� (�ٽ�)
        isAttacking = false;
        Debug.Log("EndAttack ȣ��. ���� ���� ����.");
    }
}