using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CastleController : MonoBehaviour
{
    [Header("右の城かどうか（True＝右）")]
    public bool isRightCastle;

    [Header("HP表示用Slider")]
    public Slider hpSlider;

    private Status status;

    private float maxHP;
    private float currentHP;
    private bool isInvincible = false;

    void Start()
    {
        float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float offset = 0f; // 画面端から0ユニットの距離
        Vector3 pos = transform.position;

        if (isRightCastle)
            pos.x = screenHalfWidth - offset;
        else
            pos.x = -screenHalfWidth + offset;

        transform.position = pos;
        // Status取得
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            status = gameManager.GetComponent<Status>();
        }

        if (status != null)
        {
            maxHP = isRightCastle ? status.HP2 : status.HP1;
            currentHP = maxHP;

            if (hpSlider != null)
            {
                hpSlider.maxValue = maxHP;
                hpSlider.value = currentHP;
            }

            Debug.Log($"{gameObject.name} の 初期HP: {maxHP}");
        }
        else
        {
            Debug.LogError("Status スクリプトが見つかりません！");
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
        }

        Debug.Log($"{gameObject.name} の HP: {currentHP}");

        if (currentHP <= 0)
        {
            Debug.Log($"{gameObject.name} が破壊された！");
            // ゲームオーバー処理を書く場所
        }

        StartCoroutine(InvincibleCooldown(0.3f));
    }

    private IEnumerator InvincibleCooldown(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }
}
// 