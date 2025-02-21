using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILose : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button btnPlayAgain;
    [SerializeField] private Button btnHome;
    public TextMeshProUGUI txtHighScore;

    private void OnValidate()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        txtHighScore = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        btnPlayAgain = transform.GetChild(3).GetComponent<Button>();
        btnHome = transform.GetChild(4).GetComponent<Button>();
    }

    private void Start()
    {
        btnPlayAgain.onClick.AddListener(delegate
        {
            GameManager.Instance.PlayAgain();
        });
        btnHome.onClick.AddListener(delegate
        {
           GameManager.Instance.BackHome();
        });
    }

    private void OnEnable()
    {
        txtHighScore.text = "Hight Score: " + PlayerPrefs.GetInt("HightScore", 0);
        transform.localScale = Vector3.zero;
        _canvasGroup.DOFade(1, 0.2f);
        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
