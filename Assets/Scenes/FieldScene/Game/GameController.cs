using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private ClearConditions gameClearCondition;
    public enum ClearConditions
    { 
        ItemCollected,
        EnemyDestroyed,
        BossDestroyed,
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void JudgeGameClear(ClearConditions condition)
    {
        if (gameClearCondition != condition) return;

        Time.timeScale = 0;

        GameObject.Find("Canvas").transform.Find("GameClearText").gameObject.SetActive(true);
    }
}
