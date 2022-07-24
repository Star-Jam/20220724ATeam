using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    [Header("�p�Y���̃v���t�@�u")]
    GameObject[] _candies;

    //�z��̑傫�����`�B
    int _width = 7;
    int _height = 7;

    //public��GameObject�^�̔z������B

    public GameObject[,] _candyArray = new GameObject[7, 7];

    List<GameObject> deleteList = new List<GameObject>();


    void Start()
    {
        CreateCandies();
    }

    void CreateCandies()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                int number = Random.Range(0, _candies.Length);
                var candy = Instantiate(_candies[number]);
                //��ʂ̌����ڂƂ��āAcandy��transform.position��ݒ�
                candy.transform.position = new Vector2(i/* - _width / 2*/, j/* - _height / 2*/);
                //��ʂ�7�~�V�̕\������C���[�W�ŁA�L�����f�B�̍��W�����̂܂ܔz���Index�ɗ��p���āA�z��̗v�f��Candy�����Ă���B
                _candyArray[i, j] = candy;
            }
        }
        CheckStartset();
    }

    void CheckStartset()
    {
        //���̍s���烈�R�̂Ȃ�����m�F
        for (int i = 0; i < _height; i++)
        {
            //�E����Q�ڈȍ~�͊m�F�s�v�iwidth-2�j
            for (int j = 0; j < _width - 2; j++)
            {
                //�����^�O�̃L�����f�B���R����ł�����B�w���W�����Ȃ̂Œ��ӁB

                //�O�̂��߁A�ӂ��̎����ꂼ����J�b�R�ň͂�ł���B

                if ((_candyArray[j, i].tag == _candyArray[j + 1, i].tag) && (_candyArray[j, i].tag == _candyArray[j + 2, i].tag))
                {
                    //Candy��isMatching��true��
                    _candyArray[j, i].GetComponent<CandyMove>()._isMatching = true;
                    _candyArray[j + 1, i].GetComponent<CandyMove>()._isMatching = true;
                    _candyArray[j + 2, i].GetComponent<CandyMove>()._isMatching = true;
                }
            }
        }

        //���̗񂩂�^�e�̂Ȃ�����m�F

        for (int i = 0; i < _width; i++)
        {
            //�ォ��Q�ڈȍ~�͊m�F�s�v�Bheight-2
            for (int j = 0; j < _height - 2; j++)
            {
                //�x���W�����B
                if ((_candyArray[i, j].tag == _candyArray[i, j + 1].tag) && (_candyArray[i, j].tag == _candyArray[i, j + 2].tag))
                {
                    _candyArray[i, j].GetComponent<CandyMove>()._isMatching = true;
                    _candyArray[i, j + 1].GetComponent<CandyMove>()._isMatching = true;
                    _candyArray[i, j + 2].GetComponent<CandyMove>()._isMatching = true;
                }
            }
        }
    }
}
