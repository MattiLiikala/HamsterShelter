using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Singleton = there's always one (and only one) instance of the object and it can easily accessed from anywhere by using UIManager.Instance

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject LevelPassedMenu, GameOverMenu;

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
