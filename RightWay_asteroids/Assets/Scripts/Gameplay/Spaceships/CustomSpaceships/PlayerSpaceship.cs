#pragma warning disable CS0649

using Gameplay.GUI;
using Gameplay.ShipsData.CustomData;
using Gameplay.Weapons;
using Gameplay.Helpers;
using Gameplay.Bonuses;
using Gameplay.Storage;

using UnityEngine;
using System.Collections;

namespace Gameplay.Spaceships.CustomSpaceships
{
	public class PlayerSpaceship : Spaceship, IDamagable, IBonusRecipient
	{
		//ссылка на компонент даты игрока
		[SerializeField]
		private PlayerData _playerData;
		
		//ссылка на объект отображения жизней
		private TextInfoViewer _healthViewer;
		//ссылка на объект отображения счета
		private TextInfoViewer _scoreViewer;

		//флаг задержки получения буста скорости
		private bool _isTimerStarted = false;
		//ссылка на наблюдателя
		private Observer _observer = Observer.Instance();
		//ссылка на хранилище UI объектов
		private UIObjectsStorage _objectsStorageUI = UIObjectsStorage.Instance;

		//текущая скорость игрока
		public float CurrentSpeed => _playerData.Speed;

		//Хил
		public void Healing(float heal)
		{
			_playerData.Health += heal;
			DisplayHealth();
		}

		//буст скорости
		public void IncreaseSpeed(float speedPower, float duration)
		{
			if (!_isTimerStarted)
				StartCoroutine(SpeedTimer(speedPower, duration));
		}

		//таймер буста скорости
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

		//Инициализация переменных
		private void Init()
		{
			_healthViewer = _objectsStorageUI.PlayerHealthUI;
			_scoreViewer = _objectsStorageUI.PlayerScoreUI;

			_playerData.Health = _defaultHealth;
			_playerData.Speed = Constants.DefaultPlayerSpeed;

			DisplayHealth();
			RewriteScore(Constants.DefaultPlayerScore);
		}

		//обработка получения урона
		public override void ApplyDamage(IDamageDealer damageDealer)
		{
			_playerData.Health -= damageDealer.Damage;

			if (IsShipDead())
				DestroyShip();

			DisplayHealth();
		}

		//обновление информации о жизнях игрока
		private void DisplayHealth()
		{
			_healthViewer.Display(_playerData.Health.ToString());
		}

		//обновление информации о счете игрока
		private void RefreshScore(float additionalPoints)
		{
			_playerData.Score += additionalPoints;
			_scoreViewer.Display(_playerData.Score.ToString());
		}

		//Сброс счета игрока до конкретного значеиня
		private void RewriteScore(float newScore)
		{
			_playerData.Score = newScore;
			_scoreViewer.Display(_playerData.Score.ToString());
		}

		// проверка на смерть
		private bool IsShipDead()
		{
			if (_playerData.Health > 0)
				return false;
			return true;
		}

		//Обработка уничтожения игрока 
		private void DestroyShip()
		{
			_observer.PlayerDeadWithScore.Invoke(_playerData.Score);
			_observer.PlayerDead.Invoke();
			Destroy(gameObject);
		}

		//Подписка на события
		private void Subscribe()
		{
			_observer.RestartLevel.AddListener(Init);
			_observer.DownEnemyWithReward.AddListener(RefreshScore);
		}

		//отписка от событий
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
