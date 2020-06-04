#pragma warning disable CS0649

using Gameplay.GUI;
using Gameplay.ShipsData.CustomData;
using Gameplay.Weapons;
using Gameplay.Helpers;
using UnityEngine;
using System.Collections;

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
		private const float _defaultSpeed = 1;

		private bool _isTimerStarted = false;

		public float CurrentSpeed => _playerData.Speed;

		public void Healing(float heal)
		{
			_playerData.Health += heal;
			DisplayHealth();
		}

		public void IncreaseSpeed(float speedPower, float duration)
		{
			if (!_isTimerStarted)
				StartCoroutine(SpeedTimer(speedPower, duration));
		}

		private IEnumerator SpeedTimer(float speedPower, float duration)
		{
			_isTimerStarted = true;
			_playerData.Speed += speedPower;
			yield return new WaitForSeconds(duration);
			_playerData.Speed -= speedPower;
			_isTimerStarted = false;
		} 

		private new void Start()
		{
			base.Start();
			Init();
			Subscribe();
		}

		private void Init()
		{
			_playerData.Health = _defaultHealth;
			_playerData.Speed = _defaultSpeed;

			DisplayHealth();
			RewriteScore(_defaultScore);
			DisplayPlayer();
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

		private void RewriteScore(float newScore)
		{
			_playerData.Score = newScore;
			_scoreViewer.Display(_playerData.Score.ToString());
		}

		private bool IsShipDead()
		{
			if (_playerData.Health > 0)
				return false;
			return true;
		}

		private void DisplayPlayer()
		{
			gameObject.SetActive(true);
		}

		private void DestroyShip()
		{
			Observer.Instance().PlayerDeadWithScore.Invoke(_playerData.Score);
			Observer.Instance().PlayerDead.Invoke();
			Observer.Instance().ObectOutdated.Invoke(gameObject);
		}

		private void Subscribe()
		{
			Observer.Instance().RestartLevel.AddListener(Init);
			Observer.Instance().DownEnemyWithReward.AddListener(RefreshScore);
		}

		private void UnSubscribe()
		{
			Observer.Instance().RestartLevel.RemoveListener(Init);
			Observer.Instance().DownEnemyWithReward.RemoveListener(RefreshScore);
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}
	}
}
