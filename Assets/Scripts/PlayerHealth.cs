using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // �ִ� ü��
    private float currentHealth;   // ���� ü��
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // ���� ���ݿ� ���ظ� �Դ� �Լ�
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // �ǰ� �ִϸ��̼� Ʈ����
        animator.SetTrigger("Hurt"); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("�÷��̾ ����߽��ϴ�. ���� ����!");
        // ���� ���� ���� (�� ����� ��)�� ���⿡ ����
        Destroy(gameObject);
    }
}