#pragma warning disable CS0649

using System.Collections;
using Gameplay.Helpers;
using Gameplay.ShipControllers;
using Gameplay.ShipSystems;
using UnityEngine;

public class EnemyShipController : ShipController
{

	[SerializeField]
	private Vector2 _fireDelay;

	private bool _fire = true;

	protected override void ProcessHandling(MovementSystem movementSystem)
	{
		movementSystem.LongitudinalMovement(Time.deltaTime);
	}

	protected override void ProcessFire(WeaponSystem fireSystem)
	{
		if (!_fire)
			return;

		fireSystem.TriggerFire();
		StartCoroutine(FireDelay(Random.Range(_fireDelay.x, _fireDelay.y)));
	}


	private IEnumerator FireDelay(float delay)
	{
		_fire = false;
		yield return new WaitForSeconds(delay);
		_fire = true;
	}

	private void Start()
	{
		Subscribe();
	}

	private void DestroyYourself()
	{
		Destroy(gameObject);
	}

	private void Subscribe()
	{
		Observer.Instance().PlayerDead.AddListener(DestroyYourself);
	}

	private void UnSubscribe()
	{
		Observer.Instance().PlayerDead.RemoveListener(DestroyYourself);
	}

	private void OnDestroy()
	{
		UnSubscribe();
	}
}
