#pragma warning disable CS0649

using UnityEngine;

namespace Gameplay.ShipSystems
{
	public class MovementSystem : MonoBehaviour
	{
		//Скорость горизонтального движения
		[SerializeField]
		private float _lateralMovementSpeed;

		//Скорость вертикального движения
		[SerializeField]
		private float _longitudinalMovementSpeed;

		//обработка горизонтального движения
		public void LateralMovement(float amount, bool left = false)
		{
			Vector3 movingSide;

			if (!left)
			{
				movingSide = Vector3.right;
			}
			else
			{
				movingSide = Vector3.left;
			}

			Move(amount * _lateralMovementSpeed, movingSide);
		}

		//Обработка вертикального движения
		public void LongitudinalMovement(float amount)
		{
			Move(amount * _longitudinalMovementSpeed, Vector3.up);
		}

		//Процесс передвижения(минителепортации)
		protected virtual void Move(float amount, Vector3 axis)
		{
			transform.Translate(amount * axis.normalized);
		}
	}
}
