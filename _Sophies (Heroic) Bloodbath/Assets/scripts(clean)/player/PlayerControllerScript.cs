using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{
    [Header("base movement")]
    public float moveSpeed = 12f;

    public Rigidbody2D rigidBody;

    public Camera cam;

    [Header("slow motion")]
    public GameObject sloMoScreen;

    public Text slowMoText;

    public float currentStamina, MaxStamina;
    public float slowMoRate;

    Vector2 mousePos;

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        moveMent();

        //walkAnim.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);//locks rotation of child object by countering the rotation of the parent of object, important for potential walk anim

        slowMoText.text = currentStamina.ToString("0");
    }

    void FixedUpdate()
    {
        /*
        Title: TOP DOWN SHOOTING in Unity
        Author: Asbjørn Thirslund / Brackeys
        Date: 06 April 2025
        Code version: 1
        Availability: https://www.youtube.com/watch?v=LNLVOjbrQj4

        >>>it is the "character controller facing the cursor" mechanic part of the above tutorial being referenced
        */
        Vector2 lookDirection = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rigidBody.rotation = angle;
    }

    public void moveMent()
    {
        Vector3 movePosition = Vector3.zero;//we're defined the vector (the 3 axis with which we need for our controller to move)

        if (Input.GetKey(KeyCode.W))
        {
            movePosition.y += moveSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movePosition.y -= moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movePosition.x -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movePosition.x += moveSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sloMoScreen.SetActive(true);

            Time.timeScale = .5f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            currentStamina -= slowMoRate;

            if (currentStamina <= 0)
            {
                sloMoScreen.SetActive(false);

                currentStamina = 0;
                Time.timeScale = 1f;
            }

            slowMoText.text = currentStamina.ToString("0");
        }
        else
            if (currentStamina != MaxStamina)
        {
            sloMoScreen.SetActive(false);

            Time.timeScale = 1f;
            currentStamina += slowMoRate;

            if (currentStamina >= MaxStamina)
            {
                currentStamina = MaxStamina;
            }
        }

            /*
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            */

            rigidBody.transform.position += movePosition.normalized * moveSpeed * Time.deltaTime;//this line (more so the "normalized" and "time.deltatime") essentially stops the controller from building an unfixed amount of momentum
    }

    //this is referencing the interactable script, though doing it as so isnt clean, it should for now just work
    public void uponExit()//to be called as an onclick event
    {
        FindAnyObjectByType<InteractablesScript>().pressInteractText.SetActive(false);
        FindAnyObjectByType<InteractablesScript>().sophieInteractionText.SetActive(false);

        FindAnyObjectByType<InteractablesScript>().MovementScript.moveSpeed = 12f;
        FindAnyObjectByType<InteractablesScript>().cam.yOffset = 1f;

        /*
        if (isDestroyable)
        {
            Destroy(gameObject, .5f);
        }
        */
    }
}
