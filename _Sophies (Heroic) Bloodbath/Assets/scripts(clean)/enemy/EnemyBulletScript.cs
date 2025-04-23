using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public int bullletDamage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindAnyObjectByType<AudioManager>().Play("impact");

            PlayerHealthScript playerHP = collision.GetComponent<PlayerHealthScript>();
            playerHP.takeDamage(bullletDamage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
