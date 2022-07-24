using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    public float TimeLimit => _timeLimit; 

    [SerializeField]
    [Header("制限時間")]
    float _timeLimit; 

    void Update()
    {
        //テキストに反映するためのスクリプトを書く

        if (_timeLimit <= 0)
        {
            _timeLimit = 0;
            //リザルト画面を開く
        }
        else
        {
            _timeLimit -= Time.deltaTime;
        }
    }
}
