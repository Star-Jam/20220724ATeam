using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    [SerializeField]
    [Header("遷移するシーンの名前")] string LaodSceneName;
    public void StartGame() //ボタンクリック
    {
        SceneManager.LoadScene(LaodSceneName); //シーン遷移
    }
}
