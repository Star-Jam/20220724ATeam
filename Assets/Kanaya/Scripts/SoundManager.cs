using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    [Header("タイトルシーンの音源")] AudioSource _titleAudioSource;

    [SerializeField]
    [Header("ゲームシーンの音源")] AudioSource _gameAudioSource;

    [SerializeField]
    [Header("リザルトシーンの音源")] AudioSource _resultAudioSource;

    [SerializeField]
    [Header("クリック音")] AudioClip _clickSe;
    [SerializeField]
    [Header("揃った時の音")] AudioClip _alignSe;
   　void Update()
    {
        if(Input.GetMouseButtonDown(0))//左クリック時
        {
            ClickSe();
        }
        
    }
    public void PlayTitleMusic()//タイトル画面時のBGM
    {
        _titleAudioSource.Play();
        _titleAudioSource = GetComponent<AudioSource>();
    }
    public void ClickSe()//クリック時のSE
    {
        _titleAudioSource.PlayOneShot(_clickSe);
    }
    public void PlayGameMusic()//ゲーム画時のBGM
    {
        _gameAudioSource.Play();
        _gameAudioSource = GetComponent<AudioSource>();
    }
    public void PlayResultMusic()//リザルト画面時のBGM
    {
        _resultAudioSource.Play();
        _resultAudioSource = GetComponent<AudioSource>();
    }
    public void AlignSe()//揃った時のSE
    {
        _gameAudioSource.PlayOneShot(_alignSe);
    }
}
