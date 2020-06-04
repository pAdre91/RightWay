#pragma warning disable CS0649

using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.Bonuses.CustomBonuses
{
	public class HealthBonus : Bonus
	{
		//сила аптечки
		[SerializeField]
		private float _healingPower;

		//применение оптечки к получателю
		public override void ApplyBonus(IBonusRecipient recipient)
		{
			recipient.Healing(_healingPower);
			Observer.Instance().ObectOutdated.Invoke(gameObject);
		}
	}
}
