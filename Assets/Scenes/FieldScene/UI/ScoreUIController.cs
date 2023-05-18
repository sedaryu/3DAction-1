using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour, IScoreUI
{
    //テキストUI
    private Text ScoreText;
    private Text RushTimeText;
    private Text ComboText;

    void Awake()
    {
        ScoreText = transform.Find("Score").GetComponent<Text>();
        RushTimeText = transform.Find("RushTime").GetComponent<Text>();
        ComboText = transform.Find("Combo").GetComponent<Text>();
    }

    public void UpdateScoreTextUI(string text)
    {
        ScoreText.text = text;
    }

    public void UpdateRushTimeTextUI(string text)
    {
        RushTimeText.text = text;
    }

    public void UpdateComboTextUI(string text)
    {
        ComboText.text = text;
    }
}
