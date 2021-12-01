
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoringText;
    [SerializeField] GameObject quiz;

    Scoring scoring;
    GameObject newQuiz;
    void Start()
    {
        scoring = FindObjectOfType<Scoring>();
        int finalScore = scoring.calculateScore();
        scoringText.text = $"Nilai tantanganmu sebesar {finalScore}";
        newQuiz = FindObjectOfType<Quiz>().newQuizInstance;
    }

    public void ReloadQuiz()
    {
        quiz.SetActive(false);  
        newQuiz.SetActive(true);
        Destroy(quiz);
        quiz = newQuiz;
        newQuiz.GetComponentInChildren<Quiz>().OnReset();

    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
