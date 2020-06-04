using Gameplay.GUI;
using System.Collections;
using System.Collections.Generic;
#pragma warning disable CS0649

using UnityEngine;

namespace Gameplay.Storage
{
	public class UIObjectsStorage : MonoBehaviour
	{
		//Singleton
		public static UIObjectsStorage Instance { get; private set; }

		//Ссылка на объект отображения здоровья игрока
		[SerializeField]
		private TextInfoViewer _playerHealth;

		//Ссылка на объект отображения счета игрока
		[SerializeField]
		private TextInfoViewer _playerScore;

		public TextInfoViewer PlayerHealthUI => _playerHealth;
		public TextInfoViewer PlayerScoreUI => _playerScore;

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
			else
				Destroy(this);
		}
	}
}