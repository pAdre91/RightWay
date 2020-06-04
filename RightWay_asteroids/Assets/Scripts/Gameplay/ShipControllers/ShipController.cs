using Gameplay.ShipSystems;
using Gameplay.Spaceships;
using UnityEngine;

namespace Gameplay.ShipControllers
{
	public abstract class ShipController : MonoBehaviour
	{
		//Ссылка на скрипт
		private ISpaceship _spaceship;

		//Инициализация мемберов
		public void Init(ISpaceship spaceship)
		{
			_spaceship = spaceship;
		}

		private void Update()
		{
			ProcessHandling(_spaceship.MovementSystem);
			ProcessFire(_spaceship.WeaponSystem);
		}

		//Метод реализуемый наследниками
		protected abstract void ProcessHandling(MovementSystem movementSystem);
		//Метод реализуемый наследниками
		protected abstract void ProcessFire(WeaponSystem fireSystem);
	}
}
