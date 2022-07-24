using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField]
    [Tooltip("リザルト")] GameObject _result;
    [SerializeField]
    [Tooltip("ポーズ")] GameObject _pause;
    [SerializeField]
    [Tooltip("制限時間")] Text _time ;
    [SerializeField]
    [Tooltip("スコア")] Text _score;
    [SerializeField]
    [Tooltip("リザルトスコア")] Text _resultscore;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void taitoru()
    {
        SceneManager.LoadScene("");
    }
    public void gameseen()
    {
        SceneManager.LoadScene("");
    }
    public void Teisi()
    {
        _pause.SetActive(true);
    }
    public void start()
    {
        _pause.SetActive(false);
    }
    public void ButtonExit()
    {
        Application.Quit();
        Debug.Log("a");
    }
    public void Timr(float time)
    {
        _time.text = time.ToString("f0");
    }
    void Score(float time)
    {
        _score.text = time.ToString("f0");
        _resultscore.text = time.ToString("f0");
    }
    void ResultScoer()
    {

    }
}
