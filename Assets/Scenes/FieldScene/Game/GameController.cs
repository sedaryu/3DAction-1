using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private ClearConditions gameClearCondition;

    public List<ItemParam> collectItems;
    public float life;
    public StageParam stageParam;

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

    public void GameOver()
    {
        Time.timeScale = 0;
        GameObject text = GameObject.Find("Canvas").transform.Find("GameClearText").gameObject;
        text.SetActive(true);
        text.GetComponent<Text>().color = Color.black;
        text.GetComponent<Text>().text = $"GameOver\nYouHaveSurvived\nFor {new LoadJson().LoadMenuParam().Parameter("Day")} Days";
        new LoadJson().SaveMenuParam(new MenuParam(new List<string>() { "Point", "PointMax", "Life", "LifeMax", "Random", "RandomMax", "Day", "DayMax" },
                                                   new List<float>() { 0, 10000000272564224, 5, 5, 0, 100, 1, 10000000272564224 }));
        new LoadJson().SaveEquipmentGunParam(new EquipmentGunParam(new List<string>() { "SurvivalRevolver" }, 0));
        StageParam param = new StageParam(0, new List<string>() { "Zombie", "SpeedZombie", "MiniZombie" },
                                             new List<int>() { 50, 25, 25 },
                                             new List<string>() { "Cola", "MultiVitamin", "ItemBox" },
                                             new List<int>() { 15, 5, 5 });
        Dictionary<string, Vector2> mainObstacles =  new StageMaker().MakeStage(out string[] subObstacles);
        param.SetObstacles(mainObstacles.Keys.ToList(), mainObstacles.Values.ToList(), subObstacles);
        new LoadJson().SaveSavedStageParam(new StageParam(0, new List<string>() { "Zombie", "SpeedZombie", "MiniZombie" },
                                                             new List<int>() { 50, 25, 25 },
                                                             new List<string>() { "Cola", "MultiVitamin", "ItemBox" },
                                                             new List<int>() { 15, 5, 5 }));
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
        ResultMode resulter = GameObject.Find("Menu").GetComponent<ResultMode>();
        resulter.isResultable = true;
        resulter.collectItems = collectItems;
        resulter.life = life;
        GameObject.Find("Canvas").transform.Find("NextStage").GetComponent<NextStageUI>().prevStageParam = stageParam;
    }
}
