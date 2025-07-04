using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CastleController : MonoBehaviour
{
    public int maxHP = 10;
    private int currentHP;

    [Header("この城に対応するHPバー")]
    public Slider hpSlider;

    private bool isInvincible = false;

    void Start()
    {
        currentHP = maxHP;

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }
    }

    public void TakeDamage(int damage)
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
            // ゲームオーバー処理など
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
