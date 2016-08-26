using UnityEngine;
using System.Collections;
using Fungus;

[RequireComponent (typeof(TextReader))]
[RequireComponent (typeof(ViewManager))]
public class FungusManager : MonoBehaviour {

	public Flowchart fcJulia;

	private Block currentBlock;
	private string blockID;
	public int lastCommandID;

	public TextReader TextReader;
	public ViewManager viewManager;

	void Awake (){
		TextReader = gameObject.GetComponent<TextReader> ();
		viewManager = gameObject.GetComponent<ViewManager> ();
	}

	void Start(){
		blockID = fcJulia.GetStringVariable("Block_ID");
		currentBlock = fcJulia.FindBlock (blockID);
		lastCommandID = currentBlock.commandList [0].itemId;
	}

	void Update(){
		
		if (blockID != fcJulia.GetStringVariable("Block_ID")){
			blockID = fcJulia.GetStringVariable("Block_ID");
			currentBlock = fcJulia.GetExecutingBlocks () [0];
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
		viewManager.PrintMessage (line);
	}
}
