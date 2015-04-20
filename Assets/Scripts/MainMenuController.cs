using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	public string nextScene;
	public Text highScoreText;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("High Score")) {
			highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("High Score");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			Application.LoadLevel(nextScene);
		}
	}
}
