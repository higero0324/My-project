using TMPro;

using UnityEngine;

public class McDisplay : MonoBehaviour
{
    public TextMeshProUGUI mcR;
    public TextMeshProUGUI mcB;
    public GameManager gameManager;
    void Update()
    {
        mcR.text = "mc1: " + gameManager.mc1;
        mcB.text = "mc2: " + gameManager.mc2;
    }
}
