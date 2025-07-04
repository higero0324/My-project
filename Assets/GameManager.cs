using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;     // 終了画面UI
    public Text gameOverText;            // 「青の勝ち！」などのテキスト

    private bool isGameOver = false;

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
