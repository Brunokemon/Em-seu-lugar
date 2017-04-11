using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using System.Linq;

public class ViewManager : MonoBehaviour
{
	public GameObject ChatCanvas;

	//Recebe mensagem do Say e verifica se eh uma mensagem de texto, imagem ou audio e chama a funcao apropriada
	public void PrintMessage (string sayMessage, string character)
	{
		//Aqui se assume que nao havera nenhuma sayMessage nula
		string type = sayMessage.Split (" " [0]) [0];
		switch (type) {
		case "picture":
			string pictureFile = sayMessage.Split (" " [0]) [1];
			//PrintPictureMessage (pictureFile, character);
			break;
		case "audio":
			string audioFile = sayMessage.Split (" " [0]) [1];
			//PrintAudioMessage (audioFile, character);
			break;
		default:
			PrintTextMessage (sayMessage, character);
			break;
		}
	}

	//Cria a mensagem de texto
	private void PrintTextMessage (string text, string character)
	{
		if (character != null) {
			if (character.ToUpper () == "PLAYER") {
				ChatCanvas.GetComponent<ChatController> ().AddBubbleRight (text);
			} else {
				ChatCanvas.GetComponent<ChatController> ().AddBubbleLeft (text);
			}
		}
	}
}