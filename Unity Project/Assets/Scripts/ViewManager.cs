using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using System.Linq;

public class ViewManager : MonoBehaviour
{

	public SayDialog sayDialog;
	public Font msgFont;

	private int totalMessages;
	private float scrollSpeed = 2f;

	List<GameObject> allMessages = new List<GameObject>();

	void Awake () {
		PrintSAYMessage ("                       ", null);
	}
	void Update ()
	{
		//SCRLL utilizando as setas do teclado
		if (Input.GetKey(KeyCode.UpArrow)) {
			foreach (GameObject go in allMessages) {
				go.transform.Translate (-Vector3.up * scrollSpeed);
			}
		}
		else if (Input.GetKey(KeyCode.DownArrow)) {
			foreach (GameObject go in allMessages) {
				go.transform.Translate (Vector3.up * scrollSpeed);
			}
		}
	}

	//Creates a new gameObject inside the current sayDialog with the text sent
	public void PrintSAYMessage (string text, Character character)
	{
		if (text != null) {
			Transform panel = sayDialog.gameObject.transform.Find ("Panel");
			Transform mask = panel.gameObject.transform.Find ("HistoryMask");
			Transform grid = mask.gameObject.transform.Find ("Grid");
			GameObject mensagem = new GameObject ();
			mensagem.transform.parent = grid;

			Text msgTxt = mensagem.gameObject.AddComponent<Text> ();
			msgTxt.font = msgFont;
			msgTxt.fontSize = 25;
			msgTxt.text = text;
			//msgTxt.rectTransform.sizeDelta = new Vector2 (350, 50);

			LayoutElement layoutEle = mensagem.gameObject.AddComponent<LayoutElement> ();
			layoutEle.flexibleWidth = 1;
			layoutEle.preferredWidth = 350;
			layoutEle.flexibleHeight = 1;
			layoutEle.minHeight = 100;



			if (allMessages.Count == 0) {
				msgTxt.rectTransform.anchoredPosition = new Vector2 (0, 0);
			} else {
				//Gets the anchoredPosition of the last object in the allMessages list, and add +80 to it's Y position
				msgTxt.rectTransform.anchoredPosition = allMessages.Last ().transform.GetComponentInChildren<RectTransform> ().anchoredPosition + (-Vector2.up * 80);
			}

			if (character.nameText == "Player") {
				msgTxt.color = Color.white;
			} else {
				msgTxt.color = Color.yellow;
			}

			totalMessages++;
			allMessages.Add (mensagem);
		}
	}

	public void UpdateSayDialog (SayDialog newDialog)
	{
		
	}
}
