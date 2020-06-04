#pragma warning disable CS0649

using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.Spawners
{
	public class PlayerSpawner : MonoBehaviour
	{
		//Префаб игрока
		[SerializeField]
		private GameObject _playerPrefab;

		//ссылка на объект игрока
		private GameObject _player;
		//ссылка на наблюдателя
		private Observer _observer = Observer.Instance();

		//Инициализация игрока
		private void Init()
		{
			if (_player != null)
				Destroy(_player);

			_player = Instantiate(_playerPrefab, gameObject.transform.position, gameObject.transform.rotation);
		}

		//Подписка на события
		private void Subscribe()
		{
			_observer.RestartLevel.AddListener(Init);
		}

		//Отписка от событий
		private void UnSubscribe()
		{
			_observer.RestartLevel.RemoveListener(Init);
		}

		private void Start()
		{
			Init();
			Subscribe();
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}
	}
}
