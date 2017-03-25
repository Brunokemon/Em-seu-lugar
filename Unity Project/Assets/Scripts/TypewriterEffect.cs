using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour 
{
	private float m_Pause;

	[SerializeField]
	private int m_LettersPerSecond;
	
	private string m_FullText;

	private Text m_Label;

    private void Awake()
    {
        m_Label = gameObject.GetComponent<Text>();
    }

	public void StartEffect()
	{
        StopAllCoroutines();

		m_FullText = m_Label.text;
		
		m_Label.text = "";
		m_Pause = 1f / m_LettersPerSecond;

		StartCoroutine(TypeText ());

	}

    public void StopEffect()
    {
        StopAllCoroutines();
    }
	
	IEnumerator TypeText ()
	{
		foreach (char letter in m_FullText.ToCharArray()) 
		{
			gameObject.GetComponent<Text>().text += letter;

			yield return new WaitForSeconds (m_Pause);
		}      
	}
}
