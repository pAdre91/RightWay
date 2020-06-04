namespace Gameplay.Weapons
{
	public interface IDamagable
	{
		//Дружественность объекта
		UnitBattleIdentity BattleIdentity { get; }

		//Обработка процесса получения урона
		void ApplyDamage(IDamageDealer damageDealer);
	}

	//Ванианты дружественности объекта
	public enum UnitBattleIdentity
	{
		Neutral,
		Ally,
		Enemy
	}
}
