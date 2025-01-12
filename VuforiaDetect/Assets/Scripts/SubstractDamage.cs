using UnityEngine;
using TMPro;

public class SubstractDamage : MonoBehaviour
{
    public TMP_InputField inputScore;
    public TMP_Text scoreText;
    

    void Start()
    {
        if (inputScore == null)
        {
            Debug.LogError("inputScore is not assigned!");
            return;
        }
        if (scoreText == null)
        {
            Debug.LogError("inputScore is not assigned!");
            return;
        }

        inputScore.onSubmit.AddListener(HandleSubmit);
    }

    // Function to handle the Enter key press
    private void HandleSubmit(string inputText)
    {
        if (string.IsNullOrEmpty(inputText))
        {
            Debug.LogWarning("Input field is empty.");
            return;
        }

        // Ensure scoreText contains a valid number
        if (int.TryParse(scoreText.text, out int currentScore) && int.TryParse(inputText, out int scoreToSubtract))
        {
            // Subtract the input score from the current score
            int newScore = currentScore - scoreToSubtract;
            scoreText.text = newScore.ToString();

            // Clear the input field
            inputScore.text = "";
        }
        else
        {
            Debug.LogError("Invalid input! Both scoreText and inputText must be numeric.");
        }
    }
}
