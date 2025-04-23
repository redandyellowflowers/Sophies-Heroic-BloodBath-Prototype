using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{
    public GameObject pressInteractText;
    public GameObject levelCompleteUi;

    //'serializedfield' means that these are still private, but are now accessable in the editor >> removed the variables, but still good to know

    public void Awake()
    {
        gameObject.SetActive(false);

        pressInteractText = GameObject.Find("finish level button");
        pressInteractText.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelCompleteUi.SetActive(false);

        FindAnyObjectByType<AudioManager>().Play("enemies are dead");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        pressInteractText.SetActive(true);

        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            InteractToWin();
        }
    }

    public void InteractToWin()
    {
        levelCompleteUi.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (pressInteractText != null)
        {
            pressInteractText.SetActive(false);
        }
    }
}
