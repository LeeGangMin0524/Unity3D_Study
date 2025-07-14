using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] Transform character; // 발 근처에 빈 오브젝트 추천
    [SerializeField] Animator anicon;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] int MaxJumpCount = 1;

    Vector2 moveInput;
    int nowJumpCount;
    bool isGrounded = false;

    void Start()
    {
        nowJumpCount = MaxJumpCount;
    }

    void Update()
    {
        Move();
        Attack();
        Jump();
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    void Move()
    {
        Vector2 rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.x = Mathf.MoveTowards(moveInput.x, rawInput.x, Time.deltaTime * 10);
        moveInput.y = Mathf.MoveTowards(moveInput.y, rawInput.y, Time.deltaTime * 10);
        float moveValue = moveInput.magnitude;

        if (moveValue != 0)
        {
            Vector3 inputForward = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
            rigid.MovePosition(transform.position + (inputForward * Time.deltaTime * moveSpeed));

            if (moveInput != Vector2.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(inputForward);
                character.rotation = Quaternion.Slerp(character.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

        anicon.SetBool("ISWALK", moveValue != 0);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anicon.SetTrigger("ATTACK");
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && nowJumpCount > 0)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z); // Y축 초기화
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            nowJumpCount--;

            anicon.SetTrigger("JUMP");            // 점프 애니메이션
            anicon.SetBool("JUMPEND", false);     // 아직 공중
        }
    }

    void GroundCheck()
    {
        Vector3 origin = character.position + Vector3.up * 0.1f;
        float rayLength = 0.3f;

        Debug.DrawRay(origin, Vector3.down * rayLength, Color.green);

        bool wasGrounded = isGrounded;
        isGrounded = Physics.Raycast(origin, Vector3.down, rayLength, LayerMask.GetMask("Ground"));

        if (!wasGrounded && isGrounded)
        {
            nowJumpCount = MaxJumpCount;
            anicon.SetBool("JUMPEND", true); // 착지 → 애니메이션 Idle 전환 유도
        }
    }
}
