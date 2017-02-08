using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Power_Up : MonoBehaviour {
	
	Vector3 playerPos;

	void Start () {

		
	}
	
	void Update () {


	}

	public void coroutinePowerUp()
	{
		StartCoroutine(power_up());
	}

	public void coroutinePowerDown()
	{
		StartCoroutine(power_down());
	}

	IEnumerator power_up()
	{
		GetComponent<Text>().text = "Power up!";
		yield return new WaitForSecondsRealtime(2.0f);
		GetComponent<Text>().text = "";
	}

	IEnumerator power_down()
	{
		GetComponent<Text>().text = "Power down!";
		yield return new WaitForSecondsRealtime(2.0f);
		GetComponent<Text>().text = "";
	}
}
