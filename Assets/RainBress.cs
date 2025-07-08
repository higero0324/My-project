using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/RainBress")]
public class RainBress : SkillBase
{
    public override void Activate(GameObject target, GameManager gm)
    {
        CastleController castle = target.GetComponent<CastleController>();
        if (castle != null )
        {
            castle.StartCoroutine(HealOverTime(castle));
        }
        else
        {
            Debug.LogWarning("CastleController が見つからないか、スキルが実行中です。");
        }
    }

    private IEnumerator HealOverTime(CastleController castle)
    {
        for (int i = 0; i < 10; i++)
        {
            castle.Heal(1f);
            yield return new WaitForSeconds(1f);
        }
    }
}
