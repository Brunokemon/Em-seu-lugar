using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using System.Linq;

public class ViewManager : MonoBehaviour
{
	//Grid onde sao mostradas as mensagens do chat
	//Usado nas funcoes PrintAudioMessage, PrintPictureMessage e PrintTextMessage
	public Transform grid;
	public Transform historyMask;

	//Recebe mensagem do Say e verifica se eh uma mensagem de texto, imagem ou audio e chama a funcao apropriada
	public void PrintMessage (string sayMessage, string character)
	{
		//Aqui se assume que nao havera nenhuma sayMessage nula
		string type = sayMessage.Split (" " [0]) [0];
		switch (type) {
		case "picture":
			string pictureFile = sayMessage.Split (" " [0]) [1];
			PrintPictureMessage (pictureFile, character);
			break;
		case "audio":
			string audioFile = sayMessage.Split (" " [0]) [1];
			PrintAudioMessage (audioFile, character);
			break;
		default:
			if (character == "Player") {
				//WaitWhile()
			}
			PrintTextMessage (sayMessage, character);
			break;
		}

		MoveUpChat ();
	}

	//Ainda falta terminar a funcao
	//Cria uma mensagem de audio
	public void PrintAudioMessage (string audioFile, string character)
	{
		GameObject message = Instantiate (Resources.Load ("Prefabs/" + "AudioPrefab") as GameObject);
		message.transform.parent = grid;
		message.GetComponent<AudioSource> ().clip = Resources.Load<AudioClip> ("Audios/" + audioFile);
	}

	//Ainda falta terminar a funcao
	//Cria uma mensagem de imagem
	public void  PrintPictureMessage (string pictureFile, string character)
	{
		GameObject message = Instantiate (Resources.Load ("Prefabs/" + "PicturePrefab") as GameObject);
		message.transform.parent = grid;
		message.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Pictures/" + pictureFile);
	}

	//Cria a mensagem de texto
	//Creates a new gameObject inside the current sayDialog with the text sent
	public void PrintTextMessage (string text, string character)
	{
		if (character != null && character == "Player") {
			GameObject message = Instantiate (Resources.Load ("Prefabs/" + "PlayerMessagePrefab") as GameObject);
			message.transform.SetParent (this.grid, false);

			Text messageText = message.gameObject.GetComponent<Text> ();
			//messageText.font = messageFont;
			messageText.text = text;
		} else {
			GameObject message = Instantiate (Resources.Load ("Prefabs/" + "NPCMessagePrefab") as GameObject);
			message.transform.SetParent (this.grid, false);

			Text messageText = message.gameObject.GetComponent<Text> ();
			//messageText.font = messageFont;
			messageText.text = text;
		}

		/*Tentando nesta parte comentada fazer com prefabs mais complexos (PlayerMessagePrefabBetter e NPCMessagePrefabBetter) 
		para mensagem ficar com espaço do lado para diferenciar mensagens do player e do NPC
		if (character != null && character.nameText == "Player") {
			GameObject message = Instantiate (Resources.Load ("Prefabs/" + "PlayerMessagePrefabBetter") as GameObject);
			message.transform.parent = this.messageParent;

			message.gameObject.transform.FindChild ("Message").GetComponent<Text> ().font = messageFont;
			message.gameObject.transform.FindChild ("Message").GetComponent<Text> ().text = text;
		} else {
			GameObject message = Instantiate (Resources.Load ("Prefabs/" + "NPCMessagePrefabBetter") as GameObject);
			message.transform.parent = this.messageParent;


			message.gameObject.transform.FindChild ("Message").GetComponent<Text> ().font = messageFont;
			message.gameObject.transform.FindChild ("Message").GetComponent<Text> ().text = text;
		}*/
	}

	//Move o chat para a mensagem mais recente aparecer, abaixando o scroller
	//(como o facebook faz no messenger ao receber uma nova mesangem)
	private void MoveUpChat ()
	{
		historyMask.GetComponent<ScrollRect> ().verticalNormalizedPosition = -0.1f;
	}

	//Para mostrar animacao para informar que NPC esta digitando
	public void NpcWritingAnswer ()
	{
		//Detecta se o command atual eh um Wait
		//Se for faz o MoveUpChat() e cria um GameObject com a animacao de digitacao na parte inferior do Grid
		//Quando trocar de command para algo que nao eh Wait destroi o GameObject da animacao para sumir a animacao
		//Em seguida se chama o PrinSayMessage()
	}

	//Para mostrar que NPC esta ausente da conversa (quando for colocar real-time)
	public void NpcAFK ()
	{
		//Detecta se o command atual eh um Wait com espera maior que X (a determinar) segundos
		//Se for cria um GameObject com a animacao de digitacao
		//Quando trocar de command para algo que nao eh Wait destroi o GameObject da animacao para sumir a animacao
		//Em seguida se chama o PrinSayMessage()
	}
}