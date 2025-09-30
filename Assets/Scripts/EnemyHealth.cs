using UnityEngine;
using System.Collections; // ���� �� using ���� �ڷ�ƾ�� �ʿ� �����ϴ�.

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("���� �׾����ϴ�.");
        Destroy(gameObject);
    }
}