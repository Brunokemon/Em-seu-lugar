using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveGame : MonoBehaviour
{

	//IDs dos comandos que já foram executados

	//Modelo para todos os flowcharts diferentes
	//public static List<DictionaryClasses.SaveValues> NPCExecutedCommands = new List<DictionaryClasses.SaveValues> ();

	public static List<DictionaryClasses.SaveValues> JuliaExecutedCommands = new List<DictionaryClasses.SaveValues> ();

	public static void SaveOrder (string flowchartName, int command)
	{
		DictionaryClasses.SaveValues save = new DictionaryClasses.SaveValues ();
		save.command = command;
		FlowchartSaveList (flowchartName).Add (save);
	}

	//Retorna a lista da flowchart com o nome dado
	private static List<DictionaryClasses.SaveValues> FlowchartSaveList (string flowchartName)
	{
		switch (flowchartName) {
		case "JuliaFlowchart":
			return JuliaExecutedCommands;
		default:
			return null;
		}
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