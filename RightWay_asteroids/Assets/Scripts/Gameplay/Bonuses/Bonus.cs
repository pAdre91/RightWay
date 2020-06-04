#pragma warning disable CS0649

using Gameplay.Helpers;
using Gameplay.ShipSystems;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Bonuses
{
	abstract public class Bonus : MonoBehaviour
	{
		[SerializeField]
		private MovementSystem _movementSystem;

		[SerializeField]
		private UnitBattleIdentity _battleIdentity;

		private Observer _observer = Observer.Instance();

		public MovementSystem MovementSystem => _movementSystem;
		public UnitBattleIdentity BattleIdentity => _battleIdentity;

		abstract public void ApplyBonus(IBonusRecipient playerSpaceship);

		protected void Start()
		{
			Subscribe();
		}

		private void Update()
		{
			MovementSystem.LongitudinalMovement(Time.deltaTime);
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

		private void Subscribe()
		{
			_observer.PlayerDead.AddListener(DestroyBonus);
		}

		private void UnSubscribe()
		{
			_observer.PlayerDead.RemoveListener(DestroyBonus);
		}

		private void DestroyBonus()
		{
			_observer.ObectOutdated.Invoke(gameObject);
		}
	}
}
