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

	private InputRecorder inputRecorder;

	void Start()
	{
		pressedKeys = new HashSet<KeyCode>();
		inputRecorder = new InputRecorder();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P)) {
			manualControl = !manualControl;
			inputRecorder.Reset();
		}

		if (manualControl) {
			GetManualInputs();

			var mousePosition = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition); // TODO Handle later somehow
			transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) - 90f);
		} else {
			GetRecordedInputs();
		}

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
		inputRecorder.Update(Time.deltaTime);
	}

	private void GetManualInputs()
	{
		foreach (var key in controlKeys) {
			if (Input.GetKeyDown(key)) {
				pressedKeys.Add(key);
				inputRecorder.Record(new InputEvent(key, true, transform.rotation));
			}

			if (Input.GetKeyUp(key)) {
				pressedKeys.Remove(key);
				inputRecorder.Record(new InputEvent(key, false, transform.rotation));
			}
		}
	}

	private void GetRecordedInputs()
	{
		foreach (var @event in inputRecorder.GetEvents()) {
			if (@event.IsPressed) {
				pressedKeys.Add(@event.Key);
			} else {
				pressedKeys.Remove(@event.Key);
			}

			transform.rotation = @event.Rotation;	// TODO Maybe should not be on this level
		}
	}

	void FixedUpdate()
	{
		gameObject.transform.position += movementVector * moveSpeed * Time.fixedDeltaTime;
	}
}
