using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleView : MonoBehaviour
{

    public void SetText(string p_message)
    {
        Text messageText = transform.GetComponentInChildren<Text>();

        messageText.text = p_message;
    }
}
