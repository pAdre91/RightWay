#pragma warning disable CS0649

using Gameplay.ShipSystems;
using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.ShipControllers.CustomControllers
{
	public class PlayerShipController : ShipController
	{
		[SerializeField]
		private Collider2D objectCollider;

		protected override void ProcessHandling(MovementSystem movementSystem)
		{
			Vector3 oldPosition = gameObject.transform.position;

			movementSystem.LateralMovement(Input.GetAxis("Horizontal") * Time.deltaTime);

			if (!GameAreaHelper.IsAllObjectInGameplayArea(transform, objectCollider.bounds))
				gameObject.transform.position = oldPosition;
		}

		protected override void ProcessFire(WeaponSystem fireSystem)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				fireSystem.TriggerFire();
			}
		}
	}
}
