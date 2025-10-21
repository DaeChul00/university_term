using UnityEngine;
using UnityEngine.UI;
using System.Collections; // ���� �� using ���� �ڷ�ƾ�� �ʿ� �����ϴ�.

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public Slider healthSlider; // ü�� �� Slider�� ������ ����

    private float currentHealth;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        // ü�� �� �ʱ� ����
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // ü�� �� ������Ʈ
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

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