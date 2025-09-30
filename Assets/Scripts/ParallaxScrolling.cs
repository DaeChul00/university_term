using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    // === Public Variables ===
    public float[] parallaxSpeeds; // �� ���̾��� ���� �ӵ�

    // === Private Variables ===
    private GameObject[] backgrounds; // ��� ���̾� �迭
    private Vector3 cameraStartPos;  // ī�޶� ���� ��ġ
    private Vector3 playerStartPos;  // �÷��̾� ���� ��ġ

    // ===================================
    //     Unity Life Cycle Methods
    // ===================================

    void Start()
    {
        // �ڽ� ������Ʈ���� �迭�� ����
        backgrounds = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
        }

        // ī�޶�� �÷��̾��� ���� ��ġ ����
        cameraStartPos = Camera.main.transform.position;
        playerStartPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        // ī�޶��� �̵� �Ÿ� ���
        Vector3 playerMovement = GameObject.FindGameObjectWithTag("Player").transform.position - playerStartPos;

        // �� ��� ���̾ �̵�
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallaxX = playerMovement.x * parallaxSpeeds[i];
            Vector3 newPos = new Vector3(cameraStartPos.x + parallaxX, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            backgrounds[i].transform.position = newPos;
        }
    }
}