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

	//IDs dos comandos que já foram executados
	List<SaveValues> savedID = new List<SaveValues> ();

	void SaveOrder (string block, int command)
	{
		SaveValues save = new SaveValues ();
		save.block = block;
		save.command = command;
		save.flowchart = 0;
		savedID.Add (save);
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
		SaveOrder (blockID, lastCommandID);

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
				sendMessageClass.SendSayMessage(CurrentBlock,say.character);
			}
			SaveOrder (blockID, lastCommandID);
		}
	}


}

//Classe para usar com Dictionary para guardar valores (frase e character) de cada SayDialog
class SaveValues
{
	public int flowchart { get; set; }

	public string block { get; set; }

	public int command { get; set; }
}