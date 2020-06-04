#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.GUI
{
	public class TextInfoViewer : MonoBehaviour
	{
		//ссыдка на текстовое поле
		[SerializeField]
		private Text _textField;

		//префкс дописываемый к отображаемой информации
		[SerializeField]
		private string _prefix;

		//изменение содержания текстового поля
		public void Display(string info)
		{
			_textField.text = _prefix + info;
		}
	}
}
