
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI  question;
    [SerializeField] List<QuizObject> AllQuestions = new List<QuizObject>();
    [SerializeField] GameObject quizGameObject;

    [SerializeField] GameObject quizPrefab;
    QuizObject currentQuestion;

    [Header("Answer")]
    [SerializeField] GameObject[] answers;

    [Header("Button Sprite")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image TimerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] GameObject EndScreen;
    Scoring scoring;
    

    [Header("Progress Bar")]
    [SerializeField] Slider ProgressBar;

    [Header("AR")]
    [SerializeField] GameObject ARSession;
    [SerializeField] GameObject ARSessionOrigin;

    bool hasAnsweredEarly;
    public bool isComplete;
    public GameObject newQuizInstance;

    void Start()
    {
        newQuizInstance = Instantiate(quizPrefab);
        newQuizInstance.SetActive(false);
        timer = FindObjectOfType<Timer>();
        scoring = FindObjectOfType<Scoring>();
        timer.loadQuestion = true;
        ProgressBar.maxValue = AllQuestions.Count;
        ProgressBar.value = 0;
        ARSession.SetActive(false);
        ARSessionOrigin.SetActive(false);
        

    }

    public  void OnReset() {
        {
            timer = FindObjectOfType<Timer>();
            scoring = FindObjectOfType<Scoring>();
            timer.loadQuestion = true;
            ProgressBar.maxValue = AllQuestions.Count;
            ProgressBar.value = 0;
            ARSession.SetActive(false);
            ARSessionOrigin.SetActive(false);
        }
    }

    private void Update() 
    {
        TimerImage.fillAmount = timer.fillFraction;
        if (isComplete)
        {
            quizGameObject.SetActive(false);
            EndScreen.SetActive(true);

        }
        
        if (timer.loadQuestion)
        {
            if (ProgressBar.value == ProgressBar.maxValue)
            {
                isComplete = true;
                Debug.Log("hitted");
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayButtons(-1);
            SetButtonState(false);
        }
        
    }

    void DisplayQuestion()
    {
         question.text = currentQuestion.getQuestion;

        for (int i = 0; i < answers.Length; i++)
        {
            TextMeshProUGUI buttonAnswer = answers[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonAnswer.text = currentQuestion.getAnswer(i);
        }
    }

    void GetNextQuestion()
    {
        
        if (AllQuestions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultSprite();
            GetRandomQuestion();
            DisplayQuestion();
            scoring.incrementSeenQuestion();
            ProgressBar.value++;
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, AllQuestions.Count);
        currentQuestion = AllQuestions[index];
        if (AllQuestions.Contains(currentQuestion))
        {
            AllQuestions.Remove(currentQuestion);
        }
    }

    public void OnSelectedAnswer(int answerIndex)
    {
        hasAnsweredEarly = true;
        DisplayButtons(answerIndex);
        SetButtonState(false);
        timer.CancelTimer();
       
    }

    void DisplayButtons(int answerIndex)
    {
        if (answerIndex == currentQuestion.getCorrectAnswerIndex)
        {
            Image buttonImage = answers[answerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            question.text = "Horee kamu benar!";
            scoring.incrementCorrectAnswer();

        }
        else
        {
            if (hasAnsweredEarly)
            {
                Image buttonImage = answers[answerIndex].GetComponent<Image>();
                buttonImage.sprite = defaultAnswerSprite;
            }
            string correctAnswer = answers[currentQuestion.getCorrectAnswerIndex].GetComponentInChildren<TextMeshProUGUI>().text;
            question.text = $"Yah sedih, jawaban yang benar adalah:\n{correctAnswer}";
        }
    }
    void SetButtonState(bool state)
    {
        for (int i = 0; i < answers.Length; i++)
        {

            Button button = answers[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultSprite()
    {
        for (int i = 0; i < answers.Length; i++)
        {

            Image buttonImage = answers[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

}
