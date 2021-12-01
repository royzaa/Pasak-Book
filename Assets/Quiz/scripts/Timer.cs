using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField] float TimeToCompleteQuestion = 30f;
    [SerializeField] float TimeToReviewQuestion = 8f;

    [SerializeField] TextMeshProUGUI timerText;

    public bool loadQuestion;
    public float fillFraction;
    float timerValue;
    public bool isAnsweringQuestion;


    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / TimeToCompleteQuestion;
                timerText.text = $"Sisa waktu {(int)timerValue} detik";

            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = TimeToReviewQuestion;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / TimeToReviewQuestion;
                timerText.text = $"Menuju soal selanjutnya {(int)timerValue} detik";
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = TimeToCompleteQuestion;
                loadQuestion = true;
            }
        }
    }
}
