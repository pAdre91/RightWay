#pragma warning disable CS0649

using Gameplay.ShipControllers;
using Gameplay.ShipSystems;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Spaceships
{
	public class Spaceship : MonoBehaviour, ISpaceship, IDamagable
	{
		//ссылка на компонент контроллера
		[SerializeField]
		private ShipController _shipController;

		//Ссылка на компонент двигательной системы
		[SerializeField]
		private MovementSystem _movementSystem;

		//ссылка на оружейную систему
		[SerializeField]
		private WeaponSystem _weaponSystem;

		//определение дружественности корабля
		[SerializeField]
		private UnitBattleIdentity _battleIdentity;

		//Стартовое здоровье корабля
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

		//стандартное поведение при получении кораблем урона
		virtual public void ApplyDamage(IDamageDealer damageDealer)
		{
			Destroy(gameObject);
		}
	}
}
