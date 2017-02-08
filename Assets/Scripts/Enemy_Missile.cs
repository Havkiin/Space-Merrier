using UnityEngine;
using System.Collections;

public class Enemy_Missile : MonoBehaviour
{

	[SerializeField]
	private float speed;

	private Vector3 playerPos;

	void Start()
	{
		playerPos = GameObject.Find("Player").transform.position;
		transform.rotation = Quaternion.LookRotation(playerPos - transform.position);
	}

	void Update() {

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
