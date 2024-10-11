using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    public TMP_Text level;
    public ScoreCounter scoreCounter;
    int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        level = GetComponent<TMP_Text>();

        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    public float GetDifficulty()
    {
        if (scoreCounter.score < 2000)
        {
            difficulty = 1;
        }
        else if (scoreCounter.score >= 2000 && scoreCounter.score < 4000)
        {
            difficulty = 2;
        }
        else if (scoreCounter.score >= 4000)
        {
            difficulty = 3;
        }

        return difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        GetDifficulty();
        level.text = $"Level {difficulty}";
    }
}
