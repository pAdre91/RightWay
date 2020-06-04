using Gameplay.Spaceships.CustomSpaceships;

namespace Gameplay.Bonuses
{
	interface IBonus
	{
		void ApplyBonus(PlayerSpaceship playerSpaceship);
	}
}