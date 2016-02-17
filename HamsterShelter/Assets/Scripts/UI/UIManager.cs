using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject LevelPassedMenu, GameOverMenu;

    public Counter WallCounter;

    void Awake()
    {
        Instance = this;
    }

    public void ShowLevelPassedMenu()
    {
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
}
