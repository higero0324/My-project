using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float timeSpeedGain = 0.05f;
    public float hitSpeedGain = 0.2f;
    public float maxSpeed = 20f;

    public float currentSpeed { get; private set; }

    private Vector2 moveDirection;
    private float startTime;
    private int hitCount = 0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        startTime = Time.time;

        // ランダムに左右スタート
        int dir = Random.Range(0, 2) * 2 - 1;
        moveDirection = new Vector2(dir, 0).normalized;

        currentSpeed = baseSpeed;
        rb.linearVelocity = moveDirection * currentSpeed;
    }

    void FixedUpdate()
    {
        currentSpeed = Mathf.Min(
            baseSpeed + (Time.time - startTime) * timeSpeedGain + hitCount * hitSpeedGain,
            maxSpeed
        );
        rb.linearVelocity = moveDirection * currentSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 normal;

        if (other.CompareTag("WallTop"))
        {
            normal = Vector2.down;
        }
        else if (other.CompareTag("WallBottom"))
        {
            normal = Vector2.up;
        }
        else if (other.CompareTag("WallLeft"))
        {
            normal = Vector2.right;
        }
        else if (other.CompareTag("WallRight"))
        {
            normal = Vector2.left;
        }
        else
        {
            // プレイヤーなどとの衝突 → 近似的な法線
            normal = (transform.position - other.transform.position).normalized;
            hitCount++;
        }

        if (other.CompareTag("CastleR"))
        {
            CastleController castle = other.GetComponent<CastleController>();
            if (castle != null)
            {
                castle.TakeDamage(1);
                Debug.Log("左の城にダメージ！");
            }
        }
        else if (other.CompareTag("CastleB"))
        {
            CastleController castle = other.GetComponent<CastleController>();
            if (castle != null)
            {
                castle.TakeDamage(1);
                Debug.Log("右の城にダメージ！");
            }
        }

        // 反射方向を更新（そのまま速度にも反映される）
        moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
        rb.linearVelocity = moveDirection * currentSpeed;
    }
}
