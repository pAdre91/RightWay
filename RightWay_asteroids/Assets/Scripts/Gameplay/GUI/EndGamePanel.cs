#pragma warning disable CS0649

using UnityEngine;

namespace Gameplay.GUI
{
	public class EndGamePanel : MonoBehaviour
	{

		//ссылка на объект панели
		[SerializeField]
		private GameObject _panel;

		//ссылка на счетчик панели
		[SerializeField]
		private TextInfoViewer _textInfo;

		//Выключение панели
		protected void TurnOffPanel()
		{
			_panel.SetActive(false);
		}

		//Включение панели
		protected void TurnOnPanel()
		{
			_panel.SetActive(true);
		}

		//Обновление информации на панели
		protected void SetInfo(string info)
		{
			_textInfo.Display(info);
		}
	}
}