using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;


/*
Nos comentários estão os locais do código onde devem ser inseridos códigos para cada NPC do jogo.
Nos comentários em que está escrito "Modelo" está o modelo que deve ser reproduzido para cada Character, usando o termo NPC no lugar do nome do Character
*/
public class TextReader : MonoBehaviour
{

	//Modelo
	//public TextAsset localizationNPC;
	//public Dictionary<int, DictionaryClasses.SayValues> dialogsNPC = new Dictionary<int, DictionaryClasses.SayValues> ();

	//Texto exportado pelo próprio Fungus - Localization
	public TextAsset localizationJulia;
	public Dictionary<int, DictionaryClasses.SayValues> dialogsJulia = new Dictionary<int, DictionaryClasses.SayValues> ();

	void Awake ()
	{
		//Modelo
		//Dictionary para os SayDialogs do Flowchart de NPC
		//dialogsOutro = CreateDialogDictionary (localizationOutro);

		//Dictionary para os SayDialogs do Flowchart da Julia
		dialogsJulia = CreateDialogDictionary (localizationJulia);
	}

	//Retorna a frase contida no comando de ID commandID do Flowchart especificado
	public string FindPhrase (Flowchart flowchart, int commandID)
	{
		Dictionary<int, DictionaryClasses.SayValues> dialogDictionary = new Dictionary<int, DictionaryClasses.SayValues> ();
		DictionaryClasses.SayValues values;

		string flowchartName = flowchart.name.ToString ();
		//Pega o nome do flowchart e acessa o dictionary com as frases do flowchart específico
		switch (flowchartName) {
		//Modelo
		//case "OutroNPC_Flowchart":
		//	dialogDictionary = dialogsOutroNPC;
		//	break;
		case "Julia_Flowchart":
			dialogDictionary = dialogsJulia;
			break;
		}

		dialogDictionary.TryGetValue (commandID, out values);
		return values.text;
	}

	//Retorna um Dictionary com Key = commandID e Values SayValues.character e SayValues.text
	private Dictionary<int, DictionaryClasses.SayValues> CreateDialogDictionary (TextAsset dialogs)
	{
		Dictionary<int, DictionaryClasses.SayValues> dictionary = new Dictionary<int, DictionaryClasses.SayValues> ();
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

				//Evita keys repetidas
				if (dictionary.ContainsKey (commandID)) {
					continue;
				}

				//Salva o character e a frase daquele SayDialog
				DictionaryClasses.SayValues sayValues = new DictionaryClasses.SayValues ();
				sayValues.character = allLines [i].Split ("." [0]) [3];
				sayValues.text = allLines [i + 1];

				//print (commandID);
				//print (sayValues.text);
				//print (sayValues.character);

				//Salva a frase relacionada a chave commandID
				dictionary.Add (commandID, sayValues);
			}
		}
		return dictionary;
	}
}