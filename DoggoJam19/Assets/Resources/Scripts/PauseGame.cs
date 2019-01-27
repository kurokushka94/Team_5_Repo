using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pausePanel = null;
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Quit()
    {
        pausePanel.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
