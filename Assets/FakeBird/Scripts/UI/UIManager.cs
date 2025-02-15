using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI txtScorce;
    [SerializeField] private UILose _uiLose;
    private bool isPause;
    public TextMeshProUGUI TextScore
    {
        get => txtScorce;
        set => txtScorce = value;
    }

    public void UILose()
    {
        GameManager.Instance.mode = ModeGame.EndGame;
        _uiLose.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        _uiLose.gameObject.SetActive(false);
    }

    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
