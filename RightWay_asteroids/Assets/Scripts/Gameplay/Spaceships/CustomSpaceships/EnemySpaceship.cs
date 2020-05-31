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
				Observer.Instance().DownEnemyWithReward.Invoke(_enemyData.Reward);
				Destroy(gameObject);
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
