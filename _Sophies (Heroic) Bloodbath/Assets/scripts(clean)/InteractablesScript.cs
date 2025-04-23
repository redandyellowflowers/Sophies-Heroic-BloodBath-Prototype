using UnityEngine;
using UnityEngine.UI;

public class InteractablesScript : MonoBehaviour
{
    public GameObject pressInteractText;
    public GameObject sophieInteractionText;
    public GameObject interactUi;

    public CameraFollowScript cam;

    public PlayerControllerScript MovementScript;

    public static InteractablesScript instance;

    //[SerializeField] bool isDestroyable = false;

    [TextArea(3, 10)]//increases the amount of text lines you can use in editor
    public string interactionInfo = "information relating to whatever it is that was interacted with";

    public string[] findDialogueUi;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowScript>();
        MovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();

        pressInteractText = GameObject.Find(findDialogueUi[0]);
        sophieInteractionText = GameObject.Find(findDialogueUi[1]);
        interactUi = GameObject.Find(findDialogueUi[2]);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sophieInteractionText.SetActive(false);
        pressInteractText.SetActive(false);
        interactUi.SetActive(false);
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
            Interact();
        }
    }

    public void Interact() 
    {
        interactUi.SetActive(true);

        sophieInteractionText.SetActive(true);
        sophieInteractionText.GetComponentInChildren<Text>().text = interactionInfo;

        MovementScript.moveSpeed = 0f;
        cam.yOffset = 0f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pressInteractText.SetActive(false);
        interactUi.SetActive(false);

        MovementScript.moveSpeed = 12f;
        cam.yOffset = 1f;
    }
}
