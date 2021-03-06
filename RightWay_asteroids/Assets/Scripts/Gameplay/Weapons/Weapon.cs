﻿#pragma warning disable CS0649

using System.Collections;
using Gameplay.Weapons.Projectiles;
using Gameplay.Helpers.Pool;
using UnityEngine;
using Gameplay.Storage;

namespace Gameplay.Weapons
{
	public class Weapon : MonoBehaviour
	{
		//ссылка на компонент объекта
		[SerializeField]
		private Projectile _projectile;

		//Ссылка на трансформ оружия
		[SerializeField]
		private Transform _barrel;

		//Задержка стрельбы
		[SerializeField]
		private float _cooldown;

		//флаг готовности стрельбы
		private bool _readyToFire = true;
		//Дружественность снаряда
		private UnitBattleIdentity _battleIdentity;

		//Инициализация дружественности
		public void Init(UnitBattleIdentity battleIdentity)
		{
			_battleIdentity = battleIdentity;
		}

		//Процесс выстрела из орудия
		public void TriggerFire()
		{
			if (!_readyToFire)
				return;

			PooledObject pooledObject = _projectile.GetComponent<PooledObject>();
			GameObject newBullet = ObjectsStorage.Instance.GetObject(pooledObject.Type);
			Projectile proj = newBullet.GetComponent<Projectile>();

			newBullet.SetActive(true);
			newBullet.transform.position = _barrel.position;
			newBullet.transform.rotation = _barrel.rotation;

			proj.Init(_battleIdentity);
			StartCoroutine(Reload(_cooldown));
		}

		//Перезарядка
		private IEnumerator Reload(float cooldown)
		{
			_readyToFire = false;
			yield return new WaitForSeconds(cooldown);
			_readyToFire = true;
		}
	}
}
