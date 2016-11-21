using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Proxima versao mudar nome para SaveLoad
//Duas opcoes possiveis para classe:
//1-deixar a classe static e tira heranca do MonoBehaviour e chama a funcao de salvar em cada operacao que considerar que deve salvar
//2-deixar como herdeira da classe MonoBehaviour e instancia em um objeto para com as funcoes Start e Update fazer automaticamente o Load e o Save
public class SaveGame : MonoBehaviour
{
	//Modelo para todos os flowcharts diferentes
	//public static List<DictionaryClasses.SaveValues> NPCExecutedCommands = new List<DictionaryClasses.SaveValues> ();

	public static List<DictionaryClasses.SaveValues> JuliaExecutedCommands = new List<DictionaryClasses.SaveValues> ();

	//Salva ordem e ID's dos comandos executados
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

	void Start ()
	{
		//Funcao que verifica se existe um arquivo de save no local onde esta sendo rodado o jogo
		//Funcao que faz load de Save caso exista
	}

	void Update ()
	{
		//Funcao que faz automaticamente o save
	}
}