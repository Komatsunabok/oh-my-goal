using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーのカメラを動かすスクリプト
public class PlayerCam : MonoBehaviour
{
    public float sensX; // X軸方向の感度
    public float sensY; // Y軸方向の感度

    public Transform orientation;  // プレイヤーの向き
    public Transform CamHolder;   // カメラの保持場所

    float xRotation; // X軸方向の回転
    float yRotation; // Y軸方向の回転

    void Start()
    {
        //マウスポインターを真ん中に固定し見えなくする
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //マウスの入力を取得
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;

        //Y軸だけ視点に上限を設ける
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //カメラとプレイヤーの向きを動かす
        CamHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}