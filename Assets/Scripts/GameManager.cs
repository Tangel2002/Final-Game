using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public PacmanScript pacman;
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
        bloodstain = false;
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
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
        pacman.ResetState();
        pacman.gameObject.GetComponent<Movement>().enabled = false;
        if (this.bloodstain == true)
        {
            GameOver();
        }
        else
        {
            bloodstain = true;
            pacman.Invoke(nameof(PacmanScript.ResetState), 0.2f); 
            //pacman will reset his state and pellets will become uninteractable,
            //ghosts will trigger ghost.soul in the same manner as frightened, 
            //deactivating all other scripts. Then, when bloodstain is cleared, it is set to false,
            //pellets re-enable, and ghosts enter scatter
        }
    }
    private void SetScore(int Score)
    {
        this.Score = Score;
    }
}
