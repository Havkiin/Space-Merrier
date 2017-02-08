using UnityEngine;
using System.Collections;

public class Wave2Spawner : MonoBehaviour {

	[SerializeField]
	private GameObject enemy2;

	private GameObject[] enemies;
	private int enemyCount;
	private bool powerUp;
	private bool power1;
	private bool power2;

	void Start () {

		enemies = new GameObject[10];
		enemyCount = 0;
		powerUp = false;
		power1 = true;
		power2 = true;

		InvokeRepeating("coroutineSpawnWave", 7.5f, 15.0f);
	}
	
	void Update () {
		
		if (Time.timeSinceLevelLoad > 29.0f || GameObject.Find("Player") == null)
		{
			CancelInvoke();
		}

		if (enemies[0] == null && enemies[1] == null && enemies[2] == null && enemies[3] == null && enemies[4] == null && power1 && Time.timeSinceLevelLoad > 8f)
		{
			powerUp = true;
			power1 = false;
		}

		if (enemies[5] == null && enemies[6] == null && enemies[8] == null && enemies[8] == null && enemies[9] == null && power2 && Time.timeSinceLevelLoad > 25.0f)
		{
			powerUp = true;
			power2 = false;
		}

		if (powerUp && GameObject.Find("Player") != null)
		{
			GameObject.Find("Player").GetComponent<Player>().powerUp();
			powerUp = false;
		}
	}

	public bool getPower2 ()
	{
		return power2;
	}

	void coroutineSpawnWave ()
	{
		StartCoroutine(spawnWave());
	}

	IEnumerator spawnWave ()
	{
		for (int i = 0; i < 5; i++)
		{
			enemies[enemyCount] = (GameObject)Instantiate(enemy2, transform.position, Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f)));

			if (enemyCount < 5)
			{
				enemies[enemyCount].GetComponent<Enemy2>().setStopPosition(-2 * enemyCount + 4);
			}
			else
			{
				enemies[enemyCount].GetComponent<Enemy2>().setStopPosition(-2 * enemyCount + 14);
			}

			enemyCount++;
			yield return new WaitForSecondsRealtime(1.0f);
		}
	}
}
