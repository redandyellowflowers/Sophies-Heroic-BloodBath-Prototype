using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerShootingScript : MonoBehaviour
{
    public int currentAmmo, maxAmmo;
    public int damage = 5;
    public float range = 15f;

    public Text ammoCountText;
    public Text reloadPromptText;

    public GameObject firePoint;
    public LineRenderer lineRenderer;

    private bool hasLineOfSight = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammoCountText.text = currentAmmo + "/" + maxAmmo.ToString();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Reload();
    }

    public void Shoot()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentAmmo -= 1;

            FindAnyObjectByType<AudioManager>().Play("shoot");

            print("Ammo count " + currentAmmo);
            ammoCountText.text = currentAmmo + "/" + maxAmmo.ToString();

            if (currentAmmo <= 0)
            {
                currentAmmo = 0;
                ammoCountText.text = "--/" + maxAmmo;

                FindAnyObjectByType<AudioManager>().Stop("shoot");
                FindAnyObjectByType<AudioManager>().Play("empty");

                print("out of ammo");

                reloadPromptText.enabled = true;
                lineRenderer.gameObject.SetActive(false);
            }

            StartCoroutine(targetRays());
        }
    }

    public IEnumerator targetRays()//as i understand it, IEnumerators allow for timers, or other such functions, and is convenient for that
    {
        RaycastHit2D ray = Physics2D.Raycast(firePoint.transform.position, transform.up * range);
        if (ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag("Enemy");
            if (hasLineOfSight)
            {
                Debug.DrawRay(firePoint.transform.position, transform.up * range, Color.green);

                TargetHealthScript target = ray.transform.GetComponent<TargetHealthScript>();
                if (target != null && currentAmmo != 0)//meaning that this will only happen when the object being fired at has a target script
                {
                    target.takeDamage(damage);
                }

                lineRenderer.SetPosition(0, firePoint.transform.position);
                lineRenderer.SetPosition(1, ray.point);
                lineRenderer.material.SetColor("_Color", Color.red);

            }
            else
            {
                Debug.DrawRay(firePoint.transform.position, transform.up * range, Color.red);

                lineRenderer.SetPosition(0, firePoint.transform.position);
                lineRenderer.SetPosition(1, ray.point);
                lineRenderer.material.SetColor("_Color", Color.white);
            }

            lineRenderer.enabled = true;

            yield return new WaitForSeconds(0.02f);

            lineRenderer.enabled = false;

            /*
            if (ray.collider.CompareTag("Respawn"))
            {
                print("I will not do that.");
            */
        }
    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentAmmo < maxAmmo)
            {
                FindAnyObjectByType<AudioManager>().Play("reload");
            }

            currentAmmo = maxAmmo;
            ammoCountText.text = currentAmmo + "/" + maxAmmo.ToString();

            reloadPromptText.enabled = false;
            lineRenderer.gameObject.SetActive(true);
        }
    }
}
