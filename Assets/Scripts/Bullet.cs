using UnityEngine;

public class Bullet : MonoBehaviour
{
	private readonly float moveSpeed = 15f;
	private Vector3 movementVector;

	void FixedUpdate()
    {
	    gameObject.transform.position += movementVector * moveSpeed * Time.fixedDeltaTime;
    }

    public void Initialize(Vector3 position, Vector3 direction)
    {
	    transform.position = position;
	    movementVector = direction;
    }
}
