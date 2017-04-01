using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleView : MonoBehaviour
{
    public Image[] m_CheckMarks;

    public Color m_Green;
    public Color m_Blue;

    public void SetText(string p_message)
    {
        Text messageText = transform.GetComponentInChildren<Text>();

        messageText.text = p_message;
    }

    public void ColorizeCheckMark(int p_index, Color p_color)
    {
        m_CheckMarks[p_index].color = p_color;
    }
}
