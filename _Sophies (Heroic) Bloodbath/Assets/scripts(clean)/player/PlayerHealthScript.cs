using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    public int currentHealth, maxHealth;
    int lowHealthAlert = 4;

    public Text healthText;

    public GameObject damageScreenUI;
    public GameObject gameOverUI;
    public GameObject impactEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthText.text = currentHealth + "/" + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < lowHealthAlert)
        {
            damageScreenUI.SetActive(true);
        }
        else
            damageScreenUI.SetActive(false);

        if (currentHealth <= 0)
        {
            gameOverUI.SetActive(true);
            damageScreenUI.SetActive(false);

            Time.timeScale = 0f;

            if (Input.GetKey(KeyCode.Backspace))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        healthText.text = currentHealth + "/" + maxHealth.ToString();
        print("Enemy Damage " + currentHealth);

        GameObject impactGameobject = Instantiate(impactEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Destroy(impactGameobject, 5f);

        if (currentHealth <= 0)
        {
            Destroy(gameObject, .5f);
        }
    }

    public void addHealth(int amount)
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth + "/" + maxHealth.ToString();
        if (currentHealth <= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
