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
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
