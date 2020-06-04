#pragma warning disable CS0649

using Gameplay.Helpers;
using Gameplay.Helpers.Pool;
using Gameplay.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Spawners
{
	public class Spawner : MonoBehaviour
	{
		//Список объектов которые может создавать этот спаунер
		[SerializeField]
		private List<GameObject> _spawnedObjects;

		//минимальная задержка спауна
		[SerializeField]
		private Vector2 _spawnPeriodRange;

		//максимальная задержка спауна
		[SerializeField]
		private Vector2 _spawnDelayRange;

		//флаг автоматического спауна врагов
		[SerializeField]
		private bool _autoStart = true;
		//Ссылка на наблюдателя
		private Observer _observer = Observer.Instance();

		private void Start()
		{
			if (_autoStart)
				StartSpawn();

			Subscribe();
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}

		//Обертка над корутиной
		public void StartSpawn()
		{
			StartCoroutine(Spawn());
		}

		//остановка спауна врагов
		public void StopSpawn()
		{
			StopAllCoroutines();
		}

		//Непосредственный спаун врага
		private IEnumerator Spawn()
		{
			System.Random randomizer = new System.Random();
			int randomObject;
			PooledObject pooledObject;
			GameObject newEnemy;

			yield return new WaitForSeconds(Random.Range(_spawnDelayRange.x, _spawnDelayRange.y));

			while (true)
			{
				randomObject = randomizer.Next(_spawnedObjects.Count);
				pooledObject = _spawnedObjects[randomObject].GetComponent<PooledObject>();

				if (pooledObject != null)
				{
					newEnemy = ObjectsStorage.Instance.GetObject(pooledObject.Type);
					newEnemy.SetActive(true);
					newEnemy.transform.position = transform.position;
					newEnemy.transform.rotation = transform.rotation;
				}

				yield return new WaitForSeconds(Random.Range(_spawnPeriodRange.x, _spawnPeriodRange.y));
			}
		}

		//Подписка на события
		private void Subscribe()
		{
			_observer.PlayerDead.AddListener(StopSpawn);
			_observer.RestartLevel.AddListener(StartSpawn);
		}
		//Отписка от событий
		private void UnSubscribe()
		{
			_observer.PlayerDead.RemoveListener(StopSpawn);
			_observer.RestartLevel.RemoveListener(StartSpawn);
		}
	}
}
