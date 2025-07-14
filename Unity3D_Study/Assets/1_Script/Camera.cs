using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // 플레이어
    public Vector3 offset = new Vector3(0, 5, -7); // 카메라 위치 오프셋
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(target); // 타겟 바라보게
    }
}
