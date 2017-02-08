using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	private float speed;

	[SerializeField]
	private GameObject missile;

	[SerializeField]
	private GameObject death;

	private Camera cam;
	private float boundX;
	private float boundY;
	private int power;
	private float missileSep;
	private float deathTimer;

	void Start () {

		cam = Camera.main;
		power = 0;
		missileSep = 0.5f;
		deathTimer = 1000000.0f;

		float distance = Mathf.Abs(cam.transform.position.z - transform.position.z);
		boundY = distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad) + cam.transform.position.y;
		boundX = boundY * cam.aspect;
	}
	
	void Update () {

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if (horizontal != 0 || vertical != 0)
		{
			transform.Translate(new Vector3(horizontal, vertical) * speed * Time.deltaTime);
		}

		if (Input.GetButtonDown("Fire1"))
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 15.0f;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
			GetComponent<AudioSource>().Play();

			if (power == 0)
			{
				var missile = Instantiate(this.missile, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), Quaternion.identity) as GameObject;
				missile.transform.LookAt(mousePos);
			}
			else if (power == 1)
			{
				var missile1 = Instantiate(missile, new Vector3(transform.position.x + missileSep / 2.0f, transform.position.y, transform.position.z + 0.5f), Quaternion.identity) as GameObject;
				missile1.transform.LookAt(mousePos + new Vector3(missileSep / 2.0f, 0.0f));
				var missile2 = Instantiate(missile, new Vector3(transform.position.x - missileSep / 2.0f, transform.position.y, transform.position.z + 0.5f), Quaternion.identity) as GameObject;
				missile2.transform.LookAt(mousePos - new Vector3(missileSep / 2.0f, 0.0f));
			}
			else
			{
				var missile1 = Instantiate(missile, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), Quaternion.identity) as GameObject;
				missile1.transform.LookAt(mousePos);
				var missile2 = Instantiate(missile, new Vector3(transform.position.x + missileSep, transform.position.y, transform.position.z + 0.5f), Quaternion.identity) as GameObject;
				missile2.transform.LookAt(mousePos + new Vector3(missileSep, 0.0f));
				var missile3 = Instantiate(missile, new Vector3(transform.position.x - missileSep, transform.position.y, transform.position.z + 0.5f), Quaternion.identity) as GameObject;
				missile3.transform.LookAt(mousePos - new Vector3(missileSep, 0.0f));
			}
		}

		if (transform.position.y < 0.01f || transform.position.y > boundY)
		{
			transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0.01f, boundY), transform.position.z);
		}

		if (transform.position.x < -boundX * 0.5f || transform.position.x > boundX * 0.5f)
		{
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundX * 0.5f, boundX * 0.5f), transform.position.y, transform.position.z);
		}

		if (GameObject.Find("WeakSpotMain") != null && GameObject.Find("WeakSpotMain").GetComponent<WeakSpotMain>().isDead())
		{
			GetComponent<MeshCollider>().isTrigger = false;
		}
	}

	public GameObject getMissile()
	{
		return missile;
	}

	public void powerUp ()
	{
		if (power < 2)
		{
			power++;
			GameObject.Find("Power_Up").GetComponent<Power_Up>().coroutinePowerUp();
			GameObject.Find("GameManager").GetComponent<GameManager>().getPowerUp().Play();
		}
	}

	public void powerDown ()
	{
		if (power > 0)
		{
			power--;
			GameObject.Find("Power_Up").GetComponent<Power_Up>().coroutinePowerDown();
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Ground")
		{
			deathTimer = Time.timeSinceLevelLoad;
		}
		else
		{
			if (power > 0)
			{
				powerDown();
				StartCoroutine(damage());
				GameObject.Find("GameManager").GetComponent<GameManager>().getPlayerHit().Play();
			}
			else
			{
				Instantiate(death, transform.position, transform.rotation);
				GameObject.Find("End").GetComponent<Text>().text = "Game Over";
				GameObject.Find("GameManager").GetComponent<GameManager>().getPlayer().Play();
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (col.tag == "Ground")
		{
			StartCoroutine(groundCollide());
		}

		if (Time.timeSinceLevelLoad - deathTimer > 3.0f)
		{
			Instantiate(death, transform.position, transform.rotation);
			GameObject.Find("End").GetComponent<Text>().text = "Game Over";
			GameObject.Find("GameManager").GetComponent<GameManager>().getPlayer().Play();
			Destroy(gameObject);
		}
	}

	IEnumerator groundCollide()
	{
			Vector3 shake = new Vector3(0.0f, 0.05f);

			Camera.main.transform.position -= shake;
			yield return new WaitForSecondsRealtime(0.1f);
			Camera.main.transform.position += shake;
	}

	IEnumerator damage()
	{
		GetComponent<MeshCollider>().enabled = false;

		for (int i = 0; i < 3; i++)
		{
			GetComponent<MeshRenderer>().enabled = false;
			yield return new WaitForSecondsRealtime(0.5f);
			GetComponent<MeshRenderer>().enabled = true;
			yield return new WaitForSecondsRealtime(0.5f);
		}

		GetComponent<MeshCollider>().enabled = true;
	}
}
