using Unity.VisualScripting;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    public GameObject player;

    public Rigidbody2D rigidBody;

    public int detectionRadius = 10;

    [HideInInspector]
    public float distance;

    Vector2 playerPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public void Update()
    {
        if (player != null)
        {
            playerPos = player.transform.position;
        }
        else
            gameObject.GetComponent<EnemyControllerScript>().enabled = false;
    }

    public void FixedUpdate()//used primarily when dealing with physics
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        EnemyShootingScript enemyShoot = gameObject.GetComponent<EnemyShootingScript>();

        if (distance < detectionRadius && enemyShoot.hasLineOfSight && player != null)
        {
            Vector2 lookDirection = playerPos - rigidBody.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            rigidBody.rotation = angle;
        }
    }

    //this is wherein we can add the enemy follow player mechanic
}
