using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using System.Linq;

public class ViewManager : MonoBehaviour
{
	//Panel que contem o scroll
	public Transform panelScroll;
	//Panel onde são colocadas as mensagens do chat
	public Transform panelMessages;

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
			PrintTextMessage (sayMessage, character);
			break;
		}

		MoveUpChat ();
	}

	//Ainda falta terminar a funcao
	//Cria uma mensagem de audio
	private void PrintAudioMessage (string audioFile, string character)
	{
		GameObject message = Instantiate (Resources.Load ("Prefabs/" + "AudioPrefab") as GameObject);
		message.transform.parent = panelMessages;
		message.GetComponent<AudioSource> ().clip = Resources.Load<AudioClip> ("Audios/" + audioFile);
	}

	//Ainda falta terminar a funcao
	//Cria uma mensagem de imagem
	private void  PrintPictureMessage (string pictureFile, string character)
	{
		GameObject message = Instantiate (Resources.Load ("Prefabs/" + "PicturePrefab") as GameObject);
		message.transform.parent = panelMessages;
		message.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Pictures/" + pictureFile);
	}

	//Cria a mensagem de texto
	//Creates a new gameObject inside the current sayDialog with the text sent
	private void PrintTextMessage (string text, string character)
	{
		if (character != null) {
			if (character.ToUpper () == "PLAYER") {
				GameObject message = Instantiate (Resources.Load ("Prefabs/" + "PlayerMessagePrefab") as GameObject);
				message.transform.SetParent (this.panelMessages, false);
				message.gameObject.transform.GetComponentInChildren<Text> ().text = text;
			} else {
				GameObject message = Instantiate (Resources.Load ("Prefabs/" + "NPCMessagePrefab") as GameObject);
				message.transform.SetParent (this.panelMessages, false);
				message.gameObject.transform.GetComponentInChildren<Text> ().text = text;
			}
		}
	}

	//Move o chat para a mensagem mais recente aparecer, abaixando o scroller
	//(como o facebook faz no messenger ao receber uma nova mesangem)
	private void MoveUpChat ()
	{
		panelScroll.GetComponent<ScrollRect> ().verticalNormalizedPosition = 0.0f;
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