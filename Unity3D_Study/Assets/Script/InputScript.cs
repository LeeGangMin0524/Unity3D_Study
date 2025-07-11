using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator anicon_PicoChan; // �ִϸ����� ���� ����

    void Update()
    {
        // �Է�
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // �̵�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ȸ��
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // �ִϸ����� - �ȱ�
        bool isWalk = 0 < moveDirection.magnitude;
        anicon_PicoChan.SetBool("ISWALK", isWalk); // Setbool(�Ķ������ �̸�, ���� ��)

        // �ִϸ����� - ���� (��Ŭ��)
        if (Input.GetMouseButtonDown(0))
        {
            anicon_PicoChan.SetTrigger("ATTACK");
        }
    }
}
