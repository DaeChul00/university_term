using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 필수!

public class Portal : MonoBehaviour
{
    // Inspector 창에서 이동할 씬의 이름을 적어줍니다.
    public string sceneToLoad;

    // 플레이어가 포탈 범위 안에 있는지 확인하는 변수
    private bool playerIsAtPortal = false;

    // 플레이어가 포탈 범위(트리거)에 들어왔을 때
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 들어온 오브젝트가 "Player" 태그인지 확인
        if (other.CompareTag("Player"))
        {
            playerIsAtPortal = true;
            Debug.Log("플레이어가 포탈에 도착했습니다.");
        }
    }

    // 플레이어가 포탈 범위(트리거)에서 나갔을 때
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsAtPortal = false;
            Debug.Log("플레이어가 포탈을 떠났습니다.");
        }
    }

    // 매 프레임마다 입력을 확인
    void Update()
    {
        // 플레이어가 포탈에 있고(true) "윗 방향키"를 눌렀다면
        if (playerIsAtPortal && Input.GetKeyDown(KeyCode.UpArrow))
        {
            // sceneToLoad 변수에 적힌 이름의 씬으로 이동합니다.
            Debug.Log(sceneToLoad + " 씬으로 이동합니다!");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}