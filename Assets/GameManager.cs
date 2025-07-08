using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool isGameOver = false;

    public GameObject gameOverPanel;
    public Text gameOverText;

    public BallController ball;

    public float mp1;
    public float mc1;
    public float mp2;
    public float mc2;
    public Status status;
    public bool isRunning1 = false;
    public bool isRunning2 = false;

    void Start()
    {
        ball.smashCount = 0;
        mp1 = mp2 = mc1 = mc2 = 0;

        status = FindFirstObjectByType<Status>();
        if (status == null)
        {
            Debug.LogError("Status スクリプトが見つかりません！");
        }
    }

    void Update()
    {
        if (isGameOver) return;

        elapsedTime += Time.deltaTime;

        if (timerText != null)
        {
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
                ball.smashCount = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (mc2 > 0)
            {
                mp2 -= 10;
                ball.smashCount = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isRunning1 == false)
            {
                StartCoroutine(skill1());
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isRunning2 == false)
            {
                StartCoroutine(skill2());
            }
        }
    }

    public IEnumerator skill1()
    {
        GameObject castleR = GameObject.FindWithTag("CastleR");
        if (castleR != null)
        {
            isRunning1 = true;

            status.selected1.skill?.Activate(castleR, this);

            yield return new WaitForSeconds(status.skillCool1); // クールタイム
            isRunning1 = false;
        }
        else
        {
            Debug.LogError("CastleR が見つかりません");
        }
    }

    public IEnumerator skill2()
    {
        GameObject castleB = GameObject.FindWithTag("CastleB");
        if (castleB != null)
        {
            isRunning2 = true;

            status.selected2.skill?.Activate(castleB, this);

            yield return new WaitForSeconds(status.skillCool2); // クールタイム
            isRunning2 = false;
        }
        else
        {
            Debug.LogError("CastleB が見つかりません");
        }
    }

    public void GameOver(string winner)
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverPanel.SetActive(true);
        gameOverText.text = winner + " の勝ち！";

        Time.timeScale = 0f;
        Debug.Log($"ゲーム終了！経過時間：{elapsedTime:F1} 秒");
    }
}

