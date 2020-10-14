using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public float mouseSensitivity;
	public Transform playerBody;
	private float xRotation;

	void Start()
	{
		mouseSensitivity = 100f;
		xRotation = 0f;
		Cursor.lockState = CursorLockMode.Locked; // keep mouse at center of the screen
	}

	void Update()
	{
		//camera moves with mouse
		transform.position = new Vector3(playerBody.position.x, playerBody.position.y + 3.3f, playerBody.position.z);
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f); //keep from player looking behind them by looking up/down

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * mouseX);
	}
	
}
