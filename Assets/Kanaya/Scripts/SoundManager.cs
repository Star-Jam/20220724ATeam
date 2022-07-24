using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    [Header("�^�C�g���V�[���̉���")] AudioSource _titleAudioSource;

    [SerializeField]
    [Header("�Q�[���V�[���̉���")] AudioClip _gameAudioSource;

    [SerializeField]
    [Header("���U���g�V�[���̉���")] AudioClip _resultAudioSource;

    [SerializeField]
    [Header("�N���b�N��")] AudioClip _clickSe;
    [SerializeField]
    [Header("���������̉�")] AudioClip _alignSe;
   �@void Update()
    {
        if(Input.GetMouseButtonDown(0))//���N���b�N��
        {
            ClickSe();
        }       
    }
    public void PlayTitleMusic()//�^�C�g����ʎ���BGM
    {
        _titleAudioSource.Play();
        _titleAudioSource = GetComponent<AudioSource>();
    }
    public void ClickSe()//�N���b�N����SE
    {
        _titleAudioSource.PlayOneShot(_clickSe);
    }
    public void PlayGameMusic()//�Q�[���掞��BGM
    {
        _titleAudioSource.PlayOneShot(_gameAudioSource);
    }
    public void PlayResultMusic()//���U���g��ʎ���BGM
    {
        _titleAudioSource.Stop();
        _titleAudioSource.clip = _resultAudioSource;
        _titleAudioSource.PlayOneShot(_resultAudioSource);        
    }
    public void AlignSe()//����������SE
    {
        _titleAudioSource.PlayOneShot(_alignSe);
    }
    public void PauseMusic()
    {
        
    }
}
