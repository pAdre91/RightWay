using UnityEngine;

namespace Gameplay.Weapons.Projectiles.CustomProjectiles
{
	public class LaserBeam : Projectile
	{
		protected override void Move(float speed)
		{
			transform.Translate(speed * Time.deltaTime * Vector3.up);
		}
	}
}
