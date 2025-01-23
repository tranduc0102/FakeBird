using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI txtScorce;
    public TextMeshProUGUI TextScore
    {
        get => txtScorce;
        set => txtScorce = value;
    }

    public void UILose()
    {
        
    }
}
