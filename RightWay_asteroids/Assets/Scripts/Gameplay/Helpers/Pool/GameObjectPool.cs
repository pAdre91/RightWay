using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Helpers.Pool
{
	public class GameObjectPool
	{
		private Queue<GameObject> _objectPool = new Queue<GameObject>();
		private GameObject _sampleObject;

		public void GeneratePool(GameObject poolType, int count, Transform parent)
		{
			_sampleObject = poolType;
			for (int i = 0; i < count; i++)
			{
				GameObject newObject = GameObject.Instantiate(poolType, parent);
				newObject.SetActive(false);

				_objectPool.Enqueue(newObject);
			}
		}

		public void Release(GameObject element)
		{
			element.gameObject.SetActive(false);
			_objectPool.Enqueue(element);
		}

		public GameObject Take()
		{
			int counter = 0;

			while (true)
			{
				if (++counter == _objectPool.Count || _objectPool.Count == 0)
				{
					GameObject newObject = GameObject.Instantiate(_sampleObject);
					_objectPool.Enqueue(newObject);
					return newObject;
				}

				if (!_objectPool.Peek().activeSelf)
					return _objectPool.Dequeue();

				GameObject obj = _objectPool.Dequeue();
				_objectPool.Enqueue(obj);
			}
		}
	}
}