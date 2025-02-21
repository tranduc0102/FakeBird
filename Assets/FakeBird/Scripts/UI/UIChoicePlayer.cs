using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIChoicePlayer : MonoBehaviour
{
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;
    [SerializeField] private Button btnPlay;

    [SerializeField] private Animator _animatorUIPlayer;
    [SerializeField] private RuntimeAnimatorController[] _animationRunTime;
    private int currentIndex = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("IDSelectPlayer", currentIndex);
        btnLeft.onClick.AddListener(delegate
        {
            ClickChoice(true);
        });
        btnRight.onClick.AddListener(delegate
        {
            ClickChoice(false);
        });
        btnPlay.onClick.AddListener(Play);
    }

    private void Play()
    {
        PlayerPrefs.SetInt("IDSelectPlayer", currentIndex);
        PlayerPrefs.Save();
        DOVirtual.DelayedCall(0.2f, delegate
        {
            SceneManager.LoadSceneAsync("Game");
        });
    }

    private void ClickChoice(bool isLeft)
    {
        if (isLeft)
        {
            currentIndex -= 1;
            if (currentIndex < 0)
            {
                currentIndex = _animationRunTime.Length - 1;
            }

            _animatorUIPlayer.runtimeAnimatorController = _animationRunTime[currentIndex];
        }
        else
        {
            currentIndex += 1;
            if (currentIndex >= _animationRunTime.Length)
            {
                currentIndex = 0;
            }
            _animatorUIPlayer.runtimeAnimatorController = _animationRunTime[currentIndex];
        }
    }
}
