using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private ClearConditions gameClearCondition;

    public List<ItemParam> collectItems;
    public float life;

    public UnityAction onGameClear;

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

        onGameClear?.Invoke();
        StartCoroutine(TransferScene());
    }

    private IEnumerator TransferScene()
    { 
        yield return new WaitForSecondsRealtime(5);
        SceneManager.sceneLoaded += SetCollectItemsToNextScene;
        SceneManager.LoadScene("MenuScene");
    }

    private void SetCollectItemsToNextScene(Scene scene, LoadSceneMode _mode)
    {
        SceneManager.sceneLoaded -= SetCollectItemsToNextScene;
        GameResulter resulter = GameObject.Find("Menu").GetComponent<GameResulter>();
        resulter.isResultable = true;
        resulter.collectItems = collectItems;
        resulter.life = life;
    }
}
