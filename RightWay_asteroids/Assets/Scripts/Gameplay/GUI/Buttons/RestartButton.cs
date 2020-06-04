using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.GUI.Buttons
{
	public class RestartButton : MonoBehaviour
	{
		public void Action()
		{
			Observer.Instance().RestartLevel.Invoke();
		}
	}
}
