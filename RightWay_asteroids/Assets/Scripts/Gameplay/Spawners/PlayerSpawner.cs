#pragma warning disable CS0649

using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.Spawners
{
	public class PlayerSpawner : MonoBehaviour
	{
		[SerializeField]
		private GameObject _playerPrefab;

		private GameObject _player;
		private Observer _observer = Observer.Instance();

		private void Init()
		{
			if (_player != null)
				Destroy(_player);

			_player = Instantiate(_playerPrefab, gameObject.transform.position, gameObject.transform.rotation);
		}

		private void Subscribe()
		{
			_observer.RestartLevel.AddListener(Init);
		}

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
