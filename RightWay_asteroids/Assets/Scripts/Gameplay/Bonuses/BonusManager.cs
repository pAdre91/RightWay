#pragma warning disable CS0649

using Gameplay.Helpers;
using Gameplay.Helpers.Pool;
using Gameplay.Storage;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Bonuses
{
	public class BonusManager : MonoBehaviour
	{
		//Все имеющиеся префабы бонусов
		[SerializeField]
		List<GameObject> _bonuses;

		//Ссылка на наблюдателя
		private Observer _observer = Observer.Instance();

		//Рандомайзер для использования в генерации бонусов
		private System.Random _randomizer = new System.Random();

		private void Start()
		{
			Subscribe();
		}

		//Производится рассчет создавать или не создавать бонус, в зависимости от шанса
		private void CreateBonusWithChance(GameObject downEnemy)
		{
			float chance = _randomizer.Next(100);

			if (chance < Constants.ChanceGetBonus )
				CreateBonus(downEnemy.transform.position, downEnemy.transform.rotation);

		}

		//Создание бонуса
		private void CreateBonus(Vector3 startPosition, Quaternion rotation)
		{
			int randomBonusIndex = _randomizer.Next(_bonuses.Count);
			PooledObject pooledObject = _bonuses[randomBonusIndex].GetComponent<PooledObject>();

			if (pooledObject != null)
			{
				GameObject newBonus = ObjectsStorage.Instance.GetObject(pooledObject.Type);
				newBonus.SetActive(true);
				newBonus.transform.position = startPosition;
				newBonus.transform.rotation = rotation;
			}
		}

		//Подписка на события
		private void Subscribe()
		{
			_observer.EnemyDown.AddListener(CreateBonusWithChance);
		}

		//Отписка от событий
		private void UnSubscribe()
		{
			_observer.EnemyDown.RemoveListener(CreateBonusWithChance);
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}
	}
}
