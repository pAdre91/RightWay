using Gameplay.Helpers;
using Gameplay.ShipSystems;
using Gameplay.Spaceships.CustomSpaceships;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Bonuses
{
	abstract public class Bonus : MonoBehaviour, IBonus
	{
		[SerializeField]
		private MovementSystem _movementSystem;

		[SerializeField]
		private UnitBattleIdentity _battleIdentity;

		public MovementSystem MovementSystem => _movementSystem;
		public UnitBattleIdentity BattleIdentity => _battleIdentity;

		abstract public void ApplyBonus(PlayerSpaceship playerSpaceship);

		private void Update()
		{
			MovementSystem.LongitudinalMovement(Time.deltaTime);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			PlayerSpaceship playerSpaceship = collision.gameObject.GetComponent<PlayerSpaceship>();     //Нарушение OCP

			if (playerSpaceship != null)
			{
				ApplyBonus(playerSpaceship);
				Observer.Instance().ObectOutdated.Invoke(gameObject);
			}
		}
	}
}
