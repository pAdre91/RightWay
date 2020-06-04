#pragma warning disable CS0649

using System.Collections.Generic;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.ShipSystems
{
	public class WeaponSystem : MonoBehaviour
	{

		[SerializeField]
		private List<Weapon> _weapons;



		public void Init(UnitBattleIdentity battleIdentity)
		{
			_weapons.ForEach(w => w.Init(battleIdentity));
		}


		public void TriggerFire()
		{
			_weapons.ForEach(w => w.TriggerFire());
		}

	}
}
