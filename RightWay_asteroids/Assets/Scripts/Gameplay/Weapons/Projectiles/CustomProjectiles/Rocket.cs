using Gameplay.Weapons.Projectiles;
using UnityEngine;

public class Rocket : Projectile
{
	protected override void Move(float speed)
	{
		transform.Translate(speed * Time.deltaTime * Vector3.up);
	}
}
