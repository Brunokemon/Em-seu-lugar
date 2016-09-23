using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;

[RequireComponent (typeof(TextReader))]
[RequireComponent (typeof(ViewManager))]
public class FungusManager : MonoBehaviour
{
	private TextReader TextReader;
	private ViewManager viewManager;

	public Flowchart currentFlowchart;
	private Block currentBlock;
	private List<Say> allSayDialogs = new List<Say> ();

	public string blockID;
	public int lastCommandID;

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
		TextReader = gameObject.GetComponent<TextReader> ();
		viewManager = gameObject.GetComponent<ViewManager> ();
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
			currentBlock = currentFlowchart.GetExecutingBlocks () [0];

			//change all say's wait for click to false
			print ("novo bloco: " + blockID);
		}

		//if commandID has changed printSayMessage and update variables
		if (lastCommandID != currentBlock.activeCommand.itemId) {
			lastCommandID = currentBlock.activeCommand.itemId;

			//Pega o comando atual
			Say say = currentBlock.activeCommand as Say;
			//Verifica se é um Say
			if (say != null) {
				//Pega o Character referente ao Say

				SendSAYMessage (currentBlock, say.character);
			}
			SaveOrder (blockID, lastCommandID);
		}
	}

	void SendSAYMessage (Block block, Character character)
	{
		string line = TextReader.FindCorrectLine (block.activeCommand.itemId);
		viewManager.PrintSAYMessage (line, character);
	}
}

//Classe para usar com Dictionary para guardar valores (frase e character) de cada SayDialog
class SaveValues
{
	public int flowchart { get; set; }

	public string block { get; set; }

	public int command { get; set; }
}