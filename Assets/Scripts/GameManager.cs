using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Enemy[] enemies;
    public Pacman pacman;
    public Transform points;
    public int enemyMultiplier = 1;
    public int lives { get; private set; }
    public int score { get; private set; }

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private void Initialize()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void GameOver()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
        resultText.text = "Game Over";
    }

    private void NewRound()
    {
        foreach (Transform point in this.points) {
            point.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        ResetEnemyMultiplier();
        foreach (Enemy enemy in enemies)
        {
            enemy.ResetState();
        }
        this.pacman.ResetState();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = "Score: " + score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "Lives: " + lives.ToString();
    }

    private void ResetEnemyMultiplier()
    {
        this.enemyMultiplier = 1;
    }

    public void EatEnemy(Enemy enemy)
    {

        SetScore(this.score + (enemy.points * enemyMultiplier));
        this.enemyMultiplier++;
    }

    public void EatPacman()
    {
        // if pacman is eaten
        pacman.gameObject.SetActive(false);

        SetLives (this.lives - 1);

        if(this.lives > 0) {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void EatPoint(Point point)
    {
        point.gameObject.SetActive(false);
        SetScore(score + point.points);
        if (!HasRemainingPoints())
        {
            this.pacman.gameObject.SetActive (false);
            Invoke("NewRound", 3f);
        }
    }

    public void EatSuperPoint(SuperPoint point)
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.frightened.Enable(point.influenceDuration);
        }
        Invoke("ResetEnemyMultiplier", point.influenceDuration);
        CancelInvoke();
        EatPoint(point);
    }

    private bool HasRemainingPoints()
    {
        foreach(Transform point in this.points)
        {
            if (point.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public void Update()
    {
        if(this.lives == 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }
}
