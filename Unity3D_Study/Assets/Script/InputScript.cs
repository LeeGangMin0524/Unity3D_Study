using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator anicon_PicoChan; // 애니메이터 변수 선언

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

        // 애니메이터 - 걷기
        bool isWalk = 0 < moveDirection.magnitude;
        anicon_PicoChan.SetBool("ISWALK", isWalk); // Setbool(파라미터의 이름, 설정 값)

        // 애니메이터 - 공격 (좌클릭)
        if (Input.GetMouseButtonDown(0))
        {
            anicon_PicoChan.SetTrigger("ATTACK");
        }
    }
}
