using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{

    [SerializeField]
    [Header("�X�R�A")]int _score; //�����p���X�R�A
    
    int _resultScore; //�����p�����X�R�A

    public int Score => _score;

    public void PlusScore()//���������̃X�R�A���Z
    {
        _score += 100;
    }
    public void ResultScore()//���U���g���̃X�R�A�����p��
    {
        _resultScore = _score;
    }
}
