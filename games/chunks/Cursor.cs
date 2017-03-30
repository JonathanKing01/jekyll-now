using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	public Vector3 mouse_pos;
	public float depth = 10;

	void Update () {
		mouse_pos = Input.mousePosition;
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mouse_pos.x, mouse_pos.y, depth));
	}
}
