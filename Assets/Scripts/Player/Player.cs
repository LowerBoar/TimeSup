using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
	private const float Cooldown = 0.5f;

	public Bullet Bullet;

	private float moveSpeed = 10f;
	private Vector3 movementVector;

	private readonly KeyCode[] controlKeys = { KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.Mouse0 };	// TODO Use enum
	private HashSet<KeyCode> pressedKeys;
	private bool manualControl = true;
	private float timeSinceLastShot = Cooldown;

	void Start()
	{
		pressedKeys = new HashSet<KeyCode>();
	}

	void Update()
	{
		if (manualControl) {
			GetManualInputs();
		}

		var mousePosition = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition); // TODO Handle later somehow
		transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) - 90f);

		movementVector = Vector3.zero;
		foreach (var key in pressedKeys) {
			switch (key) {
				case KeyCode.A:
					movementVector.x = -1;
					break;
				case KeyCode.D:
					movementVector.x = 1;
					break;
				case KeyCode.S:
					movementVector.y = -1;
					break;
				case KeyCode.W:
					movementVector.y = 1;
					break;
				case KeyCode.Mouse0:
					if (timeSinceLastShot >= Cooldown) {
						var bullet = Instantiate(Bullet);
						bullet.Initialize(transform.position, transform.up);
						timeSinceLastShot = 0f;
					}
					break;
			}
		}

		timeSinceLastShot += Time.deltaTime;
	}

	private void GetManualInputs()
	{
		foreach (var key in controlKeys) {
			if (Input.GetKeyDown(key)) {
				pressedKeys.Add(key);
			}

			if (Input.GetKeyUp(key)) {
				pressedKeys.Remove(key);
			}
		}
	}

	private void GetRecordedInputs()
	{
		// TODO Use InputRecorder
	}

	void FixedUpdate()
	{
		gameObject.transform.position += movementVector * moveSpeed * Time.fixedDeltaTime;
	}
}
