#pragma warning disable CS0649

using Gameplay.Helpers;
using Gameplay.Helpers.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Storage
{
	public class ObjectsStorage : MonoBehaviour
	{
		//список префабов хранимых объектов
		[SerializeField]
		private List<GameObject> _storageObjects;

		//Количество создаваемых объектов
		[SerializeField]
		private int _defaultPoolsSize;
		//пулы объектов с тегом для поиска
		private Dictionary<string, GameObjectPool> _pools = new Dictionary<string, GameObjectPool>();
		//толкьо теги объектов
		private List<string> _poolsKey = new List<string>();
		//ссылка на наблюдателя
		private Observer _observer = Observer.Instance();

		//Singleton
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
			InitializePools(_storageObjects, _defaultPoolsSize);

			Subscribe();
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}

		//возвращение объекта из пула по типу
		public GameObject GetObject(string objectType)
		{
			if (!_poolsKey.Contains(objectType))
				throw new System.NotImplementedException();
			return _pools[objectType].Take();
		}

		//Создание пулов объектов
		private void InitializePools(List<GameObject> typesObject, int poolSize)
		{
			if (typesObject.Count == 0)
				throw new System.NotImplementedException();

			foreach (GameObject newObject in typesObject)
			{
				if (!newObject.TryGetComponent(out PooledObject pooledObject))
					continue;

				GameObject newObjectStorage = new GameObject(newObject.name);
				newObjectStorage.transform.parent = gameObject.transform;

				GameObjectPool pool = new GameObjectPool();
				_pools.Add(pooledObject.Type, pool);
				pool.GeneratePool(newObject, poolSize, newObjectStorage.transform);
				_poolsKey.Add(pooledObject.Type);
			}
		}

		//Возвращение объекта в хранилище
		private void ReturnObjectToStorage(GameObject gameObject)
		{
			if (!gameObject.TryGetComponent(out PooledObject pooledObject))
				return;

			_pools[pooledObject.Type].Release(gameObject);
		}

		//Подписка на события
		private void Subscribe()
		{
			_observer.ObectOutdated.AddListener(ReturnObjectToStorage);
		}

		//Отписка от событий
		private void UnSubscribe()
		{
			_observer.ObectOutdated.RemoveListener(ReturnObjectToStorage);
		}
	}
}
