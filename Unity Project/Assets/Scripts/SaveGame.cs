using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveGame : MonoBehaviour
{

	//IDs dos comandos que já foram executados
	public static List<SaveValues> JuliaExecutedCommands = new List<SaveValues> ();

	public static void SaveOrder (List<SaveValues> list, int command)
	{
		SaveValues save = new SaveValues ();
		save.command = command;
		list.Add (save);
	}

	// Use this for initialization
	void Start ()
	{
		//Faz load de Save já existente caso tenha
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Dado que esta sendo feito ja no FungusManager podemos colocar ja a funcao de salvar automaticamente aqui, assim fica desvinculado do FungusManager e salvar fica independete aqui
	}
}