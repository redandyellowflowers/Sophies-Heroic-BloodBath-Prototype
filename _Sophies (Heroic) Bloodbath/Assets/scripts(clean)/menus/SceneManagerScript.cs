using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript sceneManager;//in this case, static means youve made the accessable within other scripts, but wont show in the inspector

    /*
    void Awake()//awake is called before start, and is most usefull in this case, for setting up references to other scripts as its called before the start method
    {
        if (sceneManager == null)
        {
            sceneManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//as a new scene is loaded, the gameobject this script is attached to will follow, and if the object already exists, then this (now duplicate) is deleted
        }
    }
    */

    private void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);//youre getting the scene after the current scene as ordered in the build index
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit.");
    }
}
