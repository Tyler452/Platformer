using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI coinsText;

    private int points = 0;
    private int coins = 0;
    private int timeLeft = 100;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    void Update()
    {
        timeLeft = Mathf.Max(0, 100 - (int)Time.time);
        timerText.text = $"Time: {timeLeft}";

        if (timeLeft == 0)
        {
            Debug.Log("Player Failed");
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdatePointsUI();
        Debug.Log("Points added: " + amount + ". Total points: " + points);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinsUI();
        Debug.Log("Coins added: " + amount + ". Total coins: " + coins);
    }

    private void UpdatePointsUI()
    {
        if (pointsText != null)
        {
            pointsText.text = $"Points: {points}";
        }
        else
        {
            Debug.LogError("Points Text is not assigned!");
        }
    }

    private void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = $"Coins: {coins}";
        }
        else
        {
            Debug.LogError("Coins Text is not assigned!");
        }
    }
}