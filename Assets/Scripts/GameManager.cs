using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    bool bloodstain;
    public int Score { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        NewRound();
        bloodstain = false;
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(true);
        }

        this.pacman.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    public void GhostEaten(Ghost ghost) {
        SetScore(this.Score + ghost.points);
    }
    public void PacmanEaten() {
        this.pacman.gameObject.SetActive(false);
        if (this.bloodstain == true)
        {
            GameOver();
        }
        else
        {
            bloodstain = true;
            Invoke(nameof(ResetState), 3.0f);
        }
    }
    private void SetScore(int Score)
    {
        this.Score = Score;
    }
}
