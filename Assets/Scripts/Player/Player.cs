using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
	private const float Cooldown = 0.5f;

	public Bullet Bullet;
    public float Health { get; set; } = 10f;

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
	}

	void Update()
	{
        if (manualControl) {
			GetManualInputs();

            transform.rotation = Math.LookAt2D(
                transform.position,
                FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition)
            );
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
					if (timeSinceLastShot >= Cooldown) {	// TODO Move to weapon class?
						var bullet = Instantiate(Bullet);
						bullet.Initialize(transform.position, transform.up, true);
                        bullet.transform.rotation = transform.rotation;
						timeSinceLastShot = 0f;
					}
					break;
			}
		}

		timeSinceLastShot += Time.deltaTime;
		inputRecorder.Update(Time.deltaTime);
	}

    void FixedUpdate()
    {
        gameObject.transform.position += movementVector * moveSpeed * Time.fixedDeltaTime;
    }

    public InputRecorder GetInputRecorder()
    {
        return inputRecorder;
    }

    public void SetupInputRecorder(InputRecorder recorder = null)
    {
        if (recorder != null) {
            inputRecorder = recorder;
            inputRecorder.Reset();
            manualControl = false;
        } else {
            inputRecorder = new InputRecorder();
            manualControl = true;
        }
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

    public void GetDamaged(float damage)
    {
        Health -= damage;
    }
}
