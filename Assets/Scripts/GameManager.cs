using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private AudioSource[] audios;
	private AudioSource player, enemy, playerHit, powerUp;

	void Start () {

		Cursor.visible = false;
		audios = GetComponents<AudioSource>();
		player = audios[1];
		enemy = audios[2];
		playerHit = audios[3];
		powerUp = audios[4];
	}
	
	void Update () {
	
	}

	public AudioSource getPlayer()
	{
		return player;
	}

	public AudioSource getEnemy()
	{
		return enemy;
	}

	public AudioSource getPlayerHit()
	{
		return playerHit;
	}

	public AudioSource getPowerUp()
	{
		return powerUp;
	}
}
