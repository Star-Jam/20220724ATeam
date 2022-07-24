using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField]
    [Tooltip("���U���g")] GameObject _resultPanel;
    [SerializeField]
    [Tooltip("�|�[�Y")] GameObject _pause;
    [SerializeField]
    [Tooltip("��������")] Text _time ;
    [SerializeField]
    [Tooltip("�X�R�A")] Text _score;
    [SerializeField]
    [Tooltip("���U���g�X�R�A")] Text _resultscore;

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
