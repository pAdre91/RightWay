using Gameplay.GUI;
using System.Collections;
using System.Collections.Generic;
#pragma warning disable CS0649

using UnityEngine;

namespace Gameplay.Storage
{
	public class UIObjectsStorage : MonoBehaviour
	{
		public static UIObjectsStorage Instance { get; private set; }

		[SerializeField]
		private TextInfoViewer _playerHealth;

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