using UnityEngine;
using UnityEngine.Rendering;

public class WinConditionScript : MonoBehaviour
{
    public GameObject endTrigger;
    public GameObject obstruction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //endTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        obstruction = GameObject.FindGameObjectWithTag("Obstruction");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int numberOfEnemies = enemies.Length;

        if (numberOfEnemies <= 0)
        {
            FindAnyObjectByType<AudioManager>().Stop("background");

            endTrigger.SetActive(true);

            if (obstruction != null)//this is where we can add the keycard system with a '&& has keycard bool == true' or something 
            {
                Destroy(obstruction, .5f);
            }
        }
    }
}
