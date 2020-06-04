using Gameplay.Bonuses;
using Gameplay.Helpers;
using Gameplay.Spaceships.CustomSpaceships;
using UnityEngine;

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
