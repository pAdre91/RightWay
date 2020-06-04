using Gameplay.Weapons.Projectiles;
using UnityEngine;

namespace Gameplay.Weapons.Projectiles.CustomProjectiles
{
	public class Rocket : Projectile
	{
		//Обработка движения
		protected override void Move(float speed)
		{
			transform.Translate(speed * Time.deltaTime * Vector3.up);
		}
	}
}
