//Script usado para guardar as classes criadas para guardar valores

using UnityEngine;
using System.Collections;

//Classe para usar com Dictionary para guardar valores (frase e character) de cada SayDialog no FungusManager.cs
public class SaveValues
{
	//Nao precisa do ID do flowchart pois ja sera dividido cada flowchart em uma lista, como no exemplo da JuliaExecutedCommands
	//public int flowchart { get; set; }

	//Nao precisa do ID do bloco, pois o ID do command ja eh unico dentro de um flowchart
	//public string block { get; set; }

	public int command { get; set; }
}

//Classe para usar com Dictionary para guardar valores (frase e character) de cada SayDialog em TextReader.cs
public class SayValues
{
	public string text { get; set; }

	public string character { get; set; }
}