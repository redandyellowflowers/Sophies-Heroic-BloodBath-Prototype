using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    /*
    Title: How to Make Camera Follow In UNITY 2D
    Author: Anup Prasad
    Date: 08 April 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=FXqwunFQuao
     */

    public float followSpeed = 2f;
    public float yOffset = 1f;

    public GameObject player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            gameObject.GetComponent<CameraFollowScript>().enabled = false;
        }

        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, -10);//defines the position (the current x, y axes, or the "newPosition") of the object to be followed (which is executed in the following line)
        transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}
