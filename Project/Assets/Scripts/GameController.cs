using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public SimpleObjectPool answerButtonObjectPool;
	public Text questionText;
	public Text TimeDisplayText;
	public Text scoreDisplayText;
	public Transform answerButtonParent;
	public GameObject questionDisplay;
	public GameObject roundEndDisplay;
	

	private DataController dataController;
	private RoundData currentRoundData;
	private QuestionData[] questionPool;

	private bool isRoundActive;
	private float timeRemaining;
	private int questionIndex;
	private int playerScore;

	private List<GameObject> answerButtonGameObjects = new List<GameObject>();




	// Use this for initialization
	void Start ()
	{
		dataController = FindObjectOfType<DataController>();
		currentRoundData = dataController.GetCurrentRoundData();
		questionPool = currentRoundData.questions;
		timeRemaining = currentRoundData.timeLimitInSecond;
		UpdateTimeRemainDisplay();
		playerScore = 0;
		questionIndex = 0;

		ShowQuestion();
		isRoundActive = true;
	}

	private void ShowQuestion()
	{
		RemoveAnswerButton();
		QuestionData questionData = questionPool[questionIndex];
		
		// display text question
		questionText.text = questionData.questionText;

		// init answer button
		for (int i = 0; i < questionData.answers.Length; i++)
		{
			GameObject answerButtonGameObject = answerButtonObjectPool.GetObject(); 
			answerButtonGameObject.transform.SetParent(answerButtonParent);

			// add button to array
			answerButtonGameObjects.Add(answerButtonGameObject);

			AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
			answerButton.Setup(questionData.answers[i]);
		}
	}

	private void RemoveAnswerButton()
	{
		while(answerButtonGameObjects.Count > 0)
		{
			answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
			answerButtonGameObjects.RemoveAt(0);
		}
	}

	public void AnswerButtonClick(bool isCorrect)
	{
		if(isCorrect)
		{
			playerScore += currentRoundData.pintsAddedForCorrectAnswer;
			scoreDisplayText.text = "Score : "+playerScore.ToString();
		}

		if(questionPool.Length > questionIndex + 1)
		{
			questionIndex++;
			ShowQuestion();
		}
		else
		{
			//end round
			EndRound();
		}
	}

	public void EndRound()
	{
		// remove answer button 
		RemoveAnswerButton();

		isRoundActive = false;	
		questionDisplay.SetActive(false);
		roundEndDisplay.SetActive(true);
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene("MenuScreen");
	}

	private void UpdateTimeRemainDisplay()
	{
		TimeDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
	}
 
	void Update()
	{
		if(isRoundActive)
		{
			timeRemaining -= Time.deltaTime;
			UpdateTimeRemainDisplay(); 
			if(timeRemaining <= 0)
			{
				EndRound();
			}
		}
	}
}
