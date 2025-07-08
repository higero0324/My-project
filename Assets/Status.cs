using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CharacterStatus
{
    public string name;
    public float HP;
    public float ATK;
    public float DEF;
    public float MAG;
    public float SkillCool; // スキルのクールタイム
    public SkillBase skill;
    public SkillBase RainBress;
    public SkillBase Power;

    public CharacterStatus(string name, float hp, float atk, float def, float mag, float skillCool, SkillBase skill = null)
    {
        this.name = name;
        this.HP = hp;
        this.ATK = atk;
        this.DEF = def;
        this.MAG = mag;
        this.SkillCool = skillCool;
        this.skill = skill;
    }
}

public class Status : MonoBehaviour
{
    public GameManager gameManager;
    public BallController ball;

    public float HP1, ATK1, DEF1, MAG1 , skillCool1;
    public float HP2, ATK2, DEF2, MAG2 , skillCool2;

    public List<CharacterStatus> statusList = new List<CharacterStatus>();

    public int select1;
    public int select2;
    public CharacterStatus selected1;
    public CharacterStatus selected2;

    void Awake()
    {
        var rainBressSkill = ScriptableObject.CreateInstance<RainBress>();
        var powerSkill = ScriptableObject.CreateInstance<Power>();
        

        statusList.Add(new CharacterStatus("RainBress", 35f, 5f, 1f, 3f, 20f , rainBressSkill));
        statusList.Add(new CharacterStatus("Power", 25f, 7f, 3f, 1f , 20f , powerSkill));
        statusList.Add(new CharacterStatus("Speed", 25f, 5f, 2f, 3f , 10f));
        statusList.Add(new CharacterStatus("Gigant", 25f, 5f, 3f, 3f , 15f));

        select1 = 1;
        select2 = 0;

        selected1 = statusList[select1];
        selected2 = statusList[select2];

        HP1 = selected1.HP;
        ATK1 = selected1.ATK;
        DEF1 = selected1.DEF;
        MAG1 = selected1.MAG;
        skillCool1 = selected1.SkillCool;

        HP2 = selected2.HP;
        ATK2 = selected2.ATK;
        DEF2 = selected2.DEF;
        MAG2 = selected2.MAG;
        skillCool2 = selected2.SkillCool;
    }
}
