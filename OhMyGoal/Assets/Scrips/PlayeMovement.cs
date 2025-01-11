using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // プレイヤーの移動速度
    public float groundDrag; // 減速値
    public Transform orientation; // プレイヤーの向き（カメラの向きに合わせて移動）

    private float horizontalInput; // 水平入力（A/Dまたは左右キー）
    private float verticalInput; // 垂直入力（W/Sまたは上下キー）
    private Vector3 moveDirection; // 移動方向
    private Rigidbody rb; // プレイヤーのRigidbody

    void Start()
    {
        // Rigidbodyの取得と回転の固定
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 回転を固定してプレイヤーが倒れないようにする
    }

    void Update()
    {
        // 入力の処理
        ProcessInput();

        // 減速値を適用
        rb.drag = groundDrag;

        // スピード制御
        SpeedControl();
    }

    void FixedUpdate()
    {
        // 移動処理
        MovePlayer();
    }

    private void ProcessInput()
    {
        // 水平入力と垂直入力を取得
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // カメラの向きに基づいた移動方向を計算
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // 入力に基づいて移動する
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    private void SpeedControl()
    {
        // 水平方向の速度を計算（y軸の速度は無視）
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        // 速度が設定された移動速度を超えないように制御
        if (flatVel.magnitude > moveSpeed)
        {
            // 速度を制限
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
