using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    public float moveSpeed = 3f;           // 이동 속도
    public float sightRange = 7f;          // 플레이어 인식 거리
    public float attackRange = 1.5f;       // 공격 거리
    public float attackDamage = 20f;       // 적의 공격력
    public Collider2D attackCollider;       // 적의 공격 판정 콜라이더 (Inspector에서 연결)

    // === Private Variables ===
    private Transform target;              // 추격 대상 (플레이어)
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 게임 시작 시 "Player" 태그를 가진 오브젝트를 찾아서 target에 연결
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
            // 공격 범위 내: 공격 실행
            if (distanceToTarget <= attackRange)
            {
                rb.velocity = Vector2.zero; // 공격할 때는 멈춤
                animator.SetFloat("speed", 0);

                // 공격 애니메이션 실행 (애니메이터에 'Attack' Trigger가 있어야 함)
                animator.SetTrigger("Attack");
            }
            // 1-2. 인식 범위 내: 플레이어 추격
            else
            {
                ChaseTarget();
            }
        }
        else
        {
            // 2. 인식 범위 밖: 멈춤
            rb.velocity = Vector2.zero;
            animator.SetFloat("speed", 0);
        }
    }

    void ChaseTarget()
    {
        // 타겟 방향으로 이동
        Vector3 direction = (target.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // 애니메이션 및 방향 전환
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

    // === 애니메이션 이벤트로 호출되는 함수들 (플레이어에게 피해를 줌) ===

    // 애니메이션 이벤트: 공격 판정 시작
    public void StartAttackHit()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = true;

            // 공격 범위 내의 모든 콜라이더 감지
            Collider2D[] hitObjects = Physics2D.OverlapBoxAll(attackCollider.bounds.center, attackCollider.bounds.size, 0);

            foreach (Collider2D hit in hitObjects)
            {
                // 충돌 대상이 "Player" 태그인지 확인
                if (hit.CompareTag("Player"))
                {
                    PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(attackDamage);
                        Debug.Log("플레이어가 피해를 입었습니다!");
                    }
                }
            }
        }
    }

    // 애니메이션 이벤트: 공격 판정 종료
    public void EndAttackHit()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }
}