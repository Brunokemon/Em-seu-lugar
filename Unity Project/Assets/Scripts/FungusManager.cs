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

	public Flowchart flowchart;
	public GameObject chatController;
	private Block currentBlock;
	private string blockName;
	private int commandID;

	public float messageTimer = 0f;

	//Lista para dizer quantas mensagens ainda estão sendo enviadas
	private List<float> runningCorroutines = new List<float> ();

	void Start ()
	{
		viewManager = gameObject.GetComponent<ViewManager> ();
		textReader = gameObject.GetComponent<TextReader> ();
	}

	void Update ()
	{
		//if has changed block
		if (flowchart.GetExecutingBlocks () != null && flowchart.GetExecutingBlocks ().Count != 0 && blockName != flowchart.GetExecutingBlocks () [0].blockName) {

			blockName = flowchart.GetExecutingBlocks () [0].blockName;
			currentBlock = flowchart.FindBlock (blockName);

			messageTimer = 0f;
		}

		//if commandID has changed printSayMessage from last command and update
		if (currentBlock.activeCommand != null && commandID != currentBlock.activeCommand.itemId) {

			//Mostra o comando anterior para dar tempo de o SayDialog do Player digitar antes de mostrar a mensagem no Histórico do Chat
			if (textReader.dialogsJulia.ContainsKey (commandID)) {
				string phrase = textReader.FindPhrase (flowchart, commandID);
				messageTimer += phrase.Length * 0.08f;
				StartCoroutine (CallSayMessageWithDelay (phrase, textReader.dialogsJulia [commandID].character, messageTimer));
			}

			//updates to actual commandID
			commandID = currentBlock.activeCommand.itemId;
		}
	}

	public void MenuOptions (string option1, string block1, string option2, string block2)
	{
		chatController.GetComponent<ChatController> ().ActivateOptions (option1, option2);

	}

	private IEnumerator CallSayMessageWithDelay (string text, string character, float delay)
	{
		//o Character está vindo com um character "invisivel" mais. Devemos remover a ultima letra para garantir que os nomes estejam corretos
		string correctName = character.Substring (0, character.Length - 1);

		runningCorroutines.Add (delay);

		if (correctName.ToUpper () != "PLAYER")
			yield return new WaitForSeconds (delay);

		viewManager.PrintMessage (text, correctName);
		runningCorroutines.Remove (delay);
	}
}