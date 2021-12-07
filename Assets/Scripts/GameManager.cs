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
    public int ghostMultiplier { get; private set; } = 1;

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
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.Score + points);
        this.ghostMultiplier++;
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
            Invoke(nameof(ResetState), 3.0f); //pacman.ResetState
            //pacman will reset his state and pellets will become uninteractable,
            //ghosts will trigger ghost.soul in the same manner as frightened, 
            //deactivating all other scripts. Then, when bloodstain is cleared, it is set to false,
            //pellets re-enable, and ghosts enter scatter
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(this.Score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        //change ghost state
    }

    private bool HasRemainingPellets()
    {

        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
    private void SetScore(int Score)
    {
        this.Score = Score;
    }
}
