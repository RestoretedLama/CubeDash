using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI LifeCountDown;
    public TextMeshProUGUI gameOverText;
    public GameObject  titleScreen;
    public bool isGameActive;
    
    private int score;
    private int LifeCount;
    private int MaxLife;
    
    private float initialSpawnRate = 2.0f;
    private float minSpawnRate = 0.5f;
    private float spawnRateDecrease = 0.1f;
    private float currentSpawnRate;

    void Start()
    {

        
    }

    void Update()
    {
        
        if (LifeCount < 0)
        {
            GameOver();
            LifeCount = 0;
            LifeCounter(0);
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(currentSpawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

            // Hýzý güncelle
            currentSpawnRate -= spawnRateDecrease;
            currentSpawnRate = Mathf.Max(currentSpawnRate, minSpawnRate);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void LifeCounter(int LifeToRemove)
    {
            LifeCount += LifeToRemove;

            LifeCount = Mathf.Min(LifeCount, MaxLife); // LifeCount, MaxLife'tan büyük olamaz

            LifeCountDown.text = "" + LifeCount;
        
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true; //ilk bu olmalý
        StartCoroutine(SpawnTarget());
        LifeCount = 0;
        LifeCounter(25);
        score = 0;
        UpdateScore(0);
        currentSpawnRate = initialSpawnRate;

        spawnRateDecrease = spawnRateDecrease / difficulty;

        MaxLife = 25;
        LifeCount = MaxLife; // LifeCount'u baþlangýçta MaxLife olarak ayarlayýn
        LifeCountDown.text = "" + LifeCount;
        titleScreen.gameObject.SetActive(false);
    }
}
