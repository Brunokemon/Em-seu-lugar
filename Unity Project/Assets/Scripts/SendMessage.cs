using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;

[RequireComponent (typeof(FungusManager))]
[RequireComponent (typeof(ViewManager))]
[RequireComponent (typeof(TextReader))]

public class SendMessage : MonoBehaviour {

	private FungusManager fungusManager;
	private ViewManager viewManager;
	private TextReader textReader;

	void Awake (){
		viewManager = gameObject.GetComponent<ViewManager> ();
		fungusManager = gameObject.GetComponent<FungusManager> ();
		textReader = gameObject.GetComponent<TextReader> ();
	}


	public void SendSayMessage (Block block, Character character)	{
		string line = textReader.FindCorrectLine (block.activeCommand.itemId);
		viewManager.PrintSAYMessage (line, character);
	}
}
