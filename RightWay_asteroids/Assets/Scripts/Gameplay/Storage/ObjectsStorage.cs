#pragma warning disable CS0649

using Gameplay.Helpers;
using Gameplay.Helpers.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Storage
{
	public class ObjectsStorage : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> _spawnObjects;

		[SerializeField]
		private int _defaultPoolsSize;

		private Dictionary<string, GameObjectPool> _pools = new Dictionary<string, GameObjectPool>();
		private List<string> _poolsKey = new List<string>();

		public static ObjectsStorage Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
			else
				Destroy(this);
		}

		private void Start()
		{
			InitializePools(_spawnObjects, _defaultPoolsSize);

			Subscribe();
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}

		public GameObject GetObject(string objectType)
		{
			if (!_poolsKey.Contains(objectType))
				throw new System.NotImplementedException();
			return _pools[objectType].Take();
		}

		private void InitializePools(List<GameObject> typesObject, int poolSize)
		{
			if (typesObject.Count == 0)
				throw new System.NotImplementedException();

			foreach (GameObject enemy in typesObject)
			{
				if (!enemy.TryGetComponent(out PooledObject pooledObject))
					continue;

				GameObjectPool pool = new GameObjectPool();
				_pools.Add(pooledObject.Type, pool);
				pool.GeneratePool(enemy, poolSize);
				_poolsKey.Add(pooledObject.Type);
			}
		}

		private void ReturnObjectToStorage(GameObject gameObject)
		{
			if (!gameObject.TryGetComponent(out PooledObject pooledObject))
				return;

			_pools[pooledObject.Type].Release(gameObject);
		}

		private void Subscribe()
		{
			Observer.Instance().ObectOutdated.AddListener(ReturnObjectToStorage);
		}

		private void UnSubscribe()
		{
			Observer.Instance().ObectOutdated.RemoveListener(ReturnObjectToStorage);
		}
	}
}
