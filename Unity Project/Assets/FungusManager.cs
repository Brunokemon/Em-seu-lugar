using UnityEngine;
using System.Collections;
using Fungus;

[RequireComponent (typeof(TextReader))]
[RequireComponent (typeof(ViewManager))]
public class FungusManager : MonoBehaviour {

	private TextReader TextReader;
	private ViewManager viewManager;

	public Flowchart currentFlowchart;
	private Block currentBlock;

	public string blockID;
	public int lastCommandID;



	void Awake (){
		TextReader = gameObject.GetComponent<TextReader> ();
		viewManager = gameObject.GetComponent<ViewManager> ();
	}

	void Start(){
		blockID = currentFlowchart.GetStringVariable("Block_ID");
		currentBlock = currentFlowchart.FindBlock (blockID);
		lastCommandID = currentBlock.commandList [0].itemId;
	}

	void Update(){
		
		if (blockID != currentFlowchart.GetStringVariable("Block_ID")){
			blockID = currentFlowchart.GetStringVariable("Block_ID");
			currentBlock = currentFlowchart.GetExecutingBlocks () [0];
			print ("novo bloco: "+ blockID);
		}

		//Se o commandID mudou imprimir a mensagem do comando finalizado
		if (lastCommandID != currentBlock.activeCommand.itemId) {
			SendSAYMessage (currentBlock);
			lastCommandID = currentBlock.activeCommand.itemId;
		}

	}

	void SendSAYMessage (Block block){
		string line = TextReader.FindCorectLine (block.activeCommand.itemId, "SAY");
		print (line);
		viewManager.PrintSAYMessage (line);
	}
}
