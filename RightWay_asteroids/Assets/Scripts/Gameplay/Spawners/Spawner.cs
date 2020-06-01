#pragma warning disable CS0649

using Gameplay.Helpers;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Spawners
{
	public class Spawner : MonoBehaviour
	{

		[SerializeField]
		private GameObject _object;

		[SerializeField]
		private Transform _parent;

		[SerializeField]
		private Vector2 _spawnPeriodRange;

		[SerializeField]
		private Vector2 _spawnDelayRange;

		[SerializeField]
		private bool _autoStart = true;


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
			yield return new WaitForSeconds(Random.Range(_spawnDelayRange.x, _spawnDelayRange.y));

			while (true)
			{
				Instantiate(_object, transform.position, transform.rotation, _parent);
				yield return new WaitForSeconds(Random.Range(_spawnPeriodRange.x, _spawnPeriodRange.y));
			}
		}

		private void Subscribe()
		{
			Observer.Instance().PlayerDead.AddListener(StopSpawn);
		}

		private void UnSubscribe()
		{
			Observer.Instance().PlayerDead.RemoveListener(StopSpawn);
		}
	}
}
