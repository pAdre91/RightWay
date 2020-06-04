#pragma warning disable CS0649

using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.Bonuses.CustomBonuses
{
	public class SpeedBonus : Bonus
	{
		//Сила увеличения скорости
		[SerializeField]
		private float _speedPower;

		//Длительность уеличения скорости
		[SerializeField]
		private float _duration;

		//Применение бонуса к получателю
		public override void ApplyBonus(IBonusRecipient recipient)
		{
			recipient.IncreaseSpeed(_speedPower, _duration);
			Observer.Instance().ObectOutdated.Invoke(gameObject);
		}
	}
}
