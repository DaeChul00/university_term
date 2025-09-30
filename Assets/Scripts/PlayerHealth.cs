using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // 최대 체력
    private float currentHealth;   // 현재 체력
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // 적의 공격에 피해를 입는 함수
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // 피격 애니메이션 트리거
        animator.SetTrigger("Hurt"); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("플레이어가 사망했습니다. 게임 오버!");
        // 게임 오버 로직 (씬 재시작 등)을 여기에 구현
        Destroy(gameObject);
    }
}