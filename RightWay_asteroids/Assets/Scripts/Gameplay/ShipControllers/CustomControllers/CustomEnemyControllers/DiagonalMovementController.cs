using Gameplay.ShipSystems;
using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.ShipControllers.CustomControllers.CustomEnemyControllers
{
	public class DiagonalMovementController : EnemyShipController
	{
		[SerializeField]
		private Collider2D _collider2D;

		private bool _isShipTouchedBorder = false;

		protected override void ProcessHandling(MovementSystem movementSystem)
		{
			if (!GameAreaHelper.IsAllObjectInGameplayArea(gameObject.transform, _collider2D.bounds))
				_isShipTouchedBorder = !_isShipTouchedBorder;

			movementSystem.LongitudinalMovement(Time.deltaTime);
			movementSystem.LateralMovement(Time.deltaTime, _isShipTouchedBorder);

		}
	}
}