using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseUI;

    private bool gameIsPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == false)
        {
            pauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == true)
        {
            resumeGame();
        }
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);

        gameIsPaused = true;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);

        gameIsPaused = false;
    }
}
