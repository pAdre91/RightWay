#pragma warning disable CS0649

using Gameplay.Helpers;
using Gameplay.Weapons;
using Gameplay.ShipsData.CustomData;
using UnityEngine;

namespace Gameplay.Spaceships.CustomSpaceships
{
	public class EnemySpaceship : Spaceship, IDamagable
	{
		[SerializeField]
		private EnemyData _enemyData;

		[SerializeField]
		private float _reward;

		private Observer _observer = Observer.Instance();

		private new void Start()
		{
			base.Start();
			_enemyData.Health = _defaultHealth;
			_enemyData.Reward = _reward;
		}

		public override void ApplyDamage(IDamageDealer damageDealer)
		{
			_enemyData.Health -= damageDealer.Damage;

			if (IsShipDead())
			{
				_observer.DownEnemyWithReward.Invoke(_enemyData.Reward);
				_observer.ObectOutdated.Invoke(gameObject);
				_observer.EnemyDown.Invoke(gameObject);
			}
		}

		private bool IsShipDead()
		{
			if (_enemyData.Health > 0)
				return false;
			return true;
		}
	}
}
