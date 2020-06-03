using UnityEngine;

namespace Gameplay.GUI
{
	public class EndGamePanel : MonoBehaviour
	{
		[SerializeField]
		private GameObject _panel;

		[SerializeField]
		private TextInfoViewer _textInfo;

		protected void TurnOffPanel()
		{
			_panel.SetActive(false);
		}

		protected void TurnOnPanel()
		{
			_panel.SetActive(true);
		}

		protected void SetInfo(string info)
		{
			_textInfo.Display(info);
		}
	}
}