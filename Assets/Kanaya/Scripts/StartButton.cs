using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    [SerializeField]
    [Header("�J�ڂ���V�[���̖��O")] string LaodSceneName;
    public void StartGame() //�{�^���N���b�N
    {
        SceneManager.LoadScene(LaodSceneName); //�V�[���J��
    }
}
