using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private float currentSceneNumber;
    public float menuSceneNumber;
    public Slider loadingSlider;
    [SerializeField] GameObject pauseMenu;
    bool gameIsPaused;

    private void Start()
    {
        Time.timeScale = 1;
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;   

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentSceneNumber != menuSceneNumber)
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            else
            {
                Application.Quit();
            }
        }
        
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void LoadLevel(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void LoadLevelAsync(string SceneName)
    {
        StartCoroutine(LoadAsync(SceneName));
    }

    public IEnumerator LoadAsync(string SceneName)
    {
        AsyncOperation operation =SceneManager.LoadSceneAsync(SceneName);

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }

    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
