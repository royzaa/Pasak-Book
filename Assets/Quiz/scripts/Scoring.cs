using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    int numOfCorrectAnswer = 0;
    int numofSeenQuestion = 0;

    public void incrementCorrectAnswer()
    {
        numOfCorrectAnswer++;
    }

    public void incrementSeenQuestion()
    {
        numofSeenQuestion++;
    }

    public int calculateScore()
    {
        float finalScore = numOfCorrectAnswer / (float)numofSeenQuestion * 100;
        Debug.Log(numOfCorrectAnswer);
        Debug.Log(numofSeenQuestion);
        Debug.Log(finalScore);
        return ((int)finalScore);
    }
}
