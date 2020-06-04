#pragma warning disable CS0649

using Gameplay.Helpers;
using Gameplay.ShipSystems;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Bonuses
{
	abstract public class Bonus : MonoBehaviour
	{
		//скрипт системы движения
		[SerializeField]
		private MovementSystem _movementSystem;

		//Ссылка на наблюдателя
		private Observer _observer = Observer.Instance();

		//Метод применения бонуса реализуемый наследниками
		abstract public void ApplyBonus(IBonusRecipient playerSpaceship);

		protected void Start()
		{
			Subscribe();
		}

		private void Update()
		{
			_movementSystem.LongitudinalMovement(Time.deltaTime);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			IBonusRecipient playerSpaceship = collision.gameObject.GetComponent<IBonusRecipient>();

			if (playerSpaceship != null)
			{
				ApplyBonus(playerSpaceship);
				Observer.Instance().ObectOutdated.Invoke(gameObject);
			}
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}

		//Подписка на события
		private void Subscribe()
		{
			_observer.PlayerDead.AddListener(DestroyBonus);
		}

		//Отписка от событий
		private void UnSubscribe()
		{
			_observer.PlayerDead.RemoveListener(DestroyBonus);
		}

		//Пометка объекта устаревшим
		private void DestroyBonus()
		{
			_observer.ObectOutdated.Invoke(gameObject);
		}
	}
}
