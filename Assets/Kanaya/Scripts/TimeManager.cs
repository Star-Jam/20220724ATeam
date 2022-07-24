using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    public float TimeLimit => _timeLimit; 

    [SerializeField]
    [Header("��������")]
    float _timeLimit; 

    void Update()
    {
        //�e�L�X�g�ɔ��f���邽�߂̃X�N���v�g������

        if (_timeLimit <= 0)
        {
            _timeLimit = 0;
            //���U���g��ʂ��J��
        }
        else
        {
            _timeLimit -= Time.deltaTime;
        }
    }
}
