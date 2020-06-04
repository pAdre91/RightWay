using Gameplay.ShipSystems;

namespace Gameplay.Spaceships
{
	public interface ISpaceship
	{
		//Система движения корабля
		MovementSystem MovementSystem { get; }
		//Оружейная система корабля
		WeaponSystem WeaponSystem { get; }
	}
}
