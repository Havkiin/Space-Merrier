using UnityEngine;
using System.Collections;

public class WeakSpot : MonoBehaviour {

	private int health;
	private bool down;

	void Start () {

		health = 5;
		down = false;
	}

	void Update () {
	
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
				down = true;
				health = 5;
				GetComponent<Renderer>().material.color = Color.grey;
				GetComponent<SphereCollider>().isTrigger = false;
			}
		}
	}

	public bool isDown()
	{
		return down;
	}

	public void restore()
	{
		down = false;
	}
}
