using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    public int Score => _score;

    [SerializeField]
    [Header("獲得できるスコア")]
    int _addScore = 100;

    int _score; //引き継ぐスコア

    public int PlusScore() => _score += _addScore;
}
