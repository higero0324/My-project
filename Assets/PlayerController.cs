using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BallController ball;

    public float baseSpeed = 10f;
    public float addSpeed = 0.7f;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    public enum Side { Red, Blue }
    public Side playerSide;

    public float moveSpeed;

    private Vector3 moveInput;

    void Update()
    {
        float moveX = Input.GetAxisRaw(horizontalAxis);
        float moveY = Input.GetAxisRaw(verticalAxis);
        moveInput = new Vector3(moveX, moveY, 0).normalized;
        moveSpeed = baseSpeed + ball.currentSpeed * addSpeed;

        Move();
        ClampPosition();
    }

    void Move()
    {
        transform.position += moveInput * moveSpeed * Time.deltaTime;
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;

        // 陣地分け：-9〜-3がRed、3〜9がBlue、-3〜3は不可侵
        float minY = -4.5f, maxY = 4.5f;

        if (playerSide == Side.Red)
    {
        pos.x = Mathf.Clamp(pos.x, -11f, -3f);
    }
    else
    {
        pos.x = Mathf.Clamp(pos.x, 3f, 11f);
    }


        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
