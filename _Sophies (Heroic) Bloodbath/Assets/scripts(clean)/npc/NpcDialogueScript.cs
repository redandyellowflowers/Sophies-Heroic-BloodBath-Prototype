using UnityEngine;
using UnityEngine.UI;

public class NpcDialogueScript : MonoBehaviour
{
    public GameObject dialogueUI;
    public GameObject pressInteractText;

    public CameraFollowScript cam;

    public PlayerControllerScript MovementScript;

    public string[] findDialogueUi;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowScript>();
        MovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();

        pressInteractText = GameObject.Find(findDialogueUi[0]);
        dialogueUI = GameObject.Find(findDialogueUi[1]);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pressInteractText.SetActive(false);
        dialogueUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MovementScript.gameObject == null)
        {
            gameObject.GetComponent<NpcDialogueScript>().enabled = false;
        }
    }

    public void whileInTriggerRaius()
    {
        pressInteractText.SetActive(true);

        if (Input.GetKey(KeyCode.E))
        {
            startDialogue();
        }
    }

    /*
    public void ButtonStartPress()
    {
        startDialogue();
    }
    */

    public void startDialogue()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(true);
            MovementScript.moveSpeed = 0f;
            cam.yOffset = 0f;
        }
    }

    public void endOfDialogue()//this will be called using an onclick event
    {
        MovementScript.moveSpeed = 12f;
        cam.yOffset = 1f;

        pressInteractText.SetActive(false);
        dialogueUI.SetActive(false);

        //Destroy(dialogueUI, .5f);
    }
}
