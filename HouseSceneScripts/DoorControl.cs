using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
	// public Vector3 targetPosition;
	private bool open;
	// private Vector3 startPosition;
	// private Vector3 startRotation;
    public GameObject player;
    public GameObject door;
    private float distance;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
        // startPosition = transform.position;
        player = GameObject.Find("Player");
        distance = 10.0f;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player.GetComponent<PlayerController>().canOpen == true)
        {
            if (Vector3.Distance(door.transform.position, player.transform.position) <= distance)
            {
                if (open == false)
                {
                    // transform.position = targetPosition;
                    // transform.Rotate(0f, -90f, 0f, Space.World);
                    open = true;
                    anim.SetBool("open", true);
                }
                else
                {
                    // transform.position = startPosition;
                    // transform.Rotate(0f, 90f, 0f, Space.World);
                    open = false;
                    anim.SetBool("open", false);
                }
            }
        	
        }
    }
}
