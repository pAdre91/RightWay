using UnityEngine;

namespace Gameplay.Helpers
{
	public static class GameAreaHelper
	{
		//Ссылка на камеру
		private static Camera _camera;

		//Инициализация ссылки на камеру
		static GameAreaHelper()
		{
			_camera = Camera.main;
		}

		//Проверят находится ли объект или его часть на сцене
		public static bool IsInGameplayArea(Transform objectTransform, Bounds objectBounds)
		{
			GetCameraBounds(out float topBound, out float bottomBound, out float leftBound, out float rightBound);

			var objectPos = objectTransform.position;

			return (objectPos.x - objectBounds.extents.x < rightBound)
				&& (objectPos.x + objectBounds.extents.x > leftBound)
				&& (objectPos.y - objectBounds.extents.y < topBound)
				&& (objectPos.y + objectBounds.extents.y > bottomBound);
		}

		//Проверяет полностью ли объект находится на сцене
		public static bool IsAllObjectInGameplayArea(Transform objectTransform, Bounds objectBounds)
		{
			GetCameraBounds(out float topBound, out float bottomBound, out float leftBound, out float rightBound);

			var objectPos = objectTransform.position;

			return (objectPos.x < rightBound - objectBounds.extents.x)
				&& (objectPos.x > leftBound + objectBounds.extents.x)
				&& (objectPos.y < topBound - objectBounds.extents.y)
				&& (objectPos.y > bottomBound + objectBounds.extents.y);
		}

		//нахождение границ видимой камерой области
		private static void GetCameraBounds(out float top, out float bottom, out float left, out float right)
		{
			var camHalfHeight = _camera.orthographicSize;
			var camHalfWidth = camHalfHeight * _camera.aspect;
			var camPos = _camera.transform.position;

			top = camPos.y + camHalfHeight;
			bottom = camPos.y - camHalfHeight;
			left = camPos.x - camHalfWidth;
			right = camPos.x + camHalfWidth;
		}
	}
}
