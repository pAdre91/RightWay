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
		[SerializeField]
		private List<GameObject> _spawnedObjects;

		[SerializeField]
		private Vector2 _spawnPeriodRange;

		[SerializeField]
		private Vector2 _spawnDelayRange;

		[SerializeField]
		private bool _autoStart = true;

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

		public void StartSpawn()
		{
			StartCoroutine(Spawn());
		}

		public void StopSpawn()
		{
			StopAllCoroutines();
		}

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

		private void Subscribe()
		{
			_observer.PlayerDead.AddListener(StopSpawn);
			_observer.RestartLevel.AddListener(StartSpawn);
		}

		private void UnSubscribe()
		{
			_observer.PlayerDead.RemoveListener(StopSpawn);
			_observer.RestartLevel.RemoveListener(StartSpawn);
		}
	}
}
