using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inputScore;

    [SerializeField]
    private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        string playername = inputName.text;
        if (int.TryParse(inputScore.text, out int score))
        {
            submitScoreEvent.Invoke(playername, score);
        }
        else
        {
            // Handle the case where inputScore.text is not a valid integer.
            Debug.LogError("Invalid score input: " + inputScore.text);
        }
    }
}
