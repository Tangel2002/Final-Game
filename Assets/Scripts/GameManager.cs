using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreDisplay;
    public GameObject gameOverCanv;
    public Transform BloodStain;
    private Transform PlayerTransform;

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
    private void Update()
    {
        ScoreDisplay.text = Score.ToString();
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SetScore(0);
        NewRound();
        Cursor.visible = false;
        
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
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
        bloodstain = false;
        this.BloodStain.gameObject.SetActive(false);
        gameOverCanv.SetActive(false);
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
        Cursor.visible = true;
        gameOverCanv.SetActive(true);
    }

    public void GhostEaten(Ghost ghost) {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.Score + points);
        this.ghostMultiplier++;
    }
    public void PacmanEaten() {
        PlayerTransform = GameObject.Find("Pacman").transform;
        if (this.bloodstain == true)
        {
            GameOver();
        }
        else
        {
            BloodStainDrop();
            pacman.ResetState();
            pacman.gameObject.GetComponent<Movement>().enabled = false;
            bloodstain = true;
            pacman.Invoke(nameof(PacmanScript.ResetState), 0.2f);
            for (int i = 0; i < this.ghosts.Length; i++)
            {
                this.ghosts[i].soul.Enable(999);
            }
            //ghosts will trigger ghost.soul in the same manner as frightened. 
            //Then, when bloodstain is cleared, it is set to false,
            //pellets re-enable, and ghosts enter scatter
        }
    }

    private void BloodStainDrop()
    {
        this.BloodStain.gameObject.SetActive(true);
        BloodStain.position = PlayerTransform.position;
    }

    public void BloodStainPickUp(BloodStain pellet)
    {
        bloodstain = false;
        PelletEaten(pellet);

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].soul.Disable();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(this.Score + pellet.points);

        if (!HasRemainingPellets() && !bloodstain)
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
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
