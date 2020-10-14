using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public CharacterController control;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	public float speed = 12f;
	public float gravity = -9.81f;
	public float jumpHeight = 3f;
	public bool canOpen;
	public float thrust;
	public GameObject ball;
	public GameObject ground;
	public Text gtext;

	private Vector3 velocity;
	private bool isGrounded;
	private bool gotKey;
	private bool done;
	private float timer;

    // Start is called before the first frame update
    void Start()
    {
    	canOpen = false;
    	gotKey = false;
    	timer = 5f;
    	done = false;
    	Physics.IgnoreLayerCollision(0, 10);
    }

    void Update()
    {
    	//checking to see if player is on the ground to jump and move around
    	isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    	if (isGrounded && velocity.y < 0f)
    		velocity.y = -2f;

    	float x = Input.GetAxis("Horizontal");
    	float z = Input.GetAxis("Vertical");

    	Vector3 move = transform.right * x + transform.forward * z;
    	control.Move(move * speed * Time.deltaTime);

    	velocity.y += gravity * Time.deltaTime;
    	control.Move(velocity * Time.deltaTime);

    	if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    		velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2f);

    	//check if the ball is outside by seeing if it is touching the ground
    	if (ball.transform.position.y - 1.1f <= ground.transform.position.y && done == false)
    	{
    		gtext.text = "Outside is a better place to play soccer...";
    		timer -= Time.deltaTime;

    		if (timer <= 0f)
    		{
    			gtext.text = "";
    			done = true;
    		}
    	}

    	//quit application
    	if (Input.GetKeyDown(KeyCode.Q))
    		Application.Quit();

    }

    void FixedUpdate()
    {
    	//allowing player to kick a soccer ball
    	float distance = Vector3.Distance(ball.transform.position, transform.position);
    	if (distance <= 4.2f)
    	{
			gtext.text = "Press 'R' to kick ball";
    		if (Input.GetKeyDown(KeyCode.R))
    			ball.GetComponent<Rigidbody>().AddForce(transform.forward * thrust);
    	}
    	else if (distance > 4.2f && distance <= 4.4f)
    		gtext.text = "";
    	
    }

     void OnTriggerEnter(Collider other)
    {
    	//check if player has collided with certain objects
    	if (other.CompareTag("key"))
    	{
    		gotKey = true;
    		Destroy(other.gameObject);
    	}

    	if (other.CompareTag("welcome") && gotKey == true)
    	{
    		gtext.text = "Press 'E' to open/close door";
    		canOpen = true;
    	}
    }

    void OnTriggerExit(Collider other)
    {
    	if (other.CompareTag("welcome"))
    	{
    		canOpen = false;
    	}

    	gtext.text = "";
    }
}
