using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy2 : MonoBehaviour {

	[SerializeField]
	private float speed;

	[SerializeField]
	private GameObject smoke;

	[SerializeField]
	private GameObject missile;

	[SerializeField]
	private GameObject collision;

	private Rigidbody rb;

	private bool phase1;
	private bool phase2;
	private bool phase3;
	private float stopPosition;
	private bool dead;

	private LinkedList<GameObject> smokes;

	void Start () {

		rb = GetComponent<Rigidbody>();
		phase1 = true;
		phase2 = false;
		phase3 = false;
		dead = false;
		smokes = new LinkedList<GameObject>();

		InvokeRepeating("fire", 22.0f, 2.0f);
	}
	
	void Update () {

		if (!dead)
		{
			if (transform.position.x < 20.0f && phase1)
			{
				transform.Translate(Vector3.forward * speed * Time.deltaTime);
			}
			else if (transform.position.x > -15.0f && phase2)
			{
				transform.Translate(Vector3.forward * speed * Time.deltaTime);
				phase1 = false;
			}
			else if (transform.position.x < stopPosition && phase3)
			{
				transform.Translate(Vector3.left * speed * Time.deltaTime);
				phase2 = false;
			}
			else if (phase1 && !phase2)
			{
				transform.position = new Vector3(20.0f, 3.0f, 15.0f);
				transform.rotation = Quaternion.LookRotation(Vector3.left);
				StartCoroutine(pausePhases12());
			}
			else if (!phase1 && phase2)
			{
				transform.position = new Vector3(-20.0f, 2.0f, 10.0f);
				transform.rotation = Quaternion.LookRotation(Vector3.back);
				StartCoroutine(pausePhases23());
			}
		}
	}

	public void setStopPosition(float pos)
	{
		stopPosition = pos;
	}

	void fire()
	{
		if (GameObject.Find("Player") != null)
		{
			Instantiate(missile, transform.position + new Vector3(0.0f, 0.0f, -0.5f), transform.rotation);
			GetComponent<AudioSource>().Play();
		}
	}

	IEnumerator pausePhases12()
	{
		yield return new WaitForSecondsRealtime(2.0f);
		phase2 = true;
	}

	IEnumerator pausePhases23()
	{
		yield return new WaitForSecondsRealtime(2.0f);
		phase3 = true;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player" && !dead)
		{
			rb.isKinematic = false;
			GetComponent<MeshCollider>().isTrigger = false;
			dead = true;
			CancelInvoke();
			rb.AddForce(Vector3.back * 10.0f, ForceMode.Impulse);
			smokes.AddLast(Instantiate(smoke, transform.position, transform.rotation) as GameObject);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Ground" || col.gameObject.name == "Player")
		{
			foreach (GameObject t in smokes)
			{
				Destroy(t);
			}

			GameObject.Find("Score").GetComponent<Score>().addScore(200);
			Instantiate(collision, transform.position, transform.rotation);
			GameObject.Find("GameManager").GetComponent<GameManager>().getEnemy().Play();
			Destroy(gameObject);
		}
	}
}
