using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {

		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 15.0f;
		transform.position = Camera.main.ScreenToWorldPoint(mousePos - new Vector3(0.0f, 0.0f, 11.0f));
	}
}
