using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {
	public float minSpawnTime = 1.0f;
	public float maxSpawnTime = 6.0f;
	public GameObject[] entityPrefabs;

	private float currentSpawnTime = 0.0f;

	// Use this for initialization
	void Start () {
		currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		StartCoroutine("SpawnSomething");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SpawnSomething() {
		yield return new WaitForSeconds(currentSpawnTime);
		Vector2 spawnBox = new Vector2(transform.localScale.x / 2, transform.localScale.y / 2);
		int pickedNumber = Random.Range (0, entityPrefabs.Length);
		float spotX = Random.Range(-spawnBox.x, spawnBox.x) + transform.position.x;
		float spotY = Random.Range(-spawnBox.y, spawnBox.y) + transform.position.y;
		Instantiate(entityPrefabs[pickedNumber], new Vector3(spotX, spotY, 0), Quaternion.identity);
		yield return StartCoroutine("SpawnSomething");
	}
}
