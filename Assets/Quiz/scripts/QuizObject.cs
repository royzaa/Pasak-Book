
using UnityEngine;

[CreateAssetMenu(menuName = "Create Quiz/New Question", fileName = "Question")]

public class QuizObject : ScriptableObject
{
    [SerializeField] [TextArea(2, 8)] string question;

    public string getQuestion => question;

    [SerializeField] string[] answers = new string[4];

    public string getAnswer(int index) => answers[index];

    [SerializeField] int correctAnswerIndex;

    public int getCorrectAnswerIndex => correctAnswerIndex;

}
