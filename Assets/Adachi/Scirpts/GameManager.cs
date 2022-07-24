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
    int _score;

    bool _isStart;
    //publicでGameObject型の配列を作る。
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
                    _candyArray[j, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[j + 1, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[j + 2, i].GetComponent<CandyMove>().ChangeIsMatching(true);
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
                    _candyArray[i, j].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[i, j + 1].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[i, j + 2].GetComponent<CandyMove>().ChangeIsMatching(true);
                }
            }
        }

        //isMatching=trueのものをＬｉｓｔに入れる
        foreach (var item in _candyArray)
        {
            if (item.GetComponent<CandyMove>().IsMatching)
            {
                _deleteList.Add(item);
            }
        }

        //List内にキャンディがある場合
        if (_deleteList.Count > 0)
        {
            //該当する配列をnullにして（内部管理）、キャンディを消去する（見た目）。
            foreach (var item in _deleteList)
            {
                _candyArray[(int)item.transform.position.x, (int)item.transform.position.y] = null;
                Destroy(item);
            }
            //Listを空っぽに。
            _deleteList.Clear();
            //空欄に新しいキャンディを入れる。
            SpawnNewCandy();
        }
        else//Listにキャンディがない場合。
        {
            //ゲーム開始。
            _isStart = true;
        }
    }

    //空欄に新しいキャンディを生成
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
                    //見た目の処理
                    candy.transform.position = new Vector2(i, j + 0.3f);
                    //内部管理の処理
                    _candyArray[i, j] = candy;
                }
            }
        }

        if (_isStart == false)
        {
            CheckStartset();
        }
        else　//isStart==trueのとき。
        {
            //新しい位置をmyPreviousPosに設定
            foreach (var item in _candyArray)
            {
                int column = (int)item.transform.position.x;
                int row = (int)item.transform.position.y;
                item.GetComponent<CandyMove>().ChangeMyPreviousPos(new Vector2(column, row)); 
            }

            //続けざまに３つそろっているかどうか判定。
            Invoke("CheckMatching", 0.2f);
        }
    }

    public void CheckMatching()
    {
        //下の行からヨコのつながりを確認
        for (int i = 0; i < _height; i++)
        {
            //右から２つ目以降は確認不要
            for (int j = 0; j < _width - 2; j++)
            {
                //同じタグのキャンディが３つ並んでいたら。Ｘ座標がｊ。
                if ((_candyArray[j, i].tag == _candyArray[j + 1, i].tag) && (_candyArray[j, i].tag == _candyArray[j + 2, i].tag))
                {
                    //CandyのisMatchingをtrueに
                    _candyArray[j, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[j + 1, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[j + 2, i].GetComponent<CandyMove>().ChangeIsMatching(true);
                }
            }
        }

        //左の列からタテのつながりを確認
        for (int i = 0; i < _width; i++)
        {
            //上から２つ目以降は確認不要。
            for (int j = 0; j < _height - 2; j++)
            {
                //Ｙ座標がｊ。
                if ((_candyArray[i, j].tag == _candyArray[i, j + 1].tag) && (_candyArray[i, j].tag == _candyArray[i, j + 2].tag))
                {
                    _candyArray[i, j].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[i, j + 1].GetComponent<CandyMove>().ChangeIsMatching(true);
                    _candyArray[i, j + 2].GetComponent<CandyMove>().ChangeIsMatching(true);
                }
            }
        }

        //isMatching=trueのものをＬｉｓｔに入れる
        foreach (var item in _candyArray)
        {
            if (item.GetComponent<CandyMove>().IsMatching)
            {
                //３つ以上そろったとき、キャンディを半透明にする。
                item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                _deleteList.Add(item);
            }
        }

        //List内にキャンディがある場合
        if (_deleteList.Count > 0)
        {
            //キャンディを消去するとき、一瞬の間を持たせるためIvoke関数にする。
            Invoke("DeleteCandies", 0.2f);
        }
        else//Listにキャンディがない場合。
        {
            //いま位置交換したキャンディを元の位置に。
            foreach (var item in _candyArray)
            {
                item.GetComponent<CandyMove>().BackToPreviousPos();
            }
        }
    }

    void DeleteCandies()
    {
        //List内のキャンディを消去。かつ、その配列をnullに。
        foreach (var item in _deleteList)
        {
            Destroy(item);
            _candyArray[(int)item.transform.position.x, (int)item.transform.position.y] = null;
            //スコア加算してくれる関数呼び出す
            _score += 100;
            Debug.Log("揃った");
        }
        //Listを空っぽに。
        _deleteList.Clear();
        //キャンディの落下を待って、空欄に新しいキャンディを入れる。
        Invoke("SpawnNewCandy", 1.2f);
    }
}
