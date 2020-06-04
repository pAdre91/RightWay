namespace Gameplay.Weapons
{
	public interface IDamageDealer
	{
		//Дружественность объекта
		UnitBattleIdentity BattleIdentity { get; }

		//Урон объекта
		float Damage { get; }
	}
}
