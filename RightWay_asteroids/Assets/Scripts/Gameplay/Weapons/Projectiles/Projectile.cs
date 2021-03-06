﻿#pragma warning disable CS0649

using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.Weapons.Projectiles
{
	public abstract class Projectile : MonoBehaviour, IDamageDealer
	{
		//Значение скорости снаряда
		[SerializeField]
		private float _speed;

		//Урон снаряда
		[SerializeField]
		private float _damage;

		//Друженственность снаряда
		private UnitBattleIdentity _battleIdentity;

		public UnitBattleIdentity BattleIdentity => _battleIdentity;
		public float Damage => _damage;

		//Инициализация дружественности снаряда
		public void Init(UnitBattleIdentity battleIdentity)
		{
			_battleIdentity = battleIdentity;
		}

		private void Update()
		{
			Move(_speed);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			var damagableObject = other.gameObject.GetComponent<IDamagable>();

			if (damagableObject != null
				&& damagableObject.BattleIdentity != BattleIdentity)
			{
				damagableObject.ApplyDamage(this);
				Observer.Instance().ObectOutdated.Invoke(gameObject);
			}
		}

		// реализуемый наследниками метод движения снаряда
		protected abstract void Move(float speed);
	}
}
