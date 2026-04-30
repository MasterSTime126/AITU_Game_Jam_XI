using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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

    [Header("Some extra items heree")]
    [SerializeField] private GameObject buttonsPanel;

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
                AddCurrentQuestionIndex();
                if (currentQuestionIndex < 5)
                {
                    GenerateQuestion();
                    currentInput = "";
                    ClearInput();
                }
                else
                {
                    //Add condition of level clear here
                    //Also add win animation here, which processess this one:
                    //SceneManager.LoadScene("_Main_Gacha");
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

    private void AddCurrentQuestionIndex()
    {
        currentQuestionIndex++;
        switch (currentQuestionIndex)
        {
            case 2:
                buttonsPanel.transform.rotation = Quaternion.Euler(0, 0, -180);
                break;
            case 3:
                buttonsPanel.transform.rotation = Quaternion.identity;
                break;
            case 4:
                buttonsPanel.transform.rotation = Quaternion.identity;
                break;

        }
    }

    private void Update()
    {
        if(currentQuestionIndex == 3)
        {
            buttonsPanel.transform.Rotate(0, 0, 20 * Time.deltaTime);
        }
        if(currentQuestionIndex == 4)
        {
            MoveAndClampButton(button1);
            MoveAndClampButton(button2);
            MoveAndClampButton(button3);
            MoveAndClampButton(button4);
            MoveAndClampButton(button5);
            MoveAndClampButton(button6);
            MoveAndClampButton(button7);
            MoveAndClampButton(button8);
            MoveAndClampButton(button9);
            MoveAndClampButton(button0);
            MoveAndClampButton(buttonClear);
            MoveAndClampButton(buttonEnter);
        }
    }

    // Moves the button randomly and clamps its position to stay within the screen bounds
    private void MoveAndClampButton(Button btn)
    {
        // Move randomly
        btn.transform.position = (Vector3)Random.insideUnitCircle * 25 + btn.transform.position;

        // Clamp to screen bounds
        Vector3 pos = btn.transform.position;
        Vector3 min = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        // If using Canvas in Screen Space - Overlay, clamp using Screen.width/height directly
        if (btn.transform is RectTransform rectTransform)
        {
            float width = rectTransform.rect.width * rectTransform.lossyScale.x;
            float height = rectTransform.rect.height * rectTransform.lossyScale.y;
            float minX = width / 2f;
            float maxX = Screen.width - width / 2f;
            float minY = height / 2f;
            float maxY = Screen.height - height / 2f;
            Vector3 screenPos = rectTransform.position;
            screenPos.x = Mathf.Clamp(screenPos.x, minX, maxX);
            screenPos.y = Mathf.Clamp(screenPos.y, minY, maxY);
            rectTransform.position = screenPos;
        }
        else
        {
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            btn.transform.position = pos;
        }
    }
}