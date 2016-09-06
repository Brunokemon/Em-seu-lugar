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
			SendSAYMessage (currentBlock, "Julia");
			lastCommandID = currentBlock.activeCommand.itemId;
		}
	}

	void SendSAYMessage (Block block, string character)
	{
		string line = TextReader.FindCorectLine (block.activeCommand.itemId, character);
		//print (line);
		viewManager.PrintSAYMessage (line, character);
	}
}