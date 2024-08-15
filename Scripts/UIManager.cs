using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject GameOverPanel;
    public TextMeshProUGUI TapCount;

    public void Awake()    // Initialize singleton instance.
    {
        instance = this;
    }

    public void Start()    // Hide game over panel at start.
    {
        GameOverPanel.SetActive(false);
    }

    public void Update()
    {
        if (GameManager.instance.IsGameOver)    // Check if the game is over and show the game over panel.
        {
            GameOverPanel.SetActive(true);
        }
    }

    public void RetartBtnClick()    // Reload the initial scene to restart the game.
    {
        SceneManager.LoadScene(0);
    }

    public void TapText()    // Update the tap count display.
    {
        TapCount.text = ("Score: ") + TargetPointer.instance.Tap.ToString();
    }

    public void QuitBtnClick()    // Quit the game.
    {
        Application.Quit();
    }

}