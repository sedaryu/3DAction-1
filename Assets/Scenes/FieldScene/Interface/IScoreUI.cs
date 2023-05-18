using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreUI
{
    public void UpdateScoreTextUI(string text);

    public void UpdateRushTimeTextUI(string text);

    public void UpdateComboTextUI(string text);
}
