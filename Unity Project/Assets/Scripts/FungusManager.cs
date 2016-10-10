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
		//Check if block has changed update variables and PrintMenuMessage
		if (blockName != JuliaFlowchart.GetExecutingBlocks () [0].blockName) {
			blockName = JuliaFlowchart.GetExecutingBlocks () [0].blockName;
			currentBlock = JuliaFlowchart.FindBlock (blockName);
		}

		//if commandID has changed printSayMessage from last command and update variables
		if (currentBlock.activeCommand != null && commandID != currentBlock.activeCommand.itemId) {

			//Mostra o comando anterior para dar tempo de o SayDialog do Player digitar antes de mostrar a mensagem no Histórico do Chat
			if (textReader.dialogsJulia.ContainsKey (commandID)) {
				string phrase = textReader.FindPhrase (JuliaFlowchart, commandID);
				viewManager.PrintMessage (phrase, textReader.dialogsJulia [commandID].character);	
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
}