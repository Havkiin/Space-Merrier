using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class Play_Again : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	private bool active;

	void Start () {
	
		GetComponent<Text>().color = Color.white;
	}
	
	void Update () {
		
		if (GameObject.Find("End").GetComponent<Text>().text != "")
		{
			active = true;
			GetComponent<Text>().text = "Play again?";
			Cursor.visible = true;

			if (GameObject.Find("Player") != null)
			{
				GameObject.Find("Player").GetComponent<Player>().enabled = false;
			}

			if (GameObject.Find("Aim") != null)
			{
				Destroy(GameObject.Find("Aim").gameObject);
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (active)
		{
			SceneManager.LoadScene("Game");
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (active)
		{
			GetComponent<Text>().color = Color.red;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (active)
		{
			GetComponent<Text>().color = Color.white;
		}
	}
}
