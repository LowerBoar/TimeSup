using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Bullet Bullet;

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

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			var bullet = Instantiate(Bullet);
			bullet.Initialize(transform.position, transform.up);
		}
	}

	void FixedUpdate()
	{
		gameObject.transform.position += movementVector * moveSpeed * Time.fixedDeltaTime;
	}
}
