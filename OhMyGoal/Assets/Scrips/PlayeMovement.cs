using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // �v���C���[�̈ړ����x
    public float groundDrag; // �����l
    public Transform orientation; // �v���C���[�̌����i�J�����̌����ɍ��킹�Ĉړ��j

    private float horizontalInput; // �������́iA/D�܂��͍��E�L�[�j
    private float verticalInput; // �������́iW/S�܂��͏㉺�L�[�j
    private Vector3 moveDirection; // �ړ�����
    private Rigidbody rb; // �v���C���[��Rigidbody

    void Start()
    {
        // Rigidbody�̎擾�Ɖ�]�̌Œ�
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ��]���Œ肵�ăv���C���[���|��Ȃ��悤�ɂ���
    }

    void Update()
    {
        // ���͂̏���
        ProcessInput();

        // �����l��K�p
        rb.drag = groundDrag;

        // �X�s�[�h����
        SpeedControl();
    }

    void FixedUpdate()
    {
        // �ړ�����
        MovePlayer();
    }

    private void ProcessInput()
    {
        // �������͂Ɛ������͂��擾
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // �J�����̌����Ɋ�Â����ړ��������v�Z
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // ���͂Ɋ�Â��Ĉړ�����
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    private void SpeedControl()
    {
        // ���������̑��x���v�Z�iy���̑��x�͖����j
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        // ���x���ݒ肳�ꂽ�ړ����x�𒴂��Ȃ��悤�ɐ���
        if (flatVel.magnitude > moveSpeed)
        {
            // ���x�𐧌�
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
