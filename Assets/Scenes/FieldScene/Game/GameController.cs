using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private ClearConditions gameClearCondition;

    private List<ItemParam> collectItems;

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

    public void JudgeGameClear(ClearConditions condition, List<ItemParam> items)
    {
        if (gameClearCondition != condition) return;

        Time.timeScale = 0;

        GameObject.Find("Canvas").transform.Find("GameClearText").gameObject.SetActive(true);
        collectItems = items;
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
        GameObject.Find("GameResulter").GetComponent<GameResulter>().collectItems = collectItems;
    }
}
