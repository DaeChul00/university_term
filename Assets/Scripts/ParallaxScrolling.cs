using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    // === Public Variables ===
    public float[] parallaxSpeeds; // 각 레이어의 시차 속도

    // === Private Variables ===
    private GameObject[] backgrounds; // 배경 레이어 배열
    private Vector3 cameraStartPos;  // 카메라 시작 위치
    private Vector3 playerStartPos;  // 플레이어 시작 위치

    // ===================================
    //     Unity Life Cycle Methods
    // ===================================

    void Start()
    {
        // 자식 오브젝트들을 배열에 저장
        backgrounds = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
        }

        // 카메라와 플레이어의 시작 위치 저장
        cameraStartPos = Camera.main.transform.position;
        playerStartPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        // 카메라의 이동 거리 계산
        Vector3 playerMovement = GameObject.FindGameObjectWithTag("Player").transform.position - playerStartPos;

        // 각 배경 레이어를 이동
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallaxX = playerMovement.x * parallaxSpeeds[i];
            Vector3 newPos = new Vector3(cameraStartPos.x + parallaxX, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            backgrounds[i].transform.position = newPos;
        }
    }
}