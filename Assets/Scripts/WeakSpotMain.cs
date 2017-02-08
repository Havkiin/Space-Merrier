using UnityEngine;
using System.Collections;

public class WeakSpotMain : MonoBehaviour {

	private int health;
	private Renderer rend;
	private float closeTime;
	private bool dead;

	void Start () {

		health = 30;
		rend = GetComponent<Renderer>();
		rend.material.color = Color.grey;
		closeTime = 1000000.0f;
	}
	
	void Update () {
	
		if (GameObject.Find("WeakSpot1").GetComponent<WeakSpot>().isDown() && GameObject.Find("WeakSpot2").GetComponent<WeakSpot>().isDown())
		{
			GetComponent<Renderer>().material.color = Color.red;
			GetComponent<SphereCollider>().isTrigger = true;
			closeTime = Time.timeSinceLevelLoad;
			GameObject.Find("WeakSpot1").GetComponent<WeakSpot>().restore();
			GameObject.Find("WeakSpot2").GetComponent<WeakSpot>().restore();
		}

		if (Time.timeSinceLevelLoad - closeTime > 5.0f)
		{
			GetComponent<Renderer>().material.color = Color.grey;
			GetComponent<SphereCollider>().isTrigger = false;
			GameObject.Find("WeakSpot1").GetComponent<SphereCollider>().isTrigger = true;
			GameObject.Find("WeakSpot2").GetComponent<SphereCollider>().isTrigger = true;
			GameObject.Find("WeakSpot1").GetComponent<Renderer>().material.color = Color.red;
			GameObject.Find("WeakSpot2").GetComponent<Renderer>().material.color = Color.red;
			closeTime = 1000000.0f;
		}
	}

	public bool isDead()
	{
		return dead;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			if (health > 0)
			{
				health--;
			}
			else
			{
				dead = true;
			}
		}
	}
}
