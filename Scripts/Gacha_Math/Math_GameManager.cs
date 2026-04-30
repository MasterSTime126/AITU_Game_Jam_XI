using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Math_GameManager : MonoBehaviour
{
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;
    [SerializeField] private Button button5;
    [SerializeField] private Button button6;
    [SerializeField] private Button button7;
    [SerializeField] private Button button8;
    [SerializeField] private Button button9;
    [SerializeField] private Button button0;
    [SerializeField] private Button buttonClear;
    [SerializeField] private Button buttonEnter;

    [SerializeField] private TMP_Text inputText;
    [SerializeField] private TMP_Text questionText;

    private string currentInput = "";
    private int correctAnswer;
    private int currentQuestionIndex = 0;

    private void Start()
    {
        GenerateQuestion();
        button1.onClick.AddListener(() => AppendInput("1"));
        button2.onClick.AddListener(() => AppendInput("2"));
        button3.onClick.AddListener(() => AppendInput("3"));
        button4.onClick.AddListener(() => AppendInput("4"));
        button5.onClick.AddListener(() => AppendInput("5"));
        button6.onClick.AddListener(() => AppendInput("6"));
        button7.onClick.AddListener(() => AppendInput("7"));
        button8.onClick.AddListener(() => AppendInput("8"));
        button9.onClick.AddListener(() => AppendInput("9"));
        button0.onClick.AddListener(() => AppendInput("0"));
        buttonClear.onClick.AddListener(ClearInput);
        buttonEnter.onClick.AddListener(CheckAnswer);
    }

    private void AppendInput(string value)
    {
        if(currentInput.Length >= 5) return;
        currentInput += value;
        inputText.text = currentInput;
    }

    private void ClearInput()
    {
        currentInput = "";
        inputText.text = currentInput;
    }

    private void CheckAnswer()
    {
        if (int.TryParse(currentInput, out int userAnswer))
        {
            if (userAnswer == correctAnswer)
            {
                currentQuestionIndex++;
                if (currentQuestionIndex < 5)
                {
                    GenerateQuestion();
                    currentInput = "";
                    ClearInput();
                }
                else
                {
                    //Add condition of level clear here
                    SceneManager.LoadScene("_Main_Gacha");
                }
            }
            else
            {
                ClearInput();
            }
        }
        else
        {
            ClearInput();
        }
    }

    private void GenerateQuestion()
    {
        int num1 = Random.Range(5, 100);
        int num2 = Random.Range(5, 100);
        correctAnswer = num1 + num2;
        questionText.text = $"{num1} + {num2} = ?";
    }
}