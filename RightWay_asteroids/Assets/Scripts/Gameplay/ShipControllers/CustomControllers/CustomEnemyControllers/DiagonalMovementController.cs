#pragma warning disable CS0649

using Gameplay.ShipSystems;
using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.ShipControllers.CustomControllers.CustomEnemyControllers
{
	public class DiagonalMovementController : EnemyShipController
	{
		//Ссылка на свой коллайдер
		[SerializeField]
		private Collider2D _collider2D;

		//флаг касания объекта края области видимости камеры
		private bool _isShipTouchedBorder = false;

		//Кастомное движение объекта
		protected override void ProcessHandling(MovementSystem movementSystem)
		{
			if (!GameAreaHelper.IsAllObjectInGameplayArea(gameObject.transform, _collider2D.bounds))
				_isShipTouchedBorder = !_isShipTouchedBorder;

			movementSystem.LongitudinalMovement(Time.deltaTime);
			movementSystem.LateralMovement(Time.deltaTime, _isShipTouchedBorder);

		}
	}
}