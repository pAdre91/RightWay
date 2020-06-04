using UnityEngine;
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

		public Event<float> DownEnemyWithReward;
		public Event<float> PlayerDeadWithScore;
		public Event<GameObject> ObectOutdated;
		public Event<GameObject> EnemyDown;
		public UnityEvent PlayerDead;
		public UnityEvent RestartLevel;

		private Observer()
		{
			InitEvents();
		}

		private void InitEvents()
		{
			DownEnemyWithReward = new Event<float>();
			PlayerDeadWithScore = new Event<float>();
			ObectOutdated = new Event<GameObject>();
			EnemyDown = new Event<GameObject>();
			PlayerDead = new UnityEvent();
			RestartLevel = new UnityEvent();
		}
	}
}
