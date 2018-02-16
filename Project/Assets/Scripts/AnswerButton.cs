using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
	public Text answerText;
	private GameController gameController;
	public AnswerData answerData;

 
	void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}

	public void Setup(AnswerData data)
	{
		answerData = data;
		answerText.text = answerData.answerText;
	}

	public void HanddleClick()
	{
		gameController.AnswerButtonClick(answerData.isCorrect);
	}
}
