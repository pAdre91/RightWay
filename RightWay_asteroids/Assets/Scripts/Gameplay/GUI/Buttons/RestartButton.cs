using Gameplay.Helpers;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
	public void Action()
	{
		Observer.Instance().RestartLevel.Invoke();
	}
}
