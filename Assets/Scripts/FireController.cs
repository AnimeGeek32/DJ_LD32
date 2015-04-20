using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {
	public bool isDying = false;
	public int health = 10;
	public int pointsForThePlayer = 20;

	// Use this for initialization
	void Start () {
		isDying = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Urine") {
			LoseHealth();
			Destroy (collider.gameObject);
		}
	}

	public void LoseHealth() {
		if (!isDying) {
			health--;
			if (health <= 0) {
				StartCoroutine("DieSequence");
				isDying = true;
			}
		}
	}

	IEnumerator DieSequence() {
		GetComponent<Animator>().SetBool("IsDying", true);
		yield return new WaitForSeconds(0.2f);
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpdateScore(pointsForThePlayer);
		Destroy (this.gameObject);
	}
}
