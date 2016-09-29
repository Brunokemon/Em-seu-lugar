using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;

public class TextReader : MonoBehaviour
{
	//Texto exportado pelo próprio Fungus - Localization
	public TextAsset dialogs01;
	Dictionary<int, SayValues> dialogsJulia = new Dictionary<int, SayValues> ();

	void Awake ()
	{
		//Cria um dictionary para os SayDialogs do Flowchart da Julia
		dialogsJulia = CreateDialogDictionary (dialogs01);
	}

	//Retorna a frase contida no dictionary do Flowchart Julia
	public string FindCorrectLine (int commandID)
	{
		SayValues values;
		dialogsJulia.TryGetValue (commandID, out values);
		return values.text;
	}

	//Retorna um Dictionary com Key = commandID e Values SayValues.character e SayValues.text
	private Dictionary<int, SayValues> CreateDialogDictionary (TextAsset dialogs)
	{
		Dictionary<int, SayValues> dictionary = new Dictionary<int, SayValues> ();
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
				//Salva o character e a frase daquele SayDialog
				SayValues sayValues = new SayValues ();
				sayValues.character = allLines [i].Split ("." [0]) [3];
				sayValues.text = allLines [i + 1];
				//Salva a frase relacionada a chave commandID
				dictionary.Add (commandID, sayValues);
			}
		}
		return dictionary;
	}
}