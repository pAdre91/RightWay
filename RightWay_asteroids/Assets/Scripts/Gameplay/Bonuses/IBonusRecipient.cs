namespace Gameplay.Bonuses
{
	public interface IBonusRecipient
	{
		//увеличение скорости при поднятии бонуса скорости
		void IncreaseSpeed(float speedPower, float duration);
		//Увеличение здоровья при поднятии аптечки
		void Healing(float heal);
	}
}