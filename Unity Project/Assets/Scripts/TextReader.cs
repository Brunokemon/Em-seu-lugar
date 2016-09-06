using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;


public class TextReader : MonoBehaviour
{

	//Texto exportado pelo próprio Fungus - Localization
	public TextAsset dialogs01;
	//Lista com todas as linhas
	private List<string> allLines = new List<string> ();
	Dictionary<int, string> dialogsJulia =
		new Dictionary<int, string> ();

	void Awake ()
	{
		dialogsJulia = CreateDialogDictionary (dialogs01);
	}

	public string FindCorrectLine (int commandID)
	{
		string sayDialog;
		dialogsJulia.TryGetValue (commandID, out sayDialog);
		return sayDialog;
	}

	//Retorna um dicionario com Key = commandID e Value = frase de um TextAsset exportado pelo Fungus contendo os SayDialogs
	private Dictionary<int, string> CreateDialogDictionary (TextAsset dialogs)
	{
		Dictionary<int, string> dictionary = new Dictionary<int, string> ();
		//Salva o texto exportado pelo Fungus em uma única string
		string bigString = dialogs.text;
		//Quebra a string em linhas e salva cada linha em uma posição do array
		List<string> allLines = new List<string> ();
		allLines.AddRange (bigString.Split ("\n" [0]));
		//Itera a cada commandID e frase do allLines
		for (int i = 0; i < allLines.Count - 1; i = i + 3) {
			//Verifica se é um SayDialog
			if (allLines [i].Split ("." [0]) [0] == "#SAY") {
				//Salva o commandID daquele SayDialog
				string stringCommandID = allLines [i].Split ("." [0]) [2];
				int commandID = int.Parse (stringCommandID);
				//Salva a frase relacionada a chave commandID
				dictionary.Add (commandID, allLines [i + 1]);
			}
		}
		return dictionary;
	}
}
