using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("프리팹 (Prefabs)")]
    public GameObject playerPrefab; // 플레이어 원본(프리팹)

    [Header("스폰 지점 (Spawn Points)")]
    public Transform playerSpawnPoint;    // 플레이어가 생성될 위치

    [Header("씬(Scene) 설정")]
    public BoxCollider2D mapBoundary; // 맵 경계를 연결할 슬롯
    public Camera mainCamera;         // 메인 카메라를 연결할 슬롯

    void Start()
    {
        SpawnPlayer();
    }

    // 플레이어를 스폰하는 함수
    // LevelManager.cs의 SpawnPlayer 함수
    void SpawnPlayer()
    {
        // 플레이어 프리팹과 스폰 지점이 모두 설정되었는지 확인
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            // === 1. 플레이어 스폰 ===
            GameObject playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
            Debug.Log("플레이어 스폰 완료!");

            // === 2. 카메라 설정 ===
            if (mainCamera != null && mapBoundary != null)
            {
                // 메인 카메라에 붙어있는 CameraFollow 스크립트를 가져옵니다.
                CameraFollow cameraScript = mainCamera.GetComponent<CameraFollow>();

                if (cameraScript != null)
                {
                    // 2-1. 카메라에게 스폰된 플레이어를 'target'으로 알려주기
                    cameraScript.target = playerInstance.transform;

                    // 2-2. 카메라에게 맵 경계('MapBounds')를 알려주기
                    cameraScript.mapBoundary = this.mapBoundary;
                }
            }
        }
        else
        {
            Debug.LogError("플레이어 프리팹 또는 스폰 지점이 LevelManager에 연결되지 않았습니다!");
        }
    }

}