using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{

    [SerializeField]
    [Header("スコア")]int _score; //引き継ぐスコア
    
    int _resultScore; //引き継いだスコア

    public int Score => _score;

    public void PlusScore()//揃った時のスコア加算
    {
        _score += 100;
    }
    public void ResultScore()//リザルト時のスコア引き継ぎ
    {
        _resultScore = _score;
    }
}
