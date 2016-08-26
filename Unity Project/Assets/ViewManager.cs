using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Fungus;

public class ViewManager : MonoBehaviour {

	public SayDialog sayDialog;
	public Font msgFont;

	private int totalMessages;

	void Start () {
		//PrintMessage (sayDialog, "Teste01");
	}

	public void PrintSAYMessage(string text){
		Transform panel = sayDialog.gameObject.transform.Find ("Panel");
		GameObject mensagem = new GameObject ();
		mensagem.transform.parent = panel;

		Text msgTxt = mensagem.gameObject.AddComponent<Text> ();
		msgTxt.font = msgFont;
		msgTxt.fontSize = 25;
		msgTxt.text = text;
		msgTxt.rectTransform.sizeDelta = new Vector2 (350, 50);
		msgTxt.rectTransform.anchoredPosition = new Vector2 (0, 400 - totalMessages*80);
		totalMessages++;
	}

	public void UpdateSayDialog(SayDialog newDialog){
		
	}
}
