using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager> 
{

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        //if this is the last scene, don't load the next one
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount - 1) return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void RainEnded()
    {
        UIManager.Instance.ShowLevelPassedMenu();
    }

    public void GameOver()
    {
        UIManager.Instance.ShowGameOverMenu();
        Time.timeScale = 0.0f;
    }

	public void Exit() 
    {
		Application.Quit();
    }
}
