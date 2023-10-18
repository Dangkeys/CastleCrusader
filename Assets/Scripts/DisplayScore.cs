using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        ShowScore();
    }

    private void ShowScore()
    {
        scoreText.text = "Score: "+ scoreKeeper.Score.ToString("000000000");
    }


}
