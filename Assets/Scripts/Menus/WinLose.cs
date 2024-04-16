using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    public static bool hasWon;

    public Color[] backgroundColors;
    public Image background;
    public GameObject[] gameoverTexts;

    private void Start()
    {
        if (hasWon)
        {
            GameWin();
        }
        else
        {
            GameLose();
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
            Application.Quit();
    }

    void GameWin()
    {
        background.color = backgroundColors[1];
        gameoverTexts[1].SetActive(true);
        gameoverTexts[0].SetActive(false);
    }

    void GameLose()
    {
        background.color = backgroundColors[0];
        gameoverTexts[0].SetActive(true);
        gameoverTexts[1].SetActive(false);
    }
}
