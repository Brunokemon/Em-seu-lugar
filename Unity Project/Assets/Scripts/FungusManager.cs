using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;

[RequireComponent (typeof(ViewManager))]
[RequireComponent (typeof(TextReader))]
public class FungusManager : MonoBehaviour
{
	private ViewManager viewManager;
	private TextReader textReader;

	public Flowchart JuliaFlowchart;
	private Block currentBlock;

	private string blockName;
	private int commandID;

	float messageTimer = 0f;

	void Start ()
	{
		viewManager = gameObject.GetComponent<ViewManager> ();
		textReader = gameObject.GetComponent<TextReader> ();

		//Este comando pega o nome do bloco em execucao
		blockName = JuliaFlowchart.GetExecutingBlocks () [0].blockName;

		//Encontra o block
		currentBlock = JuliaFlowchart.FindBlock (blockName);
		commandID = currentBlock.commandList [0].itemId;

		//SaveGame.SaveOrder (SaveGame.JuliaExecutedCommands, commandID);
	}

	void Update ()
	{
		//Check if block has changed update variables
		if (JuliaFlowchart.GetExecutingBlocks() != null) {
			if (blockName != JuliaFlowchart.GetExecutingBlocks () [0].blockName) {
				blockName = JuliaFlowchart.GetExecutingBlocks () [0].blockName;
				currentBlock = JuliaFlowchart.FindBlock (blockName);
				messageTimer = 0f;
			}
		}

		//if commandID has changed printSayMessage from last command and update variables
		if (currentBlock.activeCommand != null && commandID != currentBlock.activeCommand.itemId) {

			//Mostra o comando anterior para dar tempo de o SayDialog do Player digitar antes de mostrar a mensagem no Histórico do Chat
			if (textReader.dialogsJulia.ContainsKey (commandID)) {
				string phrase = textReader.FindPhrase (JuliaFlowchart, commandID);
				messageTimer += phrase.Length * 0.08f;
				StartCoroutine (CallSayMessageWithDelay(phrase,textReader.dialogsJulia[commandID].character, messageTimer));
			}

			commandID = currentBlock.activeCommand.itemId;

			/*
			//Mostra o comando atual
			Say say = currentBlock.activeCommand as Say;
			//Verifica se é um Say
			if (say != null) {
				string phrase = textReader.FindPhrase (JuliaFlowchart, commandID);
				viewManager.PrintMessage (phrase, say.character.name);
			}
			*/

			//SaveGame.SaveOrder (SaveGame.JuliaExecutedCommands, commandID);
		}
	}

	IEnumerator CallSayMessageWithDelay (string text, string character, float delay){
		//o Character está vindo com um character "invisivel" mais. Devemos remover a ultima letra para garantir que os nomes estejam corretos
		string correctName = character.Substring(0,character.Length -1);

		if (correctName.ToUpper() != "PLAYER") {
			yield return new WaitForSeconds (delay);
		}
		viewManager.PrintMessage (text, correctName);
	}
}