using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float baseSpeed = 15f;
    public float currentSpeed; // 外部参照用（PlayerControllerなどから使う）

    public float hitCount = 0; // 衝突回数をカウント

    public float smashWeight = 10f;

    public float smashCount = 0f;

    public float hitWeight = 0.1f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    public float ballSpeed;

    public GameManager GameManager; // ゲームマネージャーの参照
    public float Damage1;
    public float Damage2;

    public Status status; // ステータスの参照

    private bool canAddHitCount = true;
    private bool canAddMP1 = true;
    private bool canAddMP2 = true;

    float hitCool;
    float maxSpeed; // 最大速度



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            status = gameManager.GetComponent<Status>();
        }

        if (status == null)
        {
            Debug.LogError("Status が見つかりません！");
        }

        float dir = Random.Range(0, 2) * 2 - 1;
        moveDirection = new Vector2(dir, 0).normalized;

        currentSpeed = baseSpeed;
        ballSpeed = currentSpeed; // ✅ ここで初期化
        rb.linearVelocity = moveDirection * currentSpeed;
        smashWeight = 10f;
        hitCool = 0.5f;
        hitCount = 0;
        maxSpeed = 20f; // 最大速度
    }

    void FixedUpdate()
    {
        if (currentSpeed + hitWeight <= maxSpeed) // 最大速度制限
        {
            currentSpeed = baseSpeed + hitCount * hitWeight;
            ballSpeed = currentSpeed + smashCount * smashWeight; // 衝突回数に応じて速度を調整

            // 衝突回数に応じて速度を調整
            // 等速直線運動（速度補正）
        }

        else
        {
            currentSpeed = maxSpeed; // 最大速度を超えないように制限
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
            smashCount = 0;

            if (canAddHitCount)
            {
                hitCount++;
                StartCoroutine(HitCountCooldown(hitCool));
            }
        }
        else if (collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
            rb.linearVelocity = moveDirection * ballSpeed;
        }

        if (collision.collider.CompareTag("CastleR"))
        {
            CastleController castle = collision.collider.GetComponent<CastleController>();
            if (castle != null)
            {
                Damageto1();
            }

            void Damageto1()
            {
                Damage2 = (Mathf.Abs((status.ATK2 - status.DEF1) + (status.ATK2 - status.DEF1)) / 2f)
                        + status.MAG2 * smashCount + Mathf.Abs(hitCount / 10) + 1f;
                castle.TakeDamage(Damage2);
                Debug.Log((collision.collider.CompareTag("CastleR") ? "左" : "右") + "の城にダメージ！");
            }

            smashCount = 0;
        }

        if (collision.collider.CompareTag("CastleB"))
        {
            CastleController castle = collision.collider.GetComponent<CastleController>();
            if (castle != null)
            {
                Damageto2();
            }

            void Damageto2()
            {
                Damage1 = (Mathf.Abs((status.ATK1 - status.DEF2) + (status.ATK1 - status.DEF2)) / 2f)
                        + status.MAG1 * smashCount + Mathf.Abs(hitCount / 10) + 1f;
                castle.TakeDamage(Damage1);
                Debug.Log((collision.collider.CompareTag("CastleB") ? "右" : "左") + "の城にダメージ！");
            }

            smashCount = 0;
        }

        if (collision.collider.CompareTag("PlayerR") && canAddMP1)
        {
            GameManager.mp1++;
            StartCoroutine(MPCooldown(1));
        }

        if (collision.collider.CompareTag("PlayerB") && canAddMP2)
        {
            GameManager.mp2++;
            StartCoroutine(MPCooldown(2));
        }
    }
    private IEnumerator HitCountCooldown(float duration)
    {
        canAddHitCount = false;
        yield return new WaitForSeconds(duration);
        canAddHitCount = true;
    }

    private IEnumerator MPCooldown(int player)
    {
        if (player == 1)
        {
            canAddMP1 = false;
            yield return new WaitForSeconds(hitCool);
            canAddMP1 = true;
        }
        else if (player == 2)
        {
            canAddMP2 = false;
            yield return new WaitForSeconds(hitCool);
            canAddMP2 = true;
        }
    }

}
