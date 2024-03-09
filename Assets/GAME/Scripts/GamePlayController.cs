using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    [SerializeField] private Button instructionButton;
    [SerializeField] private Text scoreText, yourScoreText, highScoreText;
    [SerializeField] private GameObject gameOverPanel, pausePanel;
    private void Awake()
    {
        Time.timeScale = 0;
        MakeInstance();
    }

    void MakeInstance()
    {
        if(instance == null) 
            instance = this;
    }

    public void InstructionButton()
    {
        Time.timeScale = 1;
        instructionButton.gameObject.SetActive(false);
    }

    public void SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void _BirdDiedShowPanel(int score)
    {
        gameOverPanel.SetActive(true);

        yourScoreText.text = " " + score;
        if(score > GameManager.instance.GetHighScore())
        {
            GameManager.instance.SetHighScore(score);
        }
        highScoreText.text = " " + GameManager.instance.GetHighScore();
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReStartGameButton()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
