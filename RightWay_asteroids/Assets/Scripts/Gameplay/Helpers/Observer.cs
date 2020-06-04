using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Helpers
{
	public class Event<T> : UnityEvent<T>
	{
	}

	public class Observer
	{
		//Singleton
		private static Observer instanceHolder = null;

		//Инстанцирование объекта
		public static Observer Instance()
		{
			if (instanceHolder != null)
				return instanceHolder;
			return instanceHolder = new Observer();
		}

		//передача вознаграждения за сбитую цель
		public Event<float> DownEnemyWithReward;
		//Передача счета сбитого игрока
		public Event<float> PlayerDeadWithScore;
		//Передача устаревшего объекта
		public Event<GameObject> ObectOutdated;
		//передача GO сбитой цели
		public Event<GameObject> EnemyDown;
		//Событие смерти игрока
		public UnityEvent PlayerDead;
		//Событие перезагрузки уровня
		public UnityEvent RestartLevel;

		//конструктор
		private Observer()
		{
			InitEvents();
		}

		//Инициализация событий
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
