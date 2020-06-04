using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.GUI.Buttons
{
	public class RestartButton : MonoBehaviour
	{
		//Действие на нажатие кнопки
		public void Action()
		{
			Observer.Instance().RestartLevel.Invoke();
		}
	}
}
