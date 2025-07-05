using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float baseSpeed = 15f;
    public float currentSpeed; // 外部参照用（PlayerControllerなどから使う）

    public int hitCount = 0; // 衝突回数をカウント

    public float smashWeight = 10f;

    public float smashCount = 0f;

    public float hitWeight = 0.2f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    public float ballSpeed;

    public GameManager GameManager; // ゲームマネージャーの参照


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        int dir = Random.Range(0, 2) * 2 - 1;
        moveDirection = new Vector2(dir, 0).normalized;

        currentSpeed = baseSpeed;
        ballSpeed = currentSpeed; // ✅ ここで初期化
        rb.linearVelocity = moveDirection * currentSpeed;
        smashWeight = 10f;
    }

    void FixedUpdate()
    {
        if (currentSpeed + hitWeight <= 25f) // 最大速度制限
        {
            currentSpeed = baseSpeed + hitCount * hitWeight;
            ballSpeed = currentSpeed + smashCount * smashWeight; // 衝突回数に応じて速度を調整

            // 衝突回数に応じて速度を調整
            // 等速直線運動（速度補正）
        }
        else
        {
            currentSpeed = 25f; // 最大速度を超えないように制限
            ballSpeed = currentSpeed + smashCount * smashWeight; // 最大速度に応じて調整
        }

        rb.linearVelocity = moveDirection * ballSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("smashCount: " + smashCount + ", speed: " + ballSpeed + ", weight: " + smashWeight);

        if (collision.contacts.Length > 0 && (collision.collider.CompareTag("PlayerR") || collision.collider.CompareTag("PlayerB")))
        {

            Vector2 toPlayer = ((Vector2)collision.collider.transform.position - rb.position).normalized;
            moveDirection = (-toPlayer).normalized;
            rb.linearVelocity = moveDirection * ballSpeed;
            smashCount = 0; // スマッシュカウントリセット
            hitCount++;
        }
        else if (collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
            rb.linearVelocity = moveDirection * ballSpeed;
        }

        if (collision.collider.CompareTag("CastleR") || collision.collider.CompareTag("CastleB"))
        {
            CastleController castle = collision.collider.GetComponent<CastleController>();
            if (castle != null)
            {
                castle.TakeDamage(1);
                Debug.Log((collision.collider.CompareTag("CastleR") ? "左" : "右") + "の城にダメージ！");
            }
            smashCount = 0; // スマッシュカウントリセット
        }
        if (collision.collider.CompareTag("PlayerR"))
        {
            GameManager.mp1 ++; // PlayerRのMPを増加
        }
        if (collision.collider.CompareTag("PlayerB"))
        {
            GameManager.mp2 ++; // PlayerBのMPを増加
        }
    }

}
