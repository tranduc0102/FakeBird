using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSlider : MonoBehaviour
{
    [SerializeField] private GameObject panelLoading;
    [SerializeField] private GameObject panelSelectPlayer;
    [SerializeField] private Slider _slider;
    [SerializeField] private float duration;
    private const float maxValueSlider = 100f;
    private float value;
    void Start()
    {
        _slider.maxValue = maxValueSlider;
        DOTween.To(() => value, x => value = x, 100f, duration).OnUpdate(() =>
        {
            _slider.value = value;
        }).OnComplete(() =>
        {
            panelLoading.SetActive(false);
            panelSelectPlayer.SetActive(true);
        });
    }
}
