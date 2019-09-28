using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Score Calculator")]
public class ScoreCalculator : ScriptableObject
{
    public void CalculateScore(Player scorer)
    {
        if (!scorer || !scorer.Score)
        {
            return;
        }

        int ScoreToSum = (scorer.IsScoreDirty ? GameData.DirtyScoreValue : GameData.CleanScoreValue) + scorer.CurrentLaunchBonus;
        scorer.Score.Value += (scorer.IsFireballActive) ? ScoreToSum * GameData.FireballScoreMultiplier : ScoreToSum;

        scorer.IsScoreDirty = false;
    }
}