using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] garbage;
    public float spawnPositionOffset;
    public int garbageCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public GameObject restartButton;
    public GameObject gameOverText;

    bool gameOver;
    int score;

    void Start()
    {
        gameOver = false;
        restartButton.SetActive(false);
        gameOverText.SetActive(false);
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        gameOver = true;
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (!gameOver)
        {
            for (var i = 0; i < garbageCount; i++)
            {
                var garbageItem = garbage[Random.Range(0, garbage.Length)];
                Instantiate(garbageItem, DetermineSpawnPosition(), Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);
        }
    }

    void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    Vector3 DetermineSpawnPosition()
    {
        var positionX = Random.Range(-1f, 1f);
        var positionY = Random.Range(-1f, 1f);

        var cameraWidth = Camera.main.orthographicSize;
        var cameraHeight = Camera.main.orthographicSize * Camera.main.aspect;

        if (positionX > positionY)
        {
            positionX = positionX * cameraHeight;
            positionY = Mathf.Sign(positionY) * (cameraWidth + spawnPositionOffset);
        }
        else
        {
            positionX = Mathf.Sign(positionX) * (cameraHeight + spawnPositionOffset);
            positionY = positionY * cameraWidth;
        }

        return new Vector3(
            positionX,
            positionY,
            0f
        );
    }
}
