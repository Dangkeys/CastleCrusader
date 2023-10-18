using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    
    [Range(0, float.MaxValue)] int score;
    public int Score{get{return score;}}
    const float scoreUpdateTime = 10;
    float scoreRunningTime = 0;
    static ScoreKeeper instance;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    public void ResetScore()
    {
        score = 0;
    }
    private void Update() {
        scoreRunningTime += Time.deltaTime;
        if(scoreRunningTime >= scoreUpdateTime)
        {
            scoreRunningTime = 0;
            AddScore(10);
        }
    }
    public void AddScore(int amount){score += amount;}
}
