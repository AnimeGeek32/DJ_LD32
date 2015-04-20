using UnityEngine;
using System.Collections;

public class UrineController : MonoBehaviour {
	public float speed = 50.0f;
	//public float maxSpeed = 40.0f;
	//public float angle = 0.0f;

	// Use this for initialization
	void Start () {
		//angle = transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		/*
		Vector2 urineVelocity = Vector2.zero;
		GetComponent<Rigidbody2D>().AddForce(transform.up * speed * Time.fixedDeltaTime);
		urineVelocity = GetComponent<Rigidbody2D>().velocity;

		if (urineVelocity.x > Mathf.Abs(maxSpeed)) {
			urineVelocity.x = Mathf.Sign(urineVelocity.x) * maxSpeed;
		}
		if (urineVelocity.y > Mathf.Abs(maxSpeed)) {
			urineVelocity.y = Mathf.Sign(urineVelocity.y) * maxSpeed;
		}

		GetComponent<Rigidbody2D>().velocity = urineVelocity;
		*/
		GetComponent<Rigidbody2D>().velocity = (transform.up * speed * Time.fixedDeltaTime);
	}

	/*
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Wall") {
			Destroy(this.gameObject);
		}
	}
	*/

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Wall") {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.tag == "Wall") {
			Destroy(this.gameObject);
		}
	}
}
