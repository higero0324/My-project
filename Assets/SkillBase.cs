using UnityEngine;

public abstract class SkillBase : ScriptableObject
{
    public abstract void Activate(GameObject target, GameManager gm);
}