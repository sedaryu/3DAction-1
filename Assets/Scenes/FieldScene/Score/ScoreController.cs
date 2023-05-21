using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int Score { get; private set; } = 0;
    public float RushTime
    {
        get => rushTime;
        private set
        {
            if (value <= 0) rushTime = 0;
            else if (value >= 10) rushTime = 10;
            else rushTime = value;
        }
    }
    private float rushTime = 0;
    public int Combo 
    { 
        get => combo;
        private set
        {
            if (value <= 0) combo = 0;
            else combo = value;
        } 
    }
    private int combo = 0;

    private List<IScoreUI> scoreUIs;

    private void Awake()
    {
        scoreUIs = GameObject.Find("Canvas")?.GetComponentsInChildren<IScoreUI>().ToList();
    }

    private void Start()
    {
        scoreUIs?.ForEach(x => x.UpdateScoreTextUI(Score.ToString()));
        scoreUIs?.ForEach(x => x.UpdateRushTimeTextUI(RushTime.ToString()));
        scoreUIs?.ForEach(x => x.UpdateComboTextUI(Combo.ToString()));
    }

    private void Update()
    {
        RushTime -= Time.deltaTime;
        scoreUIs?.ForEach(x => x.UpdateRushTimeTextUI(RushTime.ToString()));
        if (RushTime == 0)
        {
            combo = 0;
            scoreUIs?.ForEach(x => x.UpdateComboTextUI(Combo.ToString()));
        }
    }

    public void IncreaseScore(int _score = 50) //�O���b�O��
    { 
        Score += _score * (int)(RushTime * 0.1f + 1);
        if (Combo > 1) Score += (Combo - 1) * 10;
        scoreUIs?.ForEach(x => x.UpdateScoreTextUI(Score.ToString()));
    }

    public void IncreaseRushTime(float time = 5) //�O���b�O��
    {
        RushTime += time;
        scoreUIs?.ForEach(x => x.UpdateRushTimeTextUI(RushTime.ToString()));
    }

    public void IncreaseCombo(int _combo = 1) //�X�}�b�V����
    {
        Combo += _combo;
        scoreUIs?.ForEach(x => x.UpdateComboTextUI(Combo.ToString()));
    }
}
