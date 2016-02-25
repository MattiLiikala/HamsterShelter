using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject LevelPassedMenu, GameOverMenu;
    [SerializeField]
    private Button FastForwardButton;

    private bool fastForward;

    public Counter WallCounter;

    void Awake()
    {
        Instance = this;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void ShowLevelPassedMenu()
    {
        LevelPassedMenu.GetComponent<LevelPassedMenu>().ShowScore();
        EnableUIElement(LevelPassedMenu);
    }

    public void ShowGameOverMenu()
    {
        EnableUIElement(GameOverMenu);
    }

    public void EnableUIElement(GameObject uiElement)
    {
        uiElement.SetActive(true);
    }

    public void DisableUIElement(GameObject uiElement)
    {
        uiElement.SetActive(false);
    }

    public void ToggleUIElement(GameObject uiElement)
    {
        uiElement.SetActive(!uiElement.activeSelf);
    }

    public void ToggleFastForward()
    {
        fastForward = !fastForward;

        var colors = FastForwardButton.colors;
        colors.normalColor = fastForward ? Color.green : Color.white;

        FastForwardButton.colors = colors;
    }

    void Update()
    {
        if (fastForward)
        {
            MeteorRain.Instance.WaitTimer -= Time.deltaTime * 5.0f;
        }
    }
}
