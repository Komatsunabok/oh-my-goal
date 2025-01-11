using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;  // 移動速度
    public float changeDirectionTime = 2f;  // 方向変更の間隔

    private float timeSinceLastDirectionChange;
    private Vector3 moveDirection;

    void Start()
    {
        SetRandomDirection();
        timeSinceLastDirectionChange = 0f;
    }

    void Update()
    {
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange >= changeDirectionTime)
        {
            SetRandomDirection();
            timeSinceLastDirectionChange = 0f;
        }

        // 移動処理（顔が向いている方向に進む＆回転）
        MoveEnemy();
    }

    // 敵キャラを顔の向きに進ませるとともに、その方向に回転させる
    void MoveEnemy()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // 回転処理（進行方向に基づいてオブジェクトが回転する）
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
    }

    // ランダムな進行方向を設定する（顔の向きに基づく）
    void SetRandomDirection()
    {
        // 現在の顔の向き（正面）を取得
        Vector3 forwardDirection = transform.forward;

        // ランダムな角度を生成
        float randomAngle = Random.Range(-90f, 90f);  // -45度から45度の範囲でランダムな角度
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);  // Y軸回りの回転を設定
        moveDirection = rotation * forwardDirection;  // 顔の向きに基づいて新しい進行方向を決定

        // ベクトルを正規化して進行方向を設定
        moveDirection = moveDirection.normalized;
    }
}
