using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyMove : MonoBehaviour
{
    public bool IsMatching => _isMatching;

    //自身の入っている配列の座標
    int _column;//列
    int _row;//行
    //スワイプしたときの座標を確認するための変数
    Vector2 _fingerDown;
    Vector2 _fingerUp;
    Vector2 _distance;
    /// <summary>隣のキャンディ</summary>
    GameObject _neighborCandy;
    /// <summary>３つ並んでいるとき知らせる</summary>
    bool _isMatching;  
    /// <summary>移動前の座標</summary>
    Vector2 _myPreviousPos;

    public bool ChangeIsMatching(bool match) => _isMatching = match;

    public Vector2 ChangeMyPreviousPos(Vector2 vector2) => _myPreviousPos = vector2;

    void Start()
    {
        //自分の位置を座標配列の番号（Index)にあてておく。
        _column = (int)transform.position.x;
        _row = (int)transform.position.y;
        //スタート位置を記録する。
        _myPreviousPos = new Vector2(_column, _row);
    }
    void Update()
    {
        //現在の座標と、column、rowの値が異なるとき。
        if (transform.position.x != _column || transform.position.y != _row)
        {
            //column,rowの位置に徐々に移動する。
            transform.position = Vector2.Lerp(transform.position, new Vector2(_column, _row), 0.3f);
            //現在の位置と、目的地(column,row)との距離を測る。
            Vector2 dif = (Vector2)transform.position - new Vector2(_column, _row);

            //目的地との距離が0.1fより小さくなったら。
            if (Mathf.Abs(dif.magnitude) < 0.1f)
            {
                transform.position = new Vector2(_column, _row);

                //自身をCandyArray配列に格納する。
                SetCandyToArray();
            }
            //自分が０行目（一番下）ではなく、かつ、下にキャンディがない場合、落下させる
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

    //指を離したとき
    void OnMouseUp()
    {
        _fingerUp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //２点のベクトルの差を計算
        _distance = _fingerUp - _fingerDown;

        GameManager.Instance.ResetCoolTime();
        moveCandies();
    }

    void moveCandies()
    {
        //右にスワイプしていたなら。（Mathf.Absとは絶対値を示す）
        if (_distance.x >= 0 && Mathf.Abs(_distance.x) > Mathf.Abs(_distance.y))
        {
            //自身が一番右にいない場合、となりのキャンディと位置を交換する
            if (_column < 4)
            {
                //右隣りのキャンディ情報をneighborCandyに代入
                _neighborCandy = GameManager.Instance._candyArray[_column + 1, _row];
                //隣のキャンディを１列左へ。
                _neighborCandy.GetComponent<CandyMove>()._column -= 1;
                //自身は１列右へ。
                _column += 1;
            }
        }
        //左にスワイプしていたなら。
        if (_distance.x < 0 && Mathf.Abs(_distance.x) > Mathf.Abs(_distance.y))
        {
            //自身が一番左にいない場合、となりのキャンディと位置を交換する
            if (_column > 0)
            {
                //左隣りのキャンディ情報を取得
                _neighborCandy = GameManager.Instance._candyArray[_column - 1, _row];
                //隣のキャンディを１列右へ。
                _neighborCandy.GetComponent<CandyMove>()._column += 1;
                //自身は１列左へ。
                _column -= 1;
            }
        }

        //上にスワイプしていたなら。
        if (_distance.y >= 0 && Mathf.Abs(_distance.x) < Mathf.Abs(_distance.y))
        {
            //自身が一番上にいない場合、となりのキャンディと位置を交換する
            if (_row < 6)
            {
                //上のキャンディ情報を取得
                _neighborCandy = GameManager.Instance._candyArray[_column, _row + 1];
                //隣のキャンディを１行下へ。
                _neighborCandy.GetComponent<CandyMove>()._row -= 1;
                //自身は１行上へ。
                _row += 1;
            }
        }

        //下にスワイプしていたなら。
        if (_distance.y < 0 && Mathf.Abs(_distance.x) < Mathf.Abs(_distance.y))
        {
            //自身が一番下にいない場合、となりのキャンディと位置を交換する
            if (_row > 0)
            {
                //下のキャンディ情報を取得
                _neighborCandy = GameManager.Instance._candyArray[_column, _row - 1];
                //隣のキャンディを１行上へ。
                _neighborCandy.GetComponent<CandyMove>()._row += 1;
                //自身は１行下へ。
                _row -= 1;
            }
        }
        Invoke("DoCheckMatching", 0.5f); Invoke("DoCheckMatching", 0.5f);
    }
    //CandyArray配列に、自身を格納する。

    public void SetCandyToArray()
    {
        GameManager.Instance._candyArray[_column, _row] = gameObject;
    }

    void FallCandy()
    {
        //自分のいた配列を空にする
        GameManager.Instance._candyArray[_column, _row] = null;
        //自分を下に移動させる
        _row -= 1;
    }

    void DoCheckMatching()
    {
        GameManager.Instance.CheckMatching();
    }

    //元の位置に戻る。
    public void BackToPreviousPos()
    {
        _column = (int)_myPreviousPos.x;
        _row = (int)_myPreviousPos.y;
    }
}
