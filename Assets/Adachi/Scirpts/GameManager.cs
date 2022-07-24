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
    int _score;

    bool _isStart;
    //public��GameObject�^�̔z������B
    public GameObject[,] _candyArray = new GameObject[7, 7];

    List<GameObject> _deleteList = new List<GameObject>();


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
                    _candyArray[j, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[j + 1, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[j + 2, i].GetComponent<CandyMove>().ChangeIsMatching(true);
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
                    _candyArray[i, j].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[i, j + 1].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[i, j + 2].GetComponent<CandyMove>().ChangeIsMatching(true);
                }
            }
        }

        //isMatching=true�̂��̂��k�������ɓ����
        foreach (var item in _candyArray)
        {
            if (item.GetComponent<CandyMove>().IsMatching)
            {
                _deleteList.Add(item);
            }
        }

        //List���ɃL�����f�B������ꍇ
        if (_deleteList.Count > 0)
        {
            //�Y������z���null�ɂ��āi�����Ǘ��j�A�L�����f�B����������i�����ځj�B
            foreach (var item in _deleteList)
            {
                _candyArray[(int)item.transform.position.x, (int)item.transform.position.y] = null;
                Destroy(item);
            }
            //List������ۂɁB
            _deleteList.Clear();
            //�󗓂ɐV�����L�����f�B������B
            SpawnNewCandy();
        }
        else//List�ɃL�����f�B���Ȃ��ꍇ�B
        {
            //�Q�[���J�n�B
            _isStart = true;
        }
    }

    //�󗓂ɐV�����L�����f�B�𐶐�
    void SpawnNewCandy()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (_candyArray[i, j] == null)
                {
                    int r = Random.Range(0, 5);
                    var candy = Instantiate(_candies[r]);
                    //�����ڂ̏���
                    candy.transform.position = new Vector2(i, j + 0.3f);
                    //�����Ǘ��̏���
                    _candyArray[i, j] = candy;
                }
            }
        }

        if (_isStart == false)
        {
            CheckStartset();
        }
        else�@//isStart==true�̂Ƃ��B
        {
            //�V�����ʒu��myPreviousPos�ɐݒ�
            foreach (var item in _candyArray)
            {
                int column = (int)item.transform.position.x;
                int row = (int)item.transform.position.y;
                item.GetComponent<CandyMove>().ChangeMyPreviousPos(new Vector2(column, row)); 
            }

            //�������܂ɂR������Ă��邩�ǂ�������B
            Invoke("CheckMatching", 0.2f);
        }
    }

    public void CheckMatching()
    {
        //���̍s���烈�R�̂Ȃ�����m�F
        for (int i = 0; i < _height; i++)
        {
            //�E����Q�ڈȍ~�͊m�F�s�v
            for (int j = 0; j < _width - 2; j++)
            {
                //�����^�O�̃L�����f�B���R����ł�����B�w���W�����B
                if ((_candyArray[j, i].tag == _candyArray[j + 1, i].tag) && (_candyArray[j, i].tag == _candyArray[j + 2, i].tag))
                {
                    //Candy��isMatching��true��
                    _candyArray[j, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[j + 1, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[j + 2, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                }
            }
        }

        //���̗񂩂�^�e�̂Ȃ�����m�F
        for (int i = 0; i < _width; i++)
        {
            //�ォ��Q�ڈȍ~�͊m�F�s�v�B
            for (int j = 0; j < _height - 2; j++)
            {
                //�x���W�����B
                if ((_candyArray[i, j].tag == _candyArray[i, j + 1].tag) && (_candyArray[i, j].tag == _candyArray[i, j + 2].tag))
                {
                    _candyArray[i, j].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[i, j + 1].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[i, j + 2].GetComponent<CandyMove>().ChangeIsMatching(true);
                }
            }
        }

        //isMatching=true�̂��̂��k�������ɓ����
        foreach (var item in _candyArray)
        {
            if (item.GetComponent<CandyMove>().IsMatching)
            {
                //�R�ȏセ������Ƃ��A�L�����f�B�𔼓����ɂ���B
                item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                _deleteList.Add(item);
            }
        }

        //List���ɃL�����f�B������ꍇ
        if (_deleteList.Count > 0)
        {
            //�L�����f�B����������Ƃ��A��u�̊Ԃ��������邽��Ivoke�֐��ɂ���B
            Invoke("DeleteCandies", 0.2f);
        }
        else//List�ɃL�����f�B���Ȃ��ꍇ�B
        {
            //���܈ʒu���������L�����f�B�����̈ʒu�ɁB
            foreach (var item in _candyArray)
            {
                item.GetComponent<CandyMove>().BackToPreviousPos();
            }
        }
    }

    void DeleteCandies()
    {
        //List���̃L�����f�B�������B���A���̔z���null�ɁB
        foreach (var item in _deleteList)
        {
            Destroy(item);
            _candyArray[(int)item.transform.position.x, (int)item.transform.position.y] = null;
            //�X�R�A���Z���Ă����֐��Ăяo��
            _score += 100;
            Debug.Log("������");
        }
        //List������ۂɁB
        _deleteList.Clear();
        //�L�����f�B�̗�����҂��āA�󗓂ɐV�����L�����f�B������B
        Invoke("SpawnNewCandy", 1.2f);
    }
}
