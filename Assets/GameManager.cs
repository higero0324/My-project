using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;     // 終了画面UI
    public Text gameOverText;

    public BallController ball;
    
    public float mp1;

    public float mc1;

    public float mp2;

    public float mc2;           // ボールの参照

    private bool isGameOver = false;

    void Start()
    {
        ball.smashCount = 0;

        mp1 = 0;
        mp2 = 0;
        mc1 = 0;
        mc2 = 0;
    }
    void Update()
    {
        mc1 = Mathf.Floor(mp1 / 10);
        mc2 = Mathf.Floor(mp2 / 10);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (mc1 > 0)
            {
                mp1 -= 10;
                ball.smashCount = 1; // スマッシュを発動
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (mc2 > 0)
            {
                mp2 -= 10;
                ball.smashCount = 1; // スマッシュを発動
            }
        }
    }
    public void GameOver(string winner)
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverPanel.SetActive(true);
        gameOverText.text = winner + " の勝ち！";

        // 必要なら、ボールやプレイヤーの操作停止処理もここに入れる
        Time.timeScale = 0f; // ゲームを停止
    }


}
