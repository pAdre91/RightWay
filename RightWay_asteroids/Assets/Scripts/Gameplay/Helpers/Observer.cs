using UnityEngine.Events;

namespace Gameplay.Helpers
{
	public class Event<T> : UnityEvent<T>
	{
	}

	public class Observer
	{
		private static Observer instanceHolder = null;

		public static Observer Instance()
		{
			if (instanceHolder != null)
				return instanceHolder;
			return instanceHolder = new Observer();
		}

		public Event<float> EnemyDown;

		Observer()
		{
			InitEvents();
		}

		public void InitEvents()
		{
			EnemyDown = new Event<float>();
		}
	}
}
