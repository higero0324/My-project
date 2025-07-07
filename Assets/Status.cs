using UnityEngine;

public class Status : MonoBehaviour
{
    public GameManager gameManager;

    public BallController ball;

    public float ATK1;
    public float DEF1;
    public float HP1;
    public float MAG1;
    public float ATK2;
    public float DEF2;
    public float HP2;
    public float MAG2;

    void Awake()
    {
        ATK1 = 5f;
        DEF1 = 3f;
        HP1 = 25f;
        MAG1 = 5f;
        ATK2 = 5f;
        DEF2 = 3f;
        HP2 = 25f;
        MAG2 = 5f;

    }

    void Update()
    {
        // ステータスの表示や更新処理をここに追加
    }
}
