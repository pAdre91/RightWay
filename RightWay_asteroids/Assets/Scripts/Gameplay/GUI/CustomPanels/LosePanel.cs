using Gameplay.Helpers;

namespace Gameplay.GUI.CustomPanels
{
	public class LosePanel : EndGamePanel
	{
		//Ссылка на наблюдателя
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

		//Обновление панели и данных
		private void RefreshPanel(float score)
		{
			TurnOnPanel();
			SetInfo(score.ToString());
		}

		//Подписка на события
		private void Subscribe()
		{
			_observer.PlayerDeadWithScore.AddListener(RefreshPanel);
			_observer.RestartLevel.AddListener(TurnOffPanel);
		}

		//Отписка от событий
		private void UnSubscribe()
		{
			_observer.PlayerDeadWithScore.RemoveListener(RefreshPanel);
			_observer.RestartLevel.RemoveListener(TurnOffPanel);
		}
	}
}
