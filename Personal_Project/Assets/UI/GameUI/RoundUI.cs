using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundUI : MonoBehaviour
{
    [Header("Text Elements")]
    public TextMeshProUGUI roundCounter;
    public TextMeshProUGUI healthCounter;
    public TextMeshProUGUI moneyCounter;
    [Header("Buttons")]
    public GameObject pauseMenu;
    public Button pauseButton;
    public Button exitButton;
    public Button returnButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        pauseButton.onClick.AddListener(pauseButtonClicked);
        exitButton.onClick.AddListener(exitButtonClicked);
        returnButton.onClick.AddListener(returnButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateRoundCounter(int round)
    {
        roundCounter.text = "Round " + round;
    }

    public void updateHealthCounter(int health)
    {
        healthCounter.text = "Health " + health;
    }

    public void updateMoneyCounter(int money)
    {
        moneyCounter.text = "Gold $" + money;
    }
    public void exitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void pauseButtonClicked()
    {
        pauseMenu.SetActive(true);
    }
    public void returnButtonClicked()
    {
        pauseMenu.SetActive(false);
    }
}
