using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool isGameOver = false; // ✅ これだけ残す

    public GameObject gameOverPanel;
    public Text gameOverText;

    public BallController ball;

    public float mp1;
    public float mc1;
    public float mp2;
    public float mc2;

    void Start()
    {
        ball.smashCount = 0;
        mp1 = mp2 = mc1 = mc2 = 0;
    }
    void Update()
    {
        if (isGameOver) return;

        elapsedTime += Time.deltaTime;

        if (timerText != null)
        {
                // 時間を「分:秒.1桁ミリ秒」で表示（例：1:23.4）
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            float seconds = elapsedTime % 60f;
            timerText.text = $"{minutes}:{seconds:00.0}";
        }
        
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
        Debug.Log($"ゲーム終了！経過時間：{elapsedTime:F1} 秒");
    }


}
