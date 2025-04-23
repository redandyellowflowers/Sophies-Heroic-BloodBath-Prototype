using TMPro;
using UnityEngine;

public class TargetHealthScript : MonoBehaviour
{
    public int health = 10;

    public GameObject impactEffect;

    public TextMeshPro text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (text != null)
        {
            text.text = health.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        if (text != null)
        {
            text.text = health.ToString();
        }

        //print("Enemy Damage " + health);
        if (health <= 0)
        {
        GameObject impactGameobject = Instantiate(impactEffect, gameObject.transform.position, gameObject.transform.rotation); // instantiate meaning spawn
        //Destroy(impactGameobject, .5f);

        Destroy(gameObject);
        }
    }
}
