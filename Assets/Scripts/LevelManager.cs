using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("프리팹 (Prefabs)")]
    public GameObject playerPrefab; // 플레이어 원본(프리팹)

    [Header("스폰 지점 (Spawn Points)")]
    public Transform playerSpawnPoint;    // 플레이어가 생성될 위치

    void Start()
    {
        SpawnPlayer();
    }

    // 플레이어를 스폰하는 함수
    void SpawnPlayer()
    {
        // 플레이어 프리팹과 스폰 지점이 모두 설정되었는지 확인
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            // playerSpawnPoint의 위치에 playerPrefab을 생성(Instantiate)
            Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
            Debug.Log("플레이어 스폰 완료!");
        }
        else
        {
            Debug.LogError("플레이어 프리팹 또는 스폰 지점이 LevelManager에 연결되지 않았습니다!");
        }
    }

}