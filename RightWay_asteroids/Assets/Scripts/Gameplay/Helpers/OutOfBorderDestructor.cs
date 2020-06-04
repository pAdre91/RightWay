#pragma warning disable CS0649

using UnityEngine;

namespace Gameplay.Helpers
{
	public class OutOfBorderDestructor : MonoBehaviour
	{
		//Спрайт отслеживаемого объекта
		[SerializeField]
		private SpriteRenderer _representation;

		void Update()
		{
			CheckBorders();
		}

		//Проверка нахождения объекта в области видимости камеры
		private void CheckBorders()
		{
			if (!GameAreaHelper.IsInGameplayArea(transform, _representation.bounds))
			{
				Observer.Instance().ObectOutdated.Invoke(gameObject);
			}
		}
	}
}
