#pragma warning disable CS0649

using Gameplay.Helpers;
using Gameplay.Spaceships.CustomSpaceships;
using UnityEngine;

namespace Gameplay.Bonuses.CustomBonuses
{
	public class HealthBonus : Bonus
	{
		[SerializeField]
		private float _healingPower;

		public override void ApplyBonus(PlayerSpaceship playerSpaceship)
		{
			playerSpaceship.Healing(_healingPower);
			Observer.Instance().ObectOutdated.Invoke(gameObject);
		}
	}
}
