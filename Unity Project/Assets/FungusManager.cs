using UnityEngine;
using System.Collections;
using Fungus;

public class FungusManager : MonoBehaviour {

	public Flowchart fcJulia;

	private Block currentBlock;
	private string blockID;

	public int lastCommandID;
	public int activeCommandID;
	public string blockName;
	public Teste01 TextFinder;

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

		blockName = currentBlock.blockName;
		activeCommandID = currentBlock.activeCommand.itemId;

		//Se o commandID mudou imprimir a mensagem do comando finalizado
		if (lastCommandID != currentBlock.activeCommand.itemId) {
			PrintMessage (currentBlock);
			lastCommandID = currentBlock.activeCommand.itemId;
		}

	}

	void PrintMessage (Block block){
		string line = TextFinder.FindCorectLine (block.activeCommand.itemId);
		print (line);
	}
}
