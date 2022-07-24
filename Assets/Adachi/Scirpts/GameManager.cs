using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    [Header("パズルのプレファブ")]
    GameObject[] _candies;

    //配列の大きさを定義。
    int _width = 7;
    int _height = 7;

    //publicでGameObject型の配列を作る。

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
                //画面の見た目として、candyのtransform.positionを設定
                candy.transform.position = new Vector2(i/* - _width / 2*/, j/* - _height / 2*/);
                //画面に7×７の表があるイメージで、キャンディの座標をそのまま配列のIndexに利用して、配列の要素にCandyを入れている。
                _candyArray[i, j] = candy;
            }
        }
        CheckStartset();
    }

    void CheckStartset()
    {
        //下の行からヨコのつながりを確認
        for (int i = 0; i < _height; i++)
        {
            //右から２つ目以降は確認不要（width-2）
            for (int j = 0; j < _width - 2; j++)
            {
                //同じタグのキャンディが３つ並んでいたら。Ｘ座標がｊなので注意。

                //念のため、ふたつの式それぞれをカッコで囲んでいる。

                if ((_candyArray[j, i].tag == _candyArray[j + 1, i].tag) && (_candyArray[j, i].tag == _candyArray[j + 2, i].tag))
                {
                    //CandyのisMatchingをtrueに
                    _candyArray[j, i].GetComponent<CandyMove>()._isMatching = true;
                    _candyArray[j + 1, i].GetComponent<CandyMove>()._isMatching = true;
                    _candyArray[j + 2, i].GetComponent<CandyMove>()._isMatching = true;
                }
            }
        }

        //左の列からタテのつながりを確認

        for (int i = 0; i < _width; i++)
        {
            //上から２つ目以降は確認不要。height-2
            for (int j = 0; j < _height - 2; j++)
            {
                //Ｙ座標がｊ。
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
