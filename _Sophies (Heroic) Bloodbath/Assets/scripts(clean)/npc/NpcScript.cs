using Unity.VisualScripting;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    public GameObject player;

    public int detectionRadius = 7;

    float distance;

    Vector2 playerPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            playerPos = player.transform.position;
        }
        else
            gameObject.GetComponent<NpcScript>().enabled = false;
    }

    void FixedUpdate()//used primarily when dealing with physics
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        NpcDialogueScript dialogue = gameObject.GetComponent<NpcDialogueScript>();

        if (distance < detectionRadius && player != null)
        {
            Rigidbody2D rigidBody = gameObject.GetComponent<Rigidbody2D>();

            Vector2 lookDirection = playerPos - rigidBody.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            rigidBody.rotation = angle;

            dialogue.whileInTriggerRaius();//ADD SMALLER RADIUS FOR THIS SPECIFIC FUNCTION

            if (dialogue.dialogueUI == null)
            {
                dialogue.pressInteractText.SetActive(false);
            }
        }
        else
        {
            dialogue.pressInteractText.SetActive(false);
        }
    }
}
