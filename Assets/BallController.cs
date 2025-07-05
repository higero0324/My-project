using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float baseSpeed = 15f;
    public float currentSpeed; // 外部参照用（PlayerControllerなどから使う）

    public int hitCount = 0; // 衝突回数をカウント

    public float hitWeight = 0.2f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // ランダムに左右へ発射
        int dir = Random.Range(0, 2) * 2 - 1;
        moveDirection = new Vector2(dir, 0).normalized;

        currentSpeed = baseSpeed;
        rb.linearVelocity = moveDirection * currentSpeed;
    }

    void FixedUpdate()
    {
        if (currentSpeed + hitWeight <= 25f) // 最大速度制限
        {
            currentSpeed = baseSpeed + hitCount * hitWeight; // 衝突回数に応じて速度を調整
            // 等速直線運動（速度補正）
        }
        else
        {
            currentSpeed = 25f; // 最大速度を超えないように制限
        }
        
        rb.linearVelocity = moveDirection * currentSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突から法線を取得し、反射方向を決定
        if (collision.contacts.Length > 0 &&collision.collider.CompareTag("Player"))
            {
                Vector2 toPlayer = ((Vector2)collision.collider.transform.position - rb.position).normalized;
                // ✅ その逆方向に飛ばす（はじく）
                moveDirection = (-toPlayer).normalized;
                // ✅ 等速で移動開始
                rb.linearVelocity = moveDirection * currentSpeed;
                hitCount++;
            }
        else if (collision.contacts.Length > 0)
            {
                Vector2 normal = collision.contacts[0].normal;
                moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
                rb.linearVelocity = moveDirection * currentSpeed;
            }

        // 城に当たったときの処理
        if (collision.collider.CompareTag("CastleR") || collision.collider.CompareTag("CastleB"))
        {
            CastleController castle = collision.collider.GetComponent<CastleController>();
            if (castle != null)
            {
                castle.TakeDamage(1);
                Debug.Log((collision.collider.CompareTag("CastleR") ? "左" : "右") + "の城にダメージ！");
            }
        }
    }
}
