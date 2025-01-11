using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;  // �ړ����x
    public float changeDirectionTime = 2f;  // �����ύX�̊Ԋu

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

        // �ړ������i�炪�����Ă�������ɐi�ށ���]�j
        MoveEnemy();
    }

    // �G�L��������̌����ɐi�܂���ƂƂ��ɁA���̕����ɉ�]������
    void MoveEnemy()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // ��]�����i�i�s�����Ɋ�Â��ăI�u�W�F�N�g����]����j
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
    }

    // �����_���Ȑi�s������ݒ肷��i��̌����Ɋ�Â��j
    void SetRandomDirection()
    {
        // ���݂̊�̌����i���ʁj���擾
        Vector3 forwardDirection = transform.forward;

        // �����_���Ȋp�x�𐶐�
        float randomAngle = Random.Range(-90f, 90f);  // -45�x����45�x�͈̔͂Ń����_���Ȋp�x
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);  // Y�����̉�]��ݒ�
        moveDirection = rotation * forwardDirection;  // ��̌����Ɋ�Â��ĐV�����i�s����������

        // �x�N�g���𐳋K�����Đi�s������ݒ�
        moveDirection = moveDirection.normalized;
    }
}
