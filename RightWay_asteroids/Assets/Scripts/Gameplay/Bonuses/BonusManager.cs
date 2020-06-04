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
		[SerializeField]
		List<GameObject> _bonuses;

		private Observer _observer = Observer.Instance();
		private System.Random _randomizer = new System.Random();

		private void CreateBonusWithChance(GameObject downEnemy)
		{
			float chance = _randomizer.Next(100);

			if (chance < Constants.ChanceGetBonus )
				CreateBonus(downEnemy.transform.position, downEnemy.transform.rotation);

		}

		private void CreateBonus(Vector3 startPosition, Quaternion rotation)
		{
			int randomBonusIndex = _randomizer.Next(_bonuses.Count);
			Debug.Log(randomBonusIndex);
			PooledObject pooledObject = _bonuses[randomBonusIndex].GetComponent<PooledObject>();

			if (pooledObject != null)
			{
				GameObject newBonus = ObjectsStorage.Instance.GetObject(pooledObject.Type);
				newBonus.SetActive(true);
				newBonus.transform.position = startPosition;
				newBonus.transform.rotation = rotation;
			}
		}

		private void Subscribe()
		{
			_observer.EnemyDown.AddListener(CreateBonusWithChance);
		}

		private void UnSubscribe()
		{
			_observer.EnemyDown.RemoveListener(CreateBonusWithChance);
		}

		private void Start()
		{
			Subscribe();
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}
	}
}
