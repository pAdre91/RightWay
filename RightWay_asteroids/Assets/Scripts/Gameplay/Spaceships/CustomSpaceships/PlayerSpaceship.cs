#pragma warning disable CS0649

using Gameplay.GUI;
using Gameplay.ShipsData.CustomData;
using Gameplay.Weapons;
using Gameplay.Helpers;
using UnityEngine;
using System.Collections;
using Gameplay.Bonuses;

namespace Gameplay.Spaceships.CustomSpaceships
{
	public class PlayerSpaceship : Spaceship, IDamagable, IBonusRecipient
	{
		[SerializeField]
		private PlayerData _playerData;

		[SerializeField]
		private TextInfoViewer _healthViewer;

		[SerializeField]
		private TextInfoViewer _scoreViewer;

		private bool _isTimerStarted = false;
		private Observer _observer = Observer.Instance();

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
			_playerData.Speed = Constants.DefaultPlayerSpeed;

			DisplayHealth();
			RewriteScore(Constants.DefaultPlayerScore);
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
			_observer.PlayerDeadWithScore.Invoke(_playerData.Score);
			_observer.PlayerDead.Invoke();
			_observer.ObectOutdated.Invoke(gameObject);
		}

		private void Subscribe()
		{
			_observer.RestartLevel.AddListener(Init);
			_observer.DownEnemyWithReward.AddListener(RefreshScore);
		}

		private void UnSubscribe()
		{
			_observer.RestartLevel.RemoveListener(Init);
			_observer.DownEnemyWithReward.RemoveListener(RefreshScore);
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}
	}
}
