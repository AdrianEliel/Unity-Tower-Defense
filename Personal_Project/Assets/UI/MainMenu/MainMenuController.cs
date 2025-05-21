using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public Button playButton;
    public Button exitButton;
    public GameObject mapSelection;
    public Button map1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu.SetActive(true);
        mapSelection.SetActive(false);
        playButton.onClick.AddListener(PlayButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
        map1.onClick.AddListener(Map1Clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonClicked()
    {
        mainMenu.SetActive(false);
        mapSelection.SetActive(true);
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
        #if UNITY_EDITOR
        EditorApplication.isPlaying=false;
        #endif
    }

    public void Map1Clicked()
    {
        SceneManager.LoadScene("Map1");
    }
}
