using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	[SerializeField]
	private float speed;

	void Start () {
		
	}
	
	void Update () {

		transform.Translate(Vector3.back * speed * Time.deltaTime);
	}
}
