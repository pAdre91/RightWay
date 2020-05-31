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
		public Event<float> PlayerDeadWithScore;
		public UnityEvent PlayerDead;

		Observer()
		{
			InitEvents();
		}

		public void InitEvents()
		{
			EnemyDown = new Event<float>();
			PlayerDeadWithScore = new Event<float>();
			PlayerDead = new UnityEvent();
		}
	}
}
