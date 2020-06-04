namespace Gameplay.Bonuses
{
	public interface IBonusRecipient
	{
		void IncreaseSpeed(float speedPower, float duration);
		void Healing(float heal);
	}
}