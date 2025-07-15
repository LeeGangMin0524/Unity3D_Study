using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("캐릭터")]
    [SerializeField] Rigidbody rigid;
    [SerializeField] Transform character;
    [SerializeField] Animator anicon;
    [SerializeField] float moveSpeed;

    Vector2 moveInput;

    [Header("점프")]
    public float jumpPower;
    public int MaxJumpCount = 1;
    int nowJumpCount;
    bool isJump;

    [Header("공격")]
    [SerializeField] int attackRange = 3;
    [SerializeField] int attackAngle = 120;

    void Awake()
    {
        nowJumpCount = MaxJumpCount;
        isJump = false;
    }

    void Update()
    {
        Move();
        Jump();
        Attack();
    }

    void Move()
    {
        Vector2 rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.x = Mathf.MoveTowards(moveInput.x, rawInput.x, Time.deltaTime * 10);
        moveInput.y = Mathf.MoveTowards(moveInput.y, rawInput.y, Time.deltaTime * 10);
        float moveValue = moveInput.magnitude;

        if (moveValue != 0)
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

            // 이동
            rigid.MovePosition(character.position + (moveDir * Time.deltaTime * moveSpeed));

            // 회전
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            character.rotation = Quaternion.Slerp(character.rotation, targetRotation, Time.deltaTime * 10f);
        }

        if (!isJump)
        {
            anicon.SetBool("ISWALK", moveValue != 0);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && nowJumpCount > 0)
        {
            rigid.velocity = Vector3.up * jumpPower;
            nowJumpCount--;
            isJump = true;

            anicon.SetTrigger("JUMP");
            anicon.SetBool("JUMPEND", false);
        }

        if (rigid.velocity.y <= 0 &&
            Physics.Raycast(character.position + Vector3.up * 0.1f, Vector3.down, 0.2f, LayerMask.GetMask("Ground")))
        {
            nowJumpCount = MaxJumpCount;
            isJump = false;

            anicon.SetBool("JUMPEND", true);
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))  // 좌클릭 공격
        {
            anicon.SetTrigger("ATTACK");
        }
    }

    public void AttackMonster()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in hitColliders)
        {
            Monster monster = collider.GetComponent<Monster>();
            if (monster != null)
            {
                Vector3 directionToTarget = (monster.transform.position - transform.position).normalized;
                float dot = Vector3.Dot(transform.forward, directionToTarget);
                float angleThreshold = Mathf.Cos(attackAngle * 0.5f * Mathf.Deg2Rad);

                if (dot >= angleThreshold)
                {
                    monster.Damaged();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Vector3 forward = transform.forward;
        Quaternion leftRotation = Quaternion.Euler(0, -attackAngle / 2, 0);
        Quaternion rightRotation = Quaternion.Euler(0, attackAngle / 2, 0);

        Vector3 leftDirection = leftRotation * forward;
        Vector3 rightDirection = rightRotation * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftDirection * attackRange);
        Gizmos.DrawLine(transform.position, transform.position + rightDirection * attackRange);
    }
}
