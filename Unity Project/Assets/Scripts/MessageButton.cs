﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageButton : MonoBehaviour
{
	[HideInInspector]
	public string m_Message;

	private Text msgText;

	private void Awake ()
	{
		msgText = transform.GetComponentInChildren<Text> ();
	}

	public void Populate (string p_message)
	{
		m_Message = p_message;
		msgText.text = m_Message;
	}

	public void ClickHandler ()
	{
		m_Message = msgText.text;
		ChatController.Instance.FillInputField (m_Message);
		ChatController.Instance.p_chosenOption = transform.parent.name;
	}
}
