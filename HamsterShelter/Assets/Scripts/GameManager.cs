using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AudioSource BackGroundMusicCalm, BackGroundMusicMeteors;

	public bool isPaused;

    [SerializeField]
    private Button muteButton;

    void Awake()
    {
        Instance = this;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Start()
    {
        if (BackGroundMusicCalm != null) BackGroundMusicCalm.enabled = false;
        if (BackGroundMusicMeteors != null) BackGroundMusicMeteors.enabled = false;
    }

    void OnLevelWasLoaded(int level)
    {
        //set normal timescale in case the game was paused before loading;
        Time.timeScale = 1.0f;
    }

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
        StartCoroutine(UIManager.Instance.ShowLevelPassedMenu(3.0f));
    }

    void Update()
    {
        if (MeteorRain.Instance == null) return;

        BackGroundMusicCalm.enabled = !MeteorRain.Instance.HasStarted;
        BackGroundMusicMeteors.enabled = MeteorRain.Instance.HasStarted;
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

	public void PauseGame() {
		Time.timeScale = 0;
		isPaused = true;
	}

	public void ResumeGame() {
		Time.timeScale = 1;
		isPaused = false;
	}

	public void DisableSound() {
		if (AudioListener.volume != 0) {
			AudioListener.volume = 0.0f;
            var colors = muteButton.colors;
            colors.normalColor = Color.green;
            colors.highlightedColor = Color.green;
            muteButton.colors = colors;
            return;
		}
		if (AudioListener.volume == 0) {
			AudioListener.volume = 1;
            var colors = muteButton.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = Color.white;
            muteButton.colors = colors;
            return;
		}
	}
		

}
