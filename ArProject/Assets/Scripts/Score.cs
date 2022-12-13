using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _textScore;
    [SerializeField] private int _score = 0;

    public void UpdateScore(int score)
    {
        _score += score;
        _textScore.text = _score.ToString();
    }
}
