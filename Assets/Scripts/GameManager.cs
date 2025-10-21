using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �ʿ��մϴ�.

public class GameManager : MonoBehaviour
{
    // �ٸ� ��ũ��Ʈ���� ���� ������ �� �ֵ��� �ϴ� �̱��� �ν��Ͻ�
    public static GameManager instance;

    // Inspector â���� ������ ���� ���� UI �г�
    public GameObject gameOverPanel;

    void Awake()
    {
        // �̱��� ���� ����
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� ������ ó���ϴ� �Լ�
    public void GameOver()
    {
        Debug.Log("���� ����!");
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // ���� ���� �г��� ȭ�鿡 ǥ��
        }
        Time.timeScale = 0f; // ���� �ð��� ����
    }

    // ����� ��ư�� ������ �Լ�
    public void RestartStage()
    {
        Time.timeScale = 1f; // ���� �ð��� �ٽ� ������� �ǵ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� ���� �ٽ� �ε�
    }
}