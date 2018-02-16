
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData
{
	 public string name;
	 public int timeLimitInSecond;
	 public int pintsAddedForCorrectAnswer;
	 public QuestionData[] questions;
}
