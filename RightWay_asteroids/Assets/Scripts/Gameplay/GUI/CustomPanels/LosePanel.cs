﻿using Gameplay.Helpers;

namespace Gameplay.GUI.CustomPanels
{
	public class LosePanel : EndGamePanel
	{
		private void Start()
		{
			Subscribe();
			TurnOffPanel();
		}

		private void OnDestroy()
		{
			UnSubscribe();
		}

		private void RefreshPanel(float score)
		{
			TurnOnPanel();
			SetInfo(score.ToString());
		}

		private void Subscribe()
		{
			Observer.Instance().PlayerDeadWithScore.AddListener(RefreshPanel);
			Observer.Instance().RestartLevel.AddListener(TurnOffPanel);
		}

		private void UnSubscribe()
		{
			Observer.Instance().PlayerDeadWithScore.RemoveListener(RefreshPanel);
			Observer.Instance().RestartLevel.RemoveListener(TurnOffPanel);
		}
	}
}