using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator anicon_PicoChan;

    // 내부적으로 사용할 스무스된 속도값 보관
    private float smoothSpeed = 0f;

    void Update()
    {
        // 입력
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 회전
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Speed 값을 Lerp로 부드럽게 전환
        float targetSpeed = moveDirection.magnitude;
        smoothSpeed = Mathf.Lerp(smoothSpeed, targetSpeed, Time.deltaTime * 10f);
        anicon_PicoChan.SetFloat("Speed", smoothSpeed);

        // 애니메이터 - 공격 (좌클릭 시 Trigger)
        if (Input.GetMouseButtonDown(0))
        {
            anicon_PicoChan.SetTrigger("ATTACK");
        }
    }
}
