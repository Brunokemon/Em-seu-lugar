using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatController : MonoBehaviour
{
	private static ChatController _instance;

	public static ChatController Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<ChatController> ();
			}
			return _instance;
		}
	}

	public Transform m_Container;
	public Text m_InputText;
	public GameObject m_buttonMenu1;
	public GameObject m_buttonMenu2;
	public string p_chosenOption;
	private string block1;
	private string block2;
	private TypewriterEffect m_textEffect;

	[SerializeField]
	private VerticalLayoutGroup layoutGroup;

	private string message;

	private void Awake ()
	{
		m_textEffect = m_InputText.transform.GetComponent<TypewriterEffect> ();
	}

	public void FillInputField (string p_message)
	{
		message = p_message;
		m_InputText.text = p_message;
		m_textEffect.StartEffect ();
	}

	public void AddBubbleLeft (string p_message = "")
	{
		GameObject newMessage = Instantiate (Resources.Load ("Prefabs/" + "LeftBubble")) as GameObject;

		newMessage.transform.SetParent (m_Container, false);
		newMessage.transform.GetComponent<BubbleView> ().SetText (p_message);

		ClearInput ();
	}

	public void AddBubbleRight (string p_message = "")
	{
		m_buttonMenu1.SetActive (false);
		m_buttonMenu2.SetActive (false);
		m_textEffect.StopEffect ();

		GameObject newMessage = Instantiate (Resources.Load ("Prefabs/" + "RightBubble")) as GameObject;

		newMessage.transform.SetParent (m_Container, false);
		newMessage.transform.GetComponent<BubbleView> ().SetText (p_message);
		ClearInput ();
	}

	public void ClickHandler ()
	{
		this.AddBubbleRight (message);
		if (p_chosenOption == m_buttonMenu1.name) {
			FungusManager.Instance.ExecuteBlock (block1);
		} else {
			FungusManager.Instance.ExecuteBlock (block2);
		}
	}

	private void ClearInput ()
	{
		m_InputText.text = ".";
		m_InputText.text = "";
		layoutGroup.CalculateLayoutInputVertical ();
	}

	public void ActivateOptions (string option1, string block1, string option2, string block2)
	{
		m_buttonMenu1.SetActive (true);
		m_buttonMenu2.SetActive (true);
		m_buttonMenu1.GetComponent<MessageButton> ().Populate (option1);
		m_buttonMenu2.GetComponent<MessageButton> ().Populate (option2);
		this.block1 = block1;
		this.block2 = block2;
	}
}