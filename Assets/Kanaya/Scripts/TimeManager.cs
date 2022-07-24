using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    public float TimeLimit => _timeLimit;

    bool Music = false;
    [SerializeField]
    [Header("��������")]
    float _timeLimit; 

    void Update()
    {
        //�e�L�X�g�ɔ��f���邽�߂̃X�N���v�g������

        if (_timeLimit <= 0)
        {
            _timeLimit = 0;
            UIManager.Instance.ResultScore(ScoreManager.Instance.Score);
            //���U���g��ʂ��J��
            UIManager.Instance.ResultSetActive(true);

            if(Music == false)
            {
                Music = true;
                SoundManager.Instance.PlayResultMusic();
            }
            
        }
        else
        {
            _timeLimit -= Time.deltaTime;
        }

        UIManager.Instance.Timer(_timeLimit);
    }
}
