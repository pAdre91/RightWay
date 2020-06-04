#pragma warning disable CS0649

using System.Collections;
using Gameplay.Helpers;
using Gameplay.ShipSystems;
using UnityEngine;

namespace Gameplay.ShipControllers.CustomControllers
{
	public class EnemyShipController : ShipController
	{

		[SerializeField]
		private Vector2 _fireDelay;

		private Observer _observer = Observer.Instance();
		private bool _fire = true;

		protected override void ProcessHandling(MovementSystem movementSystem)
		{
			movementSystem.LongitudinalMovement(Time.deltaTime);
		}

		protected override void ProcessFire(WeaponSystem fireSystem)
		{
			if (!_fire)
				return;

			fireSystem.TriggerFire();
			StartCoroutine(FireDelay(Random.Range(_fireDelay.x, _fireDelay.y)));
		}


		private IEnumerator FireDelay(float delay)
		{
			_fire = false;
			yield return new WaitForSeconds(delay);
			_fire = true;
		}

		private void Start()
		{
			Subscribe();
		}

		private void DestroyYourself()
		{
			_observer.ObectOutdated.Invoke(gameObject);
		}

		private void Subscribe()
		{
			_observer.PlayerDead.AddListener(DestroyYourself);
		}

		private void UnSubscribe()
		{
			_observer.PlayerDead.RemoveListener(DestroyYourself);
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}
	}
}
