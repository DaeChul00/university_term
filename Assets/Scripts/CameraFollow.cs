using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // ī�޶� ���� ��� (�÷��̾�)
    private Transform target; // public���� private���� ����

    // ī�޶�� ��� ���� �Ÿ� (Z��)
    public float zOffset = -10f;

    // �� ��� �ݶ��̴��� ������ ����
    public BoxCollider2D mapBoundary;

    // Start() �Լ� �߰�
    void Start()
    {
        // "Player" �±׸� ���� ������Ʈ�� ã�Ƽ� target�� ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void LateUpdate()
    {
        // target�� null�� �ƴ� ���� ����
        if (target != null && mapBoundary != null)
        {
            // ����� X, Y ��ġ�� �����ͼ� ī�޶��� ��ġ�� ������Ʈ
            Vector3 newPosition = new Vector3(target.position.x, target.position.y, zOffset);

            // === �� ��� ������ ī�޶� ��ġ ���� ===
            float camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
            float camHalfHeight = Camera.main.orthographicSize;

            float minX = mapBoundary.bounds.min.x + camHalfWidth;
            float maxX = mapBoundary.bounds.max.x - camHalfWidth;
            float minY = mapBoundary.bounds.min.y + camHalfHeight;
            float maxY = mapBoundary.bounds.max.y - camHalfHeight;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;
        }
    }
}