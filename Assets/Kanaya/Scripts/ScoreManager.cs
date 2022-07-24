using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    public int Score => _score;

    [SerializeField]
    [Header("�l���ł���X�R�A")]
    int _addScore = 100;

    int _score; //�����p���X�R�A

    public int PlusScore() => _score += _addScore;
}
