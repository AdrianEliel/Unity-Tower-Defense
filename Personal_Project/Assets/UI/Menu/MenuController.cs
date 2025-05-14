using GLTFast.Schema;
using Unity.Mathematics;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private SceneManager sceneManager;
    private UIDocument mainMenu;
    private VisualElement root;
    private VisualElement mainButtons;
    private VisualElement mapSelection;
    private Button playButton;
    private Button exitButton;
    void Awake()
    {
        
        mainMenu = GetComponentInParent<UIDocument>();
        root= mainMenu.rootVisualElement;

        mainButtons = root.Q<VisualElement>("Buttons");
        mapSelection = root.Q<VisualElement>("Maps");

        mapSelection.style.display = DisplayStyle.None;

        playButton = root.Q<Button>("PlayButton");
        playButton.clicked += playButtonClicked;

        exitButton = root.Q<Button>("ExitButton");
        exitButton.clicked += exitButtonClicked;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void exitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    void playButtonClicked()
    {
        mainButtons.style.display = DisplayStyle.None;
        mapSelection.style.display = DisplayStyle.Flex;

    }

}
