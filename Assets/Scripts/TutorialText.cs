using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField][TextArea] private string[] instructions; 
    [SerializeField] private bool[] requiresButtonClick;      

    [Header("Buttons to Highlight")]
    [SerializeField] private Button recordButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button playButton;

    [Header("Highlight Settings")]
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private float highlightScale = 1.2f;
    [SerializeField] private float highlightDuration = 0.5f;

    private int currentStep = 0;
    private bool isTyping = false;

    private void Start()
    {
        recordButton.onClick.AddListener(() => OnButtonClick(recordButton));
        stopButton.onClick.AddListener(() => OnButtonClick(stopButton));
        playButton.onClick.AddListener(() => OnButtonClick(playButton));

        StartCoroutine(TypeInstruction());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping && !requiresButtonClick[currentStep])
        {
            NextStep();
        }
    }

    private IEnumerator TypeInstruction()
    {
        isTyping = true;
        instructionText.text = "";

        foreach (char letter in instructions[currentStep])
        {
            instructionText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        isTyping = false;

        if (requiresButtonClick[currentStep])
        {
            HighlightButton(currentStep);
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        if (IsCorrectButton(clickedButton))
        {
            NextStep();
        }
    }

    private bool IsCorrectButton(Button clickedButton)
    {
        switch (currentStep)
        {
            case 1: 
                return clickedButton == recordButton;
            case 2: 
                return clickedButton == stopButton;
            case 3: 
                return clickedButton == playButton;
            default:
                return false;
        }
    }

    private void NextStep()
    {
        currentStep++;

        if (currentStep < instructions.Length)
        {
            StartCoroutine(TypeInstruction());
        }
     
    }

    private void HighlightButton(int step)
    {
        switch (step)
        {
            case 1:
                StartCoroutine(HighlightEffect(recordButton));
                break;
            case 3:
                StartCoroutine(HighlightEffect(stopButton));
                break;
            case 5:
                StartCoroutine(HighlightEffect(playButton));
                break;
        }
    }

    private IEnumerator HighlightEffect(Button button)
    {
        Image buttonImage = button.GetComponent<Image>();
        Color originalColor = buttonImage.color;
        Vector3 originalScale = button.transform.localScale;

        for (int i = 0; i < 3; i++)
        {
            buttonImage.color = highlightColor;
            button.transform.localScale = originalScale * highlightScale;
            yield return new WaitForSeconds(highlightDuration);

            buttonImage.color = originalColor;
            button.transform.localScale = originalScale;
            yield return new WaitForSeconds(highlightDuration);
        }
    }
}
