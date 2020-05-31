using System;
using Gameplay.ShipControllers;
using Gameplay.ShipSystems;
using Gameplay.Weapons;
using Gameplay.ShipsData;
using UnityEngine;

namespace Gameplay.Spaceships
{
	public class Spaceship : MonoBehaviour, ISpaceship, IDamagable
	{
		[SerializeField]
		private ShipController _shipController;

		[SerializeField]
		private MovementSystem _movementSystem;

		[SerializeField]
		private WeaponSystem _weaponSystem;

		[SerializeField]
		private UnitBattleIdentity _battleIdentity;

		[SerializeField]
		private ShipData _shipData;

		[SerializeField]
		protected float _defaultHealth;

		protected ShipData ShipData => _shipData;

		public MovementSystem MovementSystem => _movementSystem;
		public WeaponSystem WeaponSystem => _weaponSystem;

		public UnitBattleIdentity BattleIdentity => _battleIdentity;

		protected void Start()
		{
			_shipController.Init(this);
			_weaponSystem.Init(_battleIdentity);
		}

		public void ApplyDamage(IDamageDealer damageDealer)
		{
			_shipData.Health -= damageDealer.Damage;

			if (IsShipDead())
				DestroyShip();
		}

		protected bool IsShipDead()
		{
			if (_shipData.Health > 0)
				return false;
			return true;
		}

		protected void DestroyShip()
		{
			Destroy(gameObject);
		}

	}
}
