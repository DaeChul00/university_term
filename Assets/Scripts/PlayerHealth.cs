using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    
    public float maxHealth = 100f;
    public float parryWindow = 0.2f; // �и� ������ ª�� �ð�

  
    private float currentHealth;
    private Animator animator;
    private bool isParrying = false;  // ���� �и� �õ� ������
    private bool isInvincible = false; // �ǰ� �� ���� ��������

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // ���� ���ݿ� ���ظ� �Դ� �Լ� (�и� ���� �߰�)
    public void TakeDamage(float damage)
    {
        // 1. ���� ���� üũ
        if (isInvincible) return;

        // 2. �и� ���� üũ (�ٽ� ����)
        if (isParrying)
        {
            // --- �и� ���� ---
            Debug.Log("�и� ����! �� ���� ȿ�� �߻�!");
            

            // �и� ���� �ÿ��� ª�� ���� �ð� �ο� (���� ����)
            StartCoroutine(BecomeTemporarilyInvincible(0.5f));

            return; // �и� ���� �� ���ظ� ���� �ʰ� �Լ� ����
        }

        // --- �и� ���� / �Ϲ� �ǰ� ---
        currentHealth -= damage;
        Debug.Log("�Ϲ� �ǰ�! ���� ü��: " + currentHealth);

        // �ǰ� �ִϸ��̼� �� ���� �ڷ�ƾ ����
        animator.SetTrigger("Hurt");
        StartCoroutine(BecomeTemporarilyInvincible(1.0f)); // 1�ʰ� ���� ���� �ο�

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("�÷��̾ ����߽��ϴ�. ���� ����!");
        
        Destroy(gameObject);
    }

    // ���� ���¸� �����ϴ� �ڷ�ƾ
    private IEnumerator BecomeTemporarilyInvincible(float duration)
    {
        isInvincible = true;

        yield return new WaitForSeconds(duration);

        isInvincible = false;
    }

    // PlayerController���� ȣ��: �и� �Է��� ������ ��
    public void StartParryAttempt()
    {
        // �и� �ִϸ��̼� Ʈ���� (Animator�� "Parry" Trigger �ʿ�)
        animator.SetTrigger("Parry");
        isParrying = true;

        // �и� ���� �ð�(parryWindow)�� ������ isParrying�� false�� ����
        StartCoroutine(ResetParryState());
    }

    private IEnumerator ResetParryState()
    {
        // ������ ª�� �ð� ���ȸ� �и��� �����ϵ��� ���
        yield return new WaitForSeconds(parryWindow);

        isParrying = false;
    }
}