using UnityEngine;
using System.Collections;

namespace UnityEngine.UI
{
	public class PanelLayout : VerticalLayoutGroup
	{
		public void AddMessage (string character, /*Color messageColor,*/string text)
		{
			character = character.ToUpper ();

			if (character == "PLAYER") {
				GameObject message = Instantiate (Resources.Load ("Prefabs/" + "PlayerMessagePrefab") as GameObject);
				message.transform.SetParent (this.transform, false);
				message.gameObject.transform.GetComponentInChildren<Text> ().text = text;
				this.SetChildAlongAxis (message.GetComponent<RectTransform> (), 0, 80f, 250f);
			} else {
				GameObject message = Instantiate (Resources.Load ("Prefabs/" + "NPCMessagePrefab") as GameObject);
				message.transform.SetParent (this.transform, false);
				message.gameObject.transform.GetComponentInChildren<Text> ().text = text;
				this.SetChildAlongAxis (message.GetComponent<RectTransform> (), 0, 20f, 250f);
			}
		}
	}
}
