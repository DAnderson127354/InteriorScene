using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
	//this script will be used to program simple AI where the object will move around
	//and avoid obstacles

	CharacterController controller;
	public float speed;
	public LayerMask mask;
	public LayerMask mask1;
	public LayerMask mask2;
	public Light lt;

	private Vector3 targetRotation;
	private float timer;
	private float direction;
	private bool collision;
	float duration = 1.0f;
    Color color0 = Color.red;
    Color color1 = new Color(0.2f, 0.1f, 0.1f);

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

		// Set random initial rotation
		direction = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, direction, 0);
		timer = 10;
    }

    // Update is called once per frame
    void Update()
    {
    	//When the timer reaches zero, or the raycast collides with with objects on certain layermasks/objects
        timer -= Time.deltaTime;
        RaycastHit hit;
        var forward = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(transform.position, forward * 3, Color.blue);
        
        if (timer <= 0 || Physics.Raycast(transform.position, forward, out hit, 3, mask) || 
        	Physics.Raycast(transform.position, forward, out hit, 3, mask1) || 
        	Physics.Raycast(transform.position, forward, out hit, 3, mask2) || collision == true)
        {
        	//change direction and reset timer
        	direction = Random.Range(0, 360);
			transform.eulerAngles = new Vector3(0, direction, 0);
			SetTimer();
        }
        
		controller.SimpleMove(forward * speed);
		
		// set light color
        float t = Mathf.PingPong(Time.time, duration) / duration;
        lt.color = Color.Lerp(color0, color1, t);
    }

    void SetTimer()
    {
    	timer = Random.Range(10, 15);
    }

    void OnTriggerEnter(Collider other)
    {
    	//if it collides with anything other than these specific objects
    	if (!(other.CompareTag("ground") || other.CompareTag("welcome")))
    	{
    		collision = true;
    	}
    }

    void OnTriggerExit(Collider other)
    {
    	collision = false;
    }
}
