using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour 
{
	public RoundData[] allRoundData;
	private PlayerProgress playerProgress;

	void Start()
	{	
		DontDestroyOnLoad(gameObject);
		LoadPlayerProgress();
		SceneManager.LoadScene("MenuScreen");
	}

	public RoundData GetCurrentRoundData()
	{
		return allRoundData[0];
	}

	public void SubmitNewPlayerScore(int newScore)
	{
		if(newScore > playerProgress.hightestScore)
		{
			playerProgress.hightestScore = newScore;
			SavePlayerProgress();
		}
	}

	public int GetHightestScore()
	{
		return playerProgress.hightestScore;
	}

	private void LoadPlayerProgress()
	{
		playerProgress = new PlayerProgress();

		if(PlayerPrefs.HasKey("highterScore"))
		{
			playerProgress.hightestScore = PlayerPrefs.GetInt("highterScore");
		}
	}

	private void SavePlayerProgress()
	{
		PlayerPrefs.SetInt("highterScore",playerProgress.hightestScore);
	}
}
