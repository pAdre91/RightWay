using Gameplay.Helpers;

namespace Gameplay.GUI.CustomPanels
{
	public class LosePanel : EndGamePanel
	{
		private Observer _observer = Observer.Instance();

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
			_observer.PlayerDeadWithScore.AddListener(RefreshPanel);
			_observer.RestartLevel.AddListener(TurnOffPanel);
		}

		private void UnSubscribe()
		{
			_observer.PlayerDeadWithScore.RemoveListener(RefreshPanel);
			_observer.RestartLevel.RemoveListener(TurnOffPanel);
		}
	}
}
