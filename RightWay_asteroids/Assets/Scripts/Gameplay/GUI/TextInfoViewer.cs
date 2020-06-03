#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.GUI
{
	public class TextInfoViewer : MonoBehaviour
	{
		[SerializeField]
		private Text _textField;

		[SerializeField]
		private string _prefix;

		public void Display(string info)
		{
			_textField.text = _prefix + info;
		}
	}
}
