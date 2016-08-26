﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TextReader : MonoBehaviour {

	//Texto exportado pelo próprio Fungus - Localization
	public TextAsset dialogs01;
	private string bigString;
	//Lista com todas as linhas
	private List<string> allLines = new List<string> ();

	void Awake () {
		//Converte o texto em uma longa string
		bigString = dialogs01.text;
		//Quebra o texto em linhas e salva cada linha em uma posição do array
		allLines.AddRange (bigString.Split("\n"[0]));

		//Seleciona apenas o texto dos Commands exportados no texto e imprime no console
		for (int t = 0; t < allLines.Count-6; t++) {
			if (t % 3 == 1 || t == 1){
				//print (allLines[t]);
			}
		}
	}

	public string FindCorectLine(int commandID, string commandType){
		if (commandType == "SAY") {
			for (int s = 0; s < allLines.Count; s++) {
				if (allLines [s] == string.Format ("#SAY.DialogText.{0}.", commandID)) {
					return allLines [s + 1];
				}
			}
		} 
		else if (commandType == "MENU") {
			for (int s = 0; s < allLines.Count; s++) {
				if (allLines [s] == string.Format ("#MENU.DialogText.{0}", commandID)) {
					return allLines [s + 1];
				}
			}

		}
		return null;
	}
}
