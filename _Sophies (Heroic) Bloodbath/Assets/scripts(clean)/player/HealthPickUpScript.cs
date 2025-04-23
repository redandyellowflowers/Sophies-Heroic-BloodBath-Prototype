using UnityEngine;
using UnityEngine.UI;

public class HealthPickUpScript : MonoBehaviour
{
    public int health = 5;

    public GameObject pressInteractText;

    private void Awake()
    {
        pressInteractText = GameObject.Find("pick up hp ui");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pressInteractText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        pressInteractText.SetActive(true);

        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            PlayerHealthScript playerHealth = collision.GetComponent<PlayerHealthScript>();

            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                FindAnyObjectByType<AudioManager>().Play("health pick");

                playerHealth.addHealth(health);
                Destroy(gameObject, .5f);
            }
            else
                playerHealth.currentHealth = playerHealth.maxHealth;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pressInteractText.SetActive(false);
    }
}
