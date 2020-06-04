#pragma warning disable CS0649

using Gameplay.ShipSystems;
using Gameplay.Helpers;
using UnityEngine;
using Gameplay.Spaceships.CustomSpaceships;

namespace Gameplay.ShipControllers.CustomControllers
{
	public class PlayerShipController : ShipController
	{
		//Ссылка на свой коллайдер
		[SerializeField]
		private Collider2D _objectCollider;

		//Ссылка на компонент PlayerSpaceship
		[SerializeField]
		private PlayerSpaceship _playerSpaceship;

		//обработка горизонтального движения
		protected override void ProcessHandling(MovementSystem movementSystem)
		{
			Vector3 oldPosition = gameObject.transform.position;

			movementSystem.LateralMovement(Input.GetAxis("Horizontal") * Time.deltaTime * _playerSpaceship.CurrentSpeed);

			if (!GameAreaHelper.IsAllObjectInGameplayArea(transform, _objectCollider.bounds))
				gameObject.transform.position = oldPosition;
		}

		//Обработка процесса стрельбы
		protected override void ProcessFire(WeaponSystem fireSystem)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				fireSystem.TriggerFire();
			}
		}
	}
}
