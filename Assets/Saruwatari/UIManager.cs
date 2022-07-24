using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField]
    [Tooltip("リザルト")] GameObject _resultPanel;
    [SerializeField]
    [Tooltip("ポーズ")] GameObject _pause;
    [SerializeField]
    [Tooltip("制限時間")] Text _time ;
    [SerializeField]
    [Tooltip("スコア")] Text _score;
    [SerializeField]
    [Tooltip("リザルトスコア")] Text _resultscore;

    void Start()
    {
        _score.text = "0";
    }

    public void Pause(bool active)
    {
        _pause.SetActive(active);
    }

    public void Timer(float time)
    {
        _time.text = time.ToString("f0");
    }
    public void Score(int score)
    {
        _score.text = score.ToString();
    }
    public void ResultScore(int score)
    {
        _resultscore.text = score.ToString();
    }

    public void ResultSetActive(bool active)
    {
        _resultPanel.gameObject.SetActive(active);
    }
}
