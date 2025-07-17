using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<int> playerScores;
    [SerializeField] private List<TextMeshProUGUI> playerScoreTexts;

    private static GameManager instance;
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    /// <summary>
    /// Increase a player's score by one, then updated the displayed score
    /// </summary>
    /// <param name="playerType">The player who's score is being increased</param>
    public static void IncrementScore(PlayerType playerType)
    {
        if (instance == null)
        {
            return;
        }

        if (instance.playerScoreTexts.Count <= (int)playerType)
        {
            return;
        }
        
        instance.playerScores[(int) playerType]++;
        TextMeshProUGUI scoreText = instance.playerScoreTexts[(int)playerType];
        scoreText.text = instance.playerScores[(int)playerType].ToString();
    }

}
