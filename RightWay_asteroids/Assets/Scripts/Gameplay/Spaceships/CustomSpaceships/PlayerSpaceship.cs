#pragma warning disable CS0649

using Gameplay.GUI;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Spaceships.CustomSpaceships
{
	public class PlayerSpaceship : Spaceship, IDamagable
	{
		[SerializeField]
		private TextInfoViewer _healthViewer;

		private new void Start()
		{
			base.Start();
			DisplayHealth();
		}

		public new void ApplyDamage(IDamageDealer damageDealer)
		{
			ShipData.Health -= damageDealer.Damage;

			DisplayHealth();

			if (IsShipDead())
				DestroyShip();
		}

		private void DisplayHealth()
		{
			_healthViewer.Display(ShipData.Health.ToString());
		}
	}
}
