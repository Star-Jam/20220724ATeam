using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timelimit : MonoBehaviour
{
    [SerializeField]
    [Header("��������")] float _timeLimit; //�c�莞��

    public float TimeLimit => _timeLimit; 

    void Update()
    {
        _timeLimit -= Time.deltaTime;
        //�e�L�X�g�ɔ��f���邽�߂̃X�N���v�g������

        if(_timeLimit <= 0)
        {
            _timeLimit = 0;
            //���U���g��ʂ��J��
        }
    }
}
