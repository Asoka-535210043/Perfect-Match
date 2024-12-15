using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDesign : MonoBehaviour
{
    int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
    }

    public int GetScore()
    {
        return score;
    }
}
