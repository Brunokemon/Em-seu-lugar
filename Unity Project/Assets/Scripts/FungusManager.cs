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

	//Modelo para ser usado em cada flowchart diferente
	//public Flowchart OtherNPCFlowchart;
	public Flowchart JuliaFlowchart;

	private Flowchart currentFlowchart;
	private Block currentBlock;
	private string blockName;
	private int commandID;

	float messageTimer = 0f;

	public GameObject isTypingIcon;

	//Lista para dizer quantas mensagens ainda estão sendo enviadas
	List<float> runningCorroutines = new List<float> ();

	void Start ()
	{
		viewManager = gameObject.GetComponent<ViewManager> ();
		textReader = gameObject.GetComponent<TextReader> ();

		//Este comando pega o nome do bloco em execucao
		blockName = currentFlowchart.GetExecutingBlocks () [0].blockName;

		//Encontra o block
		currentBlock = currentFlowchart.FindBlock (blockName);
		commandID = currentBlock.commandList [0].itemId;

		//SaveGame.SaveOrder (SaveGame.JuliaExecutedCommands, commandID);
	}

	void Update ()
	{
		this.ActiveFlowchart ();		
		//Check if block has changed update variables
		if (currentFlowchart.GetExecutingBlocks () != null) {
			if (blockName != currentFlowchart.GetExecutingBlocks () [0].blockName) {
				blockName = currentFlowchart.GetExecutingBlocks () [0].blockName;
				currentBlock = currentFlowchart.FindBlock (blockName);
				messageTimer = 0f;
			}
		}

		//if commandID has changed printSayMessage from last command and update variables
		if (currentBlock.activeCommand != null && commandID != currentBlock.activeCommand.itemId) {

			//Mostra o comando anterior para dar tempo de o SayDialog do Player digitar antes de mostrar a mensagem no Histórico do Chat
			if (textReader.dialogsJulia.ContainsKey (commandID)) {
				string phrase = textReader.FindPhrase (currentFlowchart, commandID);
				messageTimer += phrase.Length * 0.08f;
				StartCoroutine (CallSayMessageWithDelay (phrase, textReader.dialogsJulia [commandID].character, messageTimer));
			}

			commandID = currentBlock.activeCommand.itemId;

			/*

			//Pega o comando atual
			Command command = currentBlock.activeCommand;
			//Verifica se é um Say
			if (command.GetType ().Name == "Say") {
				Say commandSay = command as Say;
				//string phrase = textReader.FindPhrase (JuliaFlowchart, commandID);
				//viewManager.PrintMessage (phrase, (Say)say.character.name);
			}

			//Verifica se é um Wait
			if (command.GetType ().Name == "Wait") {
				Wait commandWait = command as Wait;
				//duracao do Wait Command
				float duration = commandWait._duration;
			}

			SaveGame.SaveOrder (currentFlowchart.name, commandID);

			*/
		}
	}

	IEnumerator CallSayMessageWithDelay (string text, string character, float delay)
	{
		//o Character está vindo com um character "invisivel" mais. Devemos remover a ultima letra para garantir que os nomes estejam corretos
		string correctName = character.Substring (0, character.Length - 1);

		runningCorroutines.Add (delay);

		if (correctName.ToUpper () != "PLAYER") {
			isTypingIcon.SetActive (true);	
			yield return new WaitForSeconds (delay);

			//Se está é a ultima corroitine na lista, então desativar o IsTyping
			if (runningCorroutines.Count == 1) {
				isTypingIcon.SetActive (false);
			}
		}
		viewManager.PrintMessage (text, correctName);
		runningCorroutines.Remove (delay);
	}

	void ActiveFlowchart ()
	{
		//Modelo para ser usado em todos os flowcharts
		//if (OtherNPCFlowchart.isActiveAndEnabled)
		//	currentFlowchart = OtherNPCFlowchart;

		if (JuliaFlowchart.isActiveAndEnabled)
			currentFlowchart = JuliaFlowchart;
	}
}