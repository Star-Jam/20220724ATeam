using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timelimit : MonoBehaviour
{
    [SerializeField]
    [Header("制限時間")] float _timeLimit; //残り時間

    public float TimeLimit => _timeLimit; 

    void Update()
    {
        _timeLimit -= Time.deltaTime;
        //テキストに反映するためのスクリプトを書く

        if(_timeLimit <= 0)
        {
            _timeLimit = 0;
            //リザルト画面を開く
        }
    }
}
