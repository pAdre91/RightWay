#pragma warning disable CS0649

using Gameplay.Bonuses;
using Gameplay.Helpers;
using Gameplay.Spaceships.CustomSpaceships;
using UnityEngine;
public class SpeedBonus : Bonus
{
	[SerializeField]
	private float _speedPower;

	[SerializeField]
	private float _duration;

	public override void ApplyBonus(PlayerSpaceship playerSpaceship)
	{
		playerSpaceship.IncreaseSpeed(_speedPower, _duration);
		Observer.Instance().ObectOutdated.Invoke(gameObject);
	}
}
