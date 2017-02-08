using UnityEngine;
using System.Collections;

public class Wave1Spawner : MonoBehaviour {

	[SerializeField]
	private GameObject enemy1;

	[SerializeField]
	private GameObject boss;

	private GameObject[] enemies;
	private int enemyCount;
	private bool powerUp;
	private bool power1;
	private bool power2;
	private bool bossTime;

	void Start () {

		enemies = new GameObject[10];
		enemyCount = 0;
		powerUp = false;
		power1 = true;
		power2 = true;
		bossTime = true;

		InvokeRepeating("spawnWave", 0.0f, 15.0f);
	}
	
	void Update () {

		if (Time.timeSinceLevelLoad > 29.0f || GameObject.Find("Player") == null)
		{
			CancelInvoke();
		}

		if (enemies[0] == null && enemies[1] == null && enemies[2] == null && enemies[3] == null && enemies[4] == null && power1)
		{
			powerUp = true;
			power1 = false;
		}

		if (enemies[5] == null && enemies[6] == null && enemies[8] == null && enemies[8] == null && enemies[9] == null && power2 && Time.timeSinceLevelLoad > 20.0f)
		{
			powerUp = true;
			power2 = false;
		}

		if (powerUp && GameObject.Find("Player") != null)
		{
			GameObject.Find("Player").GetComponent<Player>().powerUp();
			powerUp = false;
		}

		if ((Time.timeSinceLevelLoad > 50.0f || !GameObject.Find("Wave2Spawner").GetComponent<Wave2Spawner>().getPower2()) && bossTime && GameObject.Find("Player") != null)
		{
			Instantiate(boss, transform.position + new Vector3(0.0f, -1.0f), Quaternion.Euler(-25.0f, 0.0f, 0.0f));
			bossTime = false;
		}
	}

	void spawnWave ()
	{
		for (int i = -4; i <= 4; i += 2)
		{
			if (enemyCount < 5)
			{
				enemies[enemyCount] = (GameObject)Instantiate(enemy1, transform.position + new Vector3(i, 0.0f, -Mathf.Abs(i)), Quaternion.Euler(new Vector3(10.0f, 180.0f, 0.0f)));
			}
			else
			{
				enemies[enemyCount] = (GameObject)Instantiate(enemy1, transform.position + new Vector3(i, -2.0f, -Mathf.Abs(i)), Quaternion.Euler(new Vector3(10.0f, 180.0f, 0.0f)));
			}
			

			if (Mathf.Abs(i) == 4)
			{
				enemies[enemyCount].GetComponent<Enemy1>().setStopPosition(12.0f);
			}
			else if (Mathf.Abs(i) == 2)
			{
				enemies[enemyCount].GetComponent<Enemy1>().setStopPosition(11.0f);
			}
			else
			{
				enemies[enemyCount].GetComponent<Enemy1>().setStopPosition(10.0f);
			}
			
			enemyCount++;
		}
	}
}
