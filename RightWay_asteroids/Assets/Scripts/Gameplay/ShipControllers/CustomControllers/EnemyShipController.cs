#pragma warning disable CS0649

using System.Collections;
using Gameplay.Helpers;
using Gameplay.ShipSystems;
using UnityEngine;

namespace Gameplay.ShipControllers.CustomControllers
{
	public class EnemyShipController : ShipController
	{
		//время перезарядки
		[SerializeField]
		private Vector2 _fireDelay;

		//ссылка на наблюдателя
		private Observer _observer = Observer.Instance();
		//флаг готовности к стрельбе
		private bool _fire = true;

		//обработка двжения
		protected override void ProcessHandling(MovementSystem movementSystem)
		{
			movementSystem.LongitudinalMovement(Time.deltaTime);
		}

		//обработк стрельбы
		protected override void ProcessFire(WeaponSystem fireSystem)
		{
			if (!_fire)
				return;

			fireSystem.TriggerFire();
			StartCoroutine(FireDelay(Random.Range(_fireDelay.x, _fireDelay.y)));
		}

		//перезарядка
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

		//метк устаревания объекта
		private void DestroyYourself()
		{
			_observer.ObectOutdated.Invoke(gameObject);
		}

		//подписка на события
		private void Subscribe()
		{
			_observer.PlayerDead.AddListener(DestroyYourself);
		}

		//Отписка от событий
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
