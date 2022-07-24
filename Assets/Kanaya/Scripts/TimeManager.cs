using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    public float TimeLimit => _timeLimit;

    bool Music = false;
    [SerializeField]
    [Header("制限時間")]
    float _timeLimit; 

    void Update()
    {
        //テキストに反映するためのスクリプトを書く

        if (_timeLimit <= 0)
        {
            _timeLimit = 0;
            UIManager.Instance.ResultScore(ScoreManager.Instance.Score);
            //リザルト画面を開く
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
