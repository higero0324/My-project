using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Power")]
public class Power : SkillBase
{
    // Power.cs
    public override void Activate(GameObject target, GameManager gm)
    {
        gm.StartCoroutine(ApplyBoost(gm.status, target));
    }

    private IEnumerator ApplyBoost(Status status, GameObject target)
    {
        Debug.Log("パワーアップ開始！");

        bool isRight = target.CompareTag("CastleR");

        if (isRight)
        {
            status.ATK1 += 5f;
        }
        else
        {
            status.ATK2 += 5f;
        }

        yield return new WaitForSeconds(5f);

        if (isRight)
        {
            status.ATK1 = status.selected1.ATK;
        }
        else
        {
            status.ATK2 = status.selected2.ATK;
        }

        Debug.Log("パワーアップ終了！");
    }

}
