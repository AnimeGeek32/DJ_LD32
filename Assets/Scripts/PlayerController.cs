using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Vector3 _screenMouse;
	private Vector3 _worldMouse;
	private Animator _animatorController;

	public Vector3 direction;
	public float speed = 30.0f;
	public int score = 0;
	public Text scoreUIText;
	public Transform urineSpawnPoint;
	public GameObject urinePrefab;
	public Text urineUIText;
	public int urinePoints = 0;
	public AudioClip beerPickupSound;
	public bool isDying = false;
	public bool isPeeing = false;
	public string gameOverScene = "MainMenu";

	// Use this for initialization
	void Start () {
		_animatorController = GetComponent<Animator>();
		isDying = false;
		isPeeing = false;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isDying) {
			Vector2 playerMovement = Vector2.zero;
			float currentHighVelocity = 0.0f;

			// Retrieve mouse postion
			_screenMouse.x = Input.mousePosition.x;
			_screenMouse.y = Input.mousePosition.y;
			_screenMouse.z = 0;

			// Make the player look at the mouse cursor
			_worldMouse = Camera.main.ScreenToWorldPoint(_screenMouse);
			direction = _worldMouse - transform.position;

			// Retrieve player inputs
			if (Input.GetAxis("Horizontal") < 0) {
				playerMovement.x -= speed * Time.deltaTime;
			} else if (Input.GetAxis("Horizontal") > 0) {
				playerMovement.x += speed * Time.deltaTime;
			} else {
				Vector2 tempVelocity = GetComponent<Rigidbody2D>().velocity;
				tempVelocity.x *= 0.8f;
				GetComponent<Rigidbody2D>().velocity = tempVelocity;
			}
			if (Input.GetAxis("Vertical") < 0) {
				playerMovement.y -= speed * Time.deltaTime;
			} else if (Input.GetAxis("Vertical") > 0) {
				playerMovement.y += speed * Time.deltaTime;
			} else {
				Vector2 tempVelocity = GetComponent<Rigidbody2D>().velocity;
				tempVelocity.y *= 0.8f;
				GetComponent<Rigidbody2D>().velocity = tempVelocity;
			}
			if (Input.GetMouseButtonDown(0)) {
				isPeeing = true;
			} else if (Input.GetMouseButtonUp(0)) {
				isPeeing = false;
			}

			// Check animation state
			if (currentHighVelocity < Mathf.Abs(playerMovement.x)) {
				currentHighVelocity = Mathf.Abs(playerMovement.x);
			} else if (currentHighVelocity < Mathf.Abs(playerMovement.y)) {
				currentHighVelocity = Mathf.Abs(playerMovement.y);
			}

			_animatorController.SetFloat("Velocity", currentHighVelocity);

			transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90.0f);

			// Spawn urine if peeing
			if (isPeeing && urinePoints > 0) {
				urinePoints--;
				Instantiate(urinePrefab, urineSpawnPoint.position, transform.rotation);
				UpdateUI();
			}

			GetComponent<Rigidbody2D>().AddForce(playerMovement);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(!isDying) {
			if(collision.gameObject.tag == "Fire") {
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				_animatorController.SetBool("IsDead", true);
				StartCoroutine("GoToGameOverScene");
				isDying = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(!isDying) {
			if (collider.tag == "Beer") {
				GetComponent<AudioSource>().PlayOneShot(beerPickupSound);
				score += 5;
				urinePoints += 100;
				UpdateUI();
				Destroy (collider.gameObject);
			}
		}
	}

	public void UpdateScore(int value) {
		score += value;
		UpdateUI();
	}

	void UpdateUI() {
		urineUIText.text = "PISS METER: " + urinePoints;
		scoreUIText.text = "SCORE: " + score;
	}

	IEnumerator GoToGameOverScene() {
		// Save the score, if highest
		if (PlayerPrefs.HasKey("High Score")) {
			if (score > PlayerPrefs.GetInt("High Score")) {
				PlayerPrefs.SetInt("High Score", score);
			}
		} else {
			PlayerPrefs.SetInt("High Score", score);
		}
		yield return new WaitForSeconds(4.0f);
		Application.LoadLevel(gameOverScene);
	}
}
