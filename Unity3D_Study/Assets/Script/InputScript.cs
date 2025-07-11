using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator anicon_PicoChan;

    // ���������� ����� �������� �ӵ��� ����
    private float smoothSpeed = 0f;

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

        // Speed ���� Lerp�� �ε巴�� ��ȯ
        float targetSpeed = moveDirection.magnitude;
        smoothSpeed = Mathf.Lerp(smoothSpeed, targetSpeed, Time.deltaTime * 10f);
        anicon_PicoChan.SetFloat("Speed", smoothSpeed);

        // �ִϸ����� - ���� (��Ŭ�� �� Trigger)
        if (Input.GetMouseButtonDown(0))
        {
            anicon_PicoChan.SetTrigger("ATTACK");
        }
    }
}
