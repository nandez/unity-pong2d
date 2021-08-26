using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton pattern...
    public static GameManager Instance = null;

    // Public members
    public Text scoreRef;
    public GameObject rightWallRef;
    public GameObject leftWallRef;
    public int maxScore = 10;
    public AudioClip backgroundMusic;
    public AudioClip scoreClip;
    public GameObject gameMenuRef;
    public GameObject endGameMenuRef;
    public Text endGameMessageRef;


    // Private members
    private int leftScore = 0;
    private int rightScore = 0;

    private void Awake()
    {
        // Singleton pattern implementation..
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Reset the timescale just in case.
        Time.timeScale = 1;

        // Let the sound manager to handle the music..
        SoundManager.Instance.PlayMusic(this.backgroundMusic);
    }

    private void OnDestroy()
    {
        SoundManager.Instance.StopMusic();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.gameMenuRef.activeSelf)
            {
                // As Menu is active.. we're going to deactivate.
                this.gameMenuRef.SetActive(false);
                this.ResumeGame();
            }
            else
            {
                this.PauseGame();
                this.gameMenuRef.SetActive(true);
            }
        }
    }

    public void Score(GameObject ball, Collision2D wall)
    {
        // Here we check if collision object is right or left wall by checking objects by ref.
        if (wall.gameObject.Equals(this.leftWallRef))
            this.rightScore++;

        else if (wall.gameObject.Equals(this.rightWallRef))
            this.leftScore++;

        // Updates the GUI score element.
        this.scoreRef.text = $"{this.leftScore} - {this.rightScore}";

        // Lets the sound manager to play the score sfx..
        SoundManager.Instance.Play(this.scoreClip);

        if (this.leftScore >= this.maxScore || this.rightScore >= this.maxScore)
        {
            this.PauseGame();

            var side = this.leftScore >= this.maxScore ? "Left" : "Right";
            this.endGameMessageRef.text = $"{side} player won the game!";
            this.endGameMenuRef.SetActive(true);
        }
        else
        {
            ball.GetComponent<Ball>().Initialize(true);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
