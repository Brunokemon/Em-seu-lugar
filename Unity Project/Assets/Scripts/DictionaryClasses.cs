//Script usado para guardar as classes criadas para guardar valores
using System.Collections;

[System.Serializable]
public class DictionaryClasses
{
	//Classe para usar com Dictionary para guardar valores (Block e commandID) de cada SayDialog no FungusManager.cs
	[System.Serializable]
	public class SaveValues
	{
		public int command { get; set; }

		public float time { get; set; }
	}

	//Classe para usar com Dictionary para guardar valores (frase e character) de cada SayDialog em TextReader.cs
	[System.Serializable]
	public class SayValues
	{
		public string text { get; set; }

		public string character { get; set; }
	}
}