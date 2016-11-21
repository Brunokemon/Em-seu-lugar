//Script usado para guardar as classes criadas para guardar valores
using System.Collections;

public class DictionaryClasses
{
	//Classe para usar com Dictionary para guardar valores (Block e commandID) de cada SayDialog no FungusManager.cs
	public class SaveValues
	{
		public string flowchart { get; set; }

		public string block { get; set; }

		public int command { get; set; }
	}

	//Classe para usar com Dictionary para guardar valores (frase e character) de cada SayDialog em TextReader.cs
	public class SayValues
	{
		public string text { get; set; }

		public string character { get; set; }
	}
}