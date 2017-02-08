using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy1 : MonoBehaviour {

	[SerializeField]
	private float speed;

	[SerializeField]
	private GameObject smoke;

	[SerializeField]
	private GameObject collision;

	private Rigidbody rb;
	private bool dead;
	private float stopPosition;
	private LinkedList<GameObject> smokes;

	void Start () {

		rb = GetComponent<Rigidbody>();
		dead = false;
		smokes = new LinkedList<GameObject>();
	}
	
	void Update () {

		if (transform.position.z < stopPosition)
		{
			transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
		}
	}

	public void setStopPosition(float pos)
	{
		stopPosition = pos;
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player" && !dead)
		{
			rb.isKinematic = false;
			GetComponent<MeshCollider>().isTrigger = false;
			rb.AddForce(Vector3.back * 10.0f, ForceMode.Impulse);
			smokes.AddLast(Instantiate(smoke, transform.position, transform.rotation) as GameObject);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Ground" || col.gameObject.name == "Player")
		{
			foreach (GameObject t in smokes)
			{
				Destroy(t);
			}

			GameObject.Find("Score").GetComponent<Score>().addScore(100);
			Instantiate(collision, transform.position, transform.rotation);
			GameObject.Find("GameManager").GetComponent<GameManager>().getEnemy().Play();
			Destroy(gameObject);
		}
	}
}
