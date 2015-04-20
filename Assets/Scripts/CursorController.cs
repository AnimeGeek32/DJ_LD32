using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotspot = Vector2.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {
		Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
	}

	void OnMouseExit() {
		Cursor.SetCursor (null, Vector2.zero, cursorMode);
	}
}
