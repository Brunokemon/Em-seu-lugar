using UnityEngine;
using System.Collections;
using Fungus;

[RequireComponent (typeof(TextReader))]
[RequireComponent (typeof(ViewManager))]
public class FungusManager : MonoBehaviour
{

	private TextReader TextReader;
	private ViewManager viewManager;

	public Flowchart currentFlowchart;
	private Block currentBlock;

	public string blockID;
	public int lastCommandID;

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
	}

	void Update ()
	{
		//Check if block has changed update variables and PrintMenuMessage
		if (blockID != currentFlowchart.GetStringVariable ("Block_ID")) {
			blockID = currentFlowchart.GetStringVariable ("Block_ID");
			currentBlock = currentFlowchart.GetExecutingBlocks () [0];
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
		}
	}

	void SendSAYMessage (Block block, Character character)
	{
		string line = TextReader.FindCorrectLine (block.activeCommand.itemId);
		viewManager.PrintSAYMessage (line, character);
	}
}