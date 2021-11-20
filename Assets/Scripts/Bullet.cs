using UnityEngine;

public class Bullet : MonoBehaviour
{
	private readonly float moveSpeed = 15f;
	private Vector3 movementVector;
    private bool shotByPlayer;

	void FixedUpdate()
    {
	    gameObject.transform.position += movementVector * moveSpeed * Time.fixedDeltaTime;
    }

    public void Initialize(Vector3 position, Vector3 direction, bool player)
    {
	    transform.position = position;
	    movementVector = direction;
        shotByPlayer = player;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(shotByPlayer ? "Enemy" : "Player")) {    // TODO There must be better way
            if (shotByPlayer) {
                collision.gameObject.GetComponent<Enemy>().GetDamaged(1f);
            } else {
                collision.gameObject.GetComponent<Player>().GetDamaged(1f);
            }
            Destroy(gameObject);
        }
    }
}
