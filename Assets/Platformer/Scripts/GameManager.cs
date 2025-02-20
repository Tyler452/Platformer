using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI coinsText;

    private int points = 0;
    private int coins = 0;
    private int timeLeft = 300;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Update time left
        timeLeft = Mathf.Max(0, 300 - (int)Time.time);
        timerText.text = $"Time: {timeLeft}";

        // Update points and coins
        pointsText.text = $"{points}";
        coinsText.text = $"{coins}";
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }
}