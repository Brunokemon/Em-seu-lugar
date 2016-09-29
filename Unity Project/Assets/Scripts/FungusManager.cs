using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;

[RequireComponent (typeof(SendMessage))]
public class FungusManager : MonoBehaviour
{
	private SendMessage sendMessageClass;

	public Flowchart currentFlowchart;
	private Block currentBlock;

	private string blockID;
	private int lastCommandID;

	public Block CurrentBlock {
		get {
			return currentBlock;
		}
		protected set {
			currentBlock = value;
		}
	}

	void Awake ()
	{
		sendMessageClass = gameObject.GetComponent<SendMessage> ();
	}

	void Start ()
	{
		blockID = currentFlowchart.GetStringVariable ("Block_ID");
		currentBlock = currentFlowchart.FindBlock (blockID);
		lastCommandID = currentBlock.commandList [0].itemId;
		SaveGame.SaveOrder (SaveGame.JuliaExecutedCommands, lastCommandID);
	}

	void Update ()
	{
		//Check if block has changed update variables and PrintMenuMessage
		if (blockID != currentFlowchart.GetStringVariable ("Block_ID")) {
			blockID = currentFlowchart.GetStringVariable ("Block_ID");
			CurrentBlock = currentFlowchart.GetExecutingBlocks () [0];
		}

		//if commandID has changed printSayMessage and update variables
		if (lastCommandID != currentBlock.activeCommand.itemId) {
			lastCommandID = currentBlock.activeCommand.itemId;

			//Pega o comando atual
			Say say = currentBlock.activeCommand as Say;
			//Verifica se é um Say
			if (say != null) {
				//Pega o Character referente ao Say
				sendMessageClass.SendSayMessage (CurrentBlock, say.character);
			}
			SaveGame.SaveOrder (SaveGame.JuliaExecutedCommands, lastCommandID);
		}
	}
}