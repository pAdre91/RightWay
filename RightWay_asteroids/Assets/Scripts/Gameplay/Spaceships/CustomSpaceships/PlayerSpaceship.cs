#pragma warning disable CS0649

using Gameplay.GUI;
using Gameplay.ShipsData.CustomData;
using Gameplay.Weapons;
using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.Spaceships.CustomSpaceships
{
	public class PlayerSpaceship : Spaceship, IDamagable
	{
		[SerializeField]
		private PlayerData _playerData;

		[SerializeField]
		private TextInfoViewer _healthViewer;

		[SerializeField]
		private TextInfoViewer _scoreViewer;

		private const float _defaultScore = 0;

		private new void Start()
		{
			base.Start();
			Init();
		}

		private void Init()
		{
			_playerData.Health = _defaultHealth;
			DisplayHealth();
			RefreshScore(_defaultScore);

			Subscribe();
		}

		public override void ApplyDamage(IDamageDealer damageDealer)
		{
			_playerData.Health -= damageDealer.Damage;

			if (IsShipDead())
				DestroyShip();

			DisplayHealth();
		}

		private void DisplayHealth()
		{
			_healthViewer.Display(_playerData.Health.ToString());
		}

		private void RefreshScore(float additionalPoints)
		{
			_playerData.Score += additionalPoints;
			_scoreViewer.Display(_playerData.Score.ToString());
		}

		private bool IsShipDead()
		{
			if (_playerData.Health > 0)
				return false;
			return true;
		}

		private void DestroyShip()
		{
			UnSubscribe();
			Observer.Instance().PlayerDeadWithScore.Invoke(_playerData.Score);
			Observer.Instance().PlayerDead.Invoke();

			Destroy(gameObject);
		}

		private void Subscribe()
		{
			Observer.Instance().EnemyDown.AddListener(RefreshScore);
		}

		private void UnSubscribe()
		{
			Observer.Instance().EnemyDown.RemoveListener(RefreshScore);
		}
	}
}
