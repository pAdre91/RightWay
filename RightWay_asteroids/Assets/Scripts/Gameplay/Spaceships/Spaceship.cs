#pragma warning disable CS0649


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
		protected float _defaultHealth;

		public MovementSystem MovementSystem => _movementSystem;
		public WeaponSystem WeaponSystem => _weaponSystem;

		public UnitBattleIdentity BattleIdentity => _battleIdentity;

		protected void Start()
		{
			_shipController.Init(this);
			_weaponSystem.Init(_battleIdentity);
		}

		virtual public void ApplyDamage(IDamageDealer damageDealer)
		{
			Destroy(gameObject);
		}
	}
}
