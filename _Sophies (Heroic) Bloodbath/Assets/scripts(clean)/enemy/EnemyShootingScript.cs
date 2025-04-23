using Unity.VisualScripting;
using UnityEngine;

public class EnemyShootingScript : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;

    [HideInInspector]
    public bool hasLineOfSight = false;

    public LineRenderer lineRenderer;
    public GameObject firePoint;

    public float bulletForce = 20f;

    private float nextTimeToFire = 0f;
    public float fireRate = 1f;

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
            Shooting();
        }
        else
            gameObject.GetComponent<EnemyShootingScript>().enabled = false;
    }

    public void Shooting()
    {
        RaycastHit2D ray = Physics2D.Raycast(firePoint.transform.position, player.transform.position - firePoint.transform.position);

        if (ray.collider != null)
        {
            EnemyControllerScript enemyMove = gameObject.GetComponent<EnemyControllerScript>();

            hasLineOfSight = ray.collider.CompareTag(player.tag);

            if (hasLineOfSight && enemyMove.distance < enemyMove.detectionRadius)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = true;

                if (Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;//adds a bit of delay before the enemy fires (by dividing 1 by the fire rate and adding that to the time.time, which is the current "game" time) as for them to not gun down the player in seconds

                    FindAnyObjectByType<AudioManager>().Play("enemy shoot");

                    GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.transform.up * bulletForce, ForceMode2D.Impulse);
                }

                lineRenderer.SetPosition(0, firePoint.transform.position);
                lineRenderer.SetPosition(1, ray.point);

                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.green);
            }
            else
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;//isnt necessary, COME BACK TO THIS
                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.red);
            }
        }
    }
}
