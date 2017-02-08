using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : MonoBehaviour {

	[SerializeField]
	private float speed;

	[SerializeField]
	private GameObject missile;

	[SerializeField]
	private GameObject collision;

	private int pattern;
	private bool end;

	void Start () {

		pattern = 1;
		end = true;
		InvokeRepeating("coroutineFire", 5.0f, 5.0f);
	}
	
	void Update () {

		if (transform.position.z < 10.0f)
		{
			transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
		}

		if (GameObject.Find("WeakSpotMain").GetComponent<WeakSpotMain>().isDead() && end)
		{
			CancelInvoke();
			StartCoroutine(death());
			end = false;
		}
	}

	IEnumerator death()
	{
		Instantiate(collision, transform.position, transform.rotation);
		GameObject.Find("GameManager").GetComponent<GameManager>().getEnemy().Play();
		yield return new WaitForSecondsRealtime(0.5f);
		Instantiate(collision, transform.position + new Vector3(1.0f, 0.0f), transform.rotation);
		GameObject.Find("GameManager").GetComponent<GameManager>().getEnemy().Play();
		yield return new WaitForSecondsRealtime(0.5f);
		Instantiate(collision, transform.position + new Vector3(-1.0f, 0.0f), transform.rotation);
		GameObject.Find("GameManager").GetComponent<GameManager>().getEnemy().Play();
		yield return new WaitForSecondsRealtime(0.5f);
		Instantiate(collision, transform.position + new Vector3(-0.5f, 0.0f), transform.rotation);
		GameObject.Find("GameManager").GetComponent<GameManager>().getEnemy().Play();
		yield return new WaitForSecondsRealtime(0.5f);
		Instantiate(collision, transform.position + new Vector3(0.5f, 0.0f), transform.rotation);
		GameObject.Find("GameManager").GetComponent<GameManager>().getEnemy().Play();
		yield return new WaitForSecondsRealtime(1.5f);
		Instantiate(collision, transform.position, transform.rotation);
		GameObject.Find("GameManager").GetComponent<GameManager>().getEnemy().Play();
		GameObject.Find("Score").GetComponent<Score>().addScore(2000);
		Destroy(gameObject);
		GameObject.Find("End").GetComponent<Text>().text = "   You win!";
	}

	void coroutineFire()
	{
		StartCoroutine(fire());
	}

	IEnumerator fire()
	{
		Vector3 missileRot = new Vector3();
		GetComponent<AudioSource>().Play();

		switch (pattern)
		{
			case 1:
				for (float i = -0.1f; i <= 0.1f; i += 0.05f)
				{
					missileRot = new Vector3(i, -0.2f);

					Instantiate(missile, transform.position, Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(0.0f, 0.5f), Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(0.0f, 1.0f), Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(0.0f, -0.5f), Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(0.0f, -1.0f), Quaternion.LookRotation(Vector3.back + missileRot));
					yield return new WaitForSecondsRealtime(1.0f);
				}

				pattern++;
				break;

			case 2:
				for (float i = -0.125f; i >= -0.375f; i -= 0.0625f)
				{
					missileRot = new Vector3(0.0f, i);

					Instantiate(missile, transform.position, Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(0.5f, 0.0f), Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(1.0f, 0.0f), Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(-0.5f, 0.0f), Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(-1.0f, 0.0f), Quaternion.LookRotation(Vector3.back + missileRot));
					yield return new WaitForSecondsRealtime(1.0f);
				}

				pattern++;
				break;

			case 3:
				for (float i = 0.1f; i >= -0.1f; i -= 0.05f)
				{
					missileRot = new Vector3(i, -0.2f);

					Instantiate(missile, transform.position, Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(0.0f, 0.5f), Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(0.0f, 1.0f), Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(0.0f, -0.5f), Quaternion.LookRotation(Vector3.back + missileRot));
					Instantiate(missile, transform.position + new Vector3(0.0f, -1.0f), Quaternion.LookRotation(Vector3.back + missileRot));
					yield return new WaitForSecondsRealtime(1.0f);
				}

				pattern -= 2;
				break;
		}
	}
}
