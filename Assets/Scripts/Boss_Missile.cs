using UnityEngine;
using System.Collections;

public class Boss_Missile : MonoBehaviour {

	[SerializeField]
	private float speed;

	void Start () {
	
	}
	
	void Update () {

		transform.Translate(Vector3.forward * speed * Time.deltaTime);

		if (transform.position.z > 30.0f || transform.position.z < 0.0f)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.name == "Player")
		{
			Destroy(gameObject);
		}
	}
}
