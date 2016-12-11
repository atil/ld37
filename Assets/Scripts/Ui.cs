using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Ui : MonoBehaviour
{
    public Action RestartTrigger;

    public Text ScoreText;
    public Text HiscoreText;
    public Text GameOverText;

    public BlurOptimized Blur;
    public SepiaTone Sepia;

    void Start()
    {
        enabled = false;
    }

    public void UpdateScore(int score, int hiscore)
    {
        ScoreText.text = score.ToString();
        HiscoreText.text = "Hi-score: " + hiscore;
    }

    public void GameOver(int score, int hiscore)
    {
        ScoreText.text = score.ToString();
        HiscoreText.text = "Hi-score: " + hiscore;

        GameOverText.gameObject.SetActive(true);
        Blur.enabled = true;
        Sepia.enabled = true;
        enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameOverText.gameObject.SetActive(false);
            Blur.enabled = false;
            Sepia.enabled = false;
            enabled = false;

            RestartTrigger();
        }
    }
}
