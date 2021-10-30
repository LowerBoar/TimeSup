using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private float moveSpeed = 10f;
	private Vector3 movementVector;

	void Start()
    {
        
    }

	void Update()
	{
		movementVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		var mousePosition = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) - 90f);
	}

	void FixedUpdate()
	{
		gameObject.transform.position += movementVector * moveSpeed * Time.fixedDeltaTime;
	}
}
