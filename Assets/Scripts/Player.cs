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
	}

	void FixedUpdate()
	{
		gameObject.transform.position += movementVector * moveSpeed * Time.fixedDeltaTime;
	}
}
