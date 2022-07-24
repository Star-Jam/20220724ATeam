using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyMove : MonoBehaviour
{
    public bool IsMatching => _isMatching;

    //���g�̓����Ă���z��̍��W
    int _column;//��
    int _row;//�s
    //�X���C�v�����Ƃ��̍��W���m�F���邽�߂̕ϐ�
    Vector2 _fingerDown;
    Vector2 _fingerUp;
    Vector2 _distance;
    /// <summary>�ׂ̃L�����f�B</summary>
    GameObject _neighborCandy;
    /// <summary>�R����ł���Ƃ��m�点��</summary>
    bool _isMatching;  
    /// <summary>�ړ��O�̍��W</summary>
    Vector2 _myPreviousPos;

    public bool ChangeIsMatching(bool match) => _isMatching = match;

    public Vector2 ChangeMyPreviousPos(Vector2 vector2) => _myPreviousPos = vector2;

    void Start()
    {
        //�����̈ʒu�����W�z��̔ԍ��iIndex)�ɂ��ĂĂ����B
        _column = (int)transform.position.x;
        _row = (int)transform.position.y;
        //�X�^�[�g�ʒu���L�^����B
        _myPreviousPos = new Vector2(_column, _row);
    }
    void Update()
    {
        //���݂̍��W�ƁAcolumn�Arow�̒l���قȂ�Ƃ��B
        if (transform.position.x != _column || transform.position.y != _row)
        {
            //column,row�̈ʒu�ɏ��X�Ɉړ�����B
            transform.position = Vector2.Lerp(transform.position, new Vector2(_column, _row), 0.3f);
            //���݂̈ʒu�ƁA�ړI�n(column,row)�Ƃ̋����𑪂�B
            Vector2 dif = (Vector2)transform.position - new Vector2(_column, _row);

            //�ړI�n�Ƃ̋�����0.1f��菬�����Ȃ�����B
            if (Mathf.Abs(dif.magnitude) < 0.1f)
            {
                transform.position = new Vector2(_column, _row);

                //���g��CandyArray�z��Ɋi�[����B
                SetCandyToArray();
            }
            //�������O�s�ځi��ԉ��j�ł͂Ȃ��A���A���ɃL�����f�B���Ȃ��ꍇ�A����������
            else if (_row > 0 && GameManager.Instance._candyArray[_column, _row - 1] == null)
            {
                FallCandy();
            }
        }        
    }

    void OnMouseDown()
    {
        _fingerDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //�w�𗣂����Ƃ�
    void OnMouseUp()
    {
        _fingerUp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //�Q�_�̃x�N�g���̍����v�Z
        _distance = _fingerUp - _fingerDown;

        GameManager.Instance.ResetCoolTime();
        moveCandies();
    }

    void moveCandies()
    {
        //�E�ɃX���C�v���Ă����Ȃ�B�iMathf.Abs�Ƃ͐�Βl�������j
        if (_distance.x >= 0 && Mathf.Abs(_distance.x) > Mathf.Abs(_distance.y))
        {
            //���g����ԉE�ɂ��Ȃ��ꍇ�A�ƂȂ�̃L�����f�B�ƈʒu����������
            if (_column < 4)
            {
                //�E�ׂ�̃L�����f�B����neighborCandy�ɑ��
                _neighborCandy = GameManager.Instance._candyArray[_column + 1, _row];
                //�ׂ̃L�����f�B���P�񍶂ցB
                _neighborCandy.GetComponent<CandyMove>()._column -= 1;
                //���g�͂P��E�ցB
                _column += 1;
            }
        }
        //���ɃX���C�v���Ă����Ȃ�B
        if (_distance.x < 0 && Mathf.Abs(_distance.x) > Mathf.Abs(_distance.y))
        {
            //���g����ԍ��ɂ��Ȃ��ꍇ�A�ƂȂ�̃L�����f�B�ƈʒu����������
            if (_column > 0)
            {
                //���ׂ�̃L�����f�B�����擾
                _neighborCandy = GameManager.Instance._candyArray[_column - 1, _row];
                //�ׂ̃L�����f�B���P��E�ցB
                _neighborCandy.GetComponent<CandyMove>()._column += 1;
                //���g�͂P�񍶂ցB
                _column -= 1;
            }
        }

        //��ɃX���C�v���Ă����Ȃ�B
        if (_distance.y >= 0 && Mathf.Abs(_distance.x) < Mathf.Abs(_distance.y))
        {
            //���g����ԏ�ɂ��Ȃ��ꍇ�A�ƂȂ�̃L�����f�B�ƈʒu����������
            if (_row < 6)
            {
                //��̃L�����f�B�����擾
                _neighborCandy = GameManager.Instance._candyArray[_column, _row + 1];
                //�ׂ̃L�����f�B���P�s���ցB
                _neighborCandy.GetComponent<CandyMove>()._row -= 1;
                //���g�͂P�s��ցB
                _row += 1;
            }
        }

        //���ɃX���C�v���Ă����Ȃ�B
        if (_distance.y < 0 && Mathf.Abs(_distance.x) < Mathf.Abs(_distance.y))
        {
            //���g����ԉ��ɂ��Ȃ��ꍇ�A�ƂȂ�̃L�����f�B�ƈʒu����������
            if (_row > 0)
            {
                //���̃L�����f�B�����擾
                _neighborCandy = GameManager.Instance._candyArray[_column, _row - 1];
                //�ׂ̃L�����f�B���P�s��ցB
                _neighborCandy.GetComponent<CandyMove>()._row += 1;
                //���g�͂P�s���ցB
                _row -= 1;
            }
        }
        Invoke("DoCheckMatching", 0.5f); Invoke("DoCheckMatching", 0.5f);
    }
    //CandyArray�z��ɁA���g���i�[����B

    public void SetCandyToArray()
    {
        GameManager.Instance._candyArray[_column, _row] = gameObject;
    }

    void FallCandy()
    {
        //�����̂����z�����ɂ���
        GameManager.Instance._candyArray[_column, _row] = null;
        //���������Ɉړ�������
        _row -= 1;
    }

    void DoCheckMatching()
    {
        GameManager.Instance.CheckMatching();
    }

    //���̈ʒu�ɖ߂�B
    public void BackToPreviousPos()
    {
        _column = (int)_myPreviousPos.x;
        _row = (int)_myPreviousPos.y;
    }
}
