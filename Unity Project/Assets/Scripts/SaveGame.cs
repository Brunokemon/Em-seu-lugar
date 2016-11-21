using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Obs: Proxima versao mudar nome para SaveLoad
[System.Serializable]
public static class SaveGame
{

	//Classe onde ficarao reunidas todas as informacoes a serem salvas
	[System.Serializable]
	public class SaveFile
	{
		//Modelo para todos os flowcharts diferentes
		//public static List<DictionaryClasses.SaveValues> NPCExecutedCommands = new List<DictionaryClasses.SaveValues> ();

		public List<DictionaryClasses.SaveValues> JuliaExecutedCommands = new List<DictionaryClasses.SaveValues> ();
	}

	//Instancia da classe onde serao colocadas todas as informacoes a serem salvas
	public static SaveFile saveFile = new SaveFile ();

	//variavel para indicar se ja terminou o loading
	public static bool loaded = false;

	//Obs: Proxima versao mudar nome para SaveGame
	public static void Save ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/save.esl");
		bf.Serialize (file, saveFile);
		file.Close ();
	}

	//Obs: Proxima versao mudar nome para LoadGame
	public static void Load ()
	{
		if (File.Exists (Application.persistentDataPath + "/save.esl")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/save.esl", FileMode.Open);
			saveFile = (SaveFile)bf.Deserialize (file);
			file.Close ();
		}
	}

	//Atualiza lista de ID's dos comandos executados do flowchart especificado
	public static void UpdateExecutedCommands (string flowchartName, int command)
	{
		DictionaryClasses.SaveValues save = new DictionaryClasses.SaveValues ();
		save.command = command;
		FlowchartSaveList (flowchartName).Add (save);
	}

	//Retorna a lista da flowchart com o nome dado
	private static List<DictionaryClasses.SaveValues> FlowchartSaveList (string flowchartName)
	{
		switch (flowchartName) {
		case "Julia":
			return saveFile.JuliaExecutedCommands;
		default:
			return null;
		}
	}
}