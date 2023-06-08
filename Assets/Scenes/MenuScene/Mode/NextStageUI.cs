using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NextStageUI : SelectModeUI
{
    [SerializeField] private TextMeshPro tvText;
    [SerializeField] private Collider mug;
    [SerializeField] private Collider telescope;

    [SerializeField] private SpawnObjectList spawnItemList;
    private List<StageParam> stageParams = new List<StageParam>();
    private int selectedStage = 0;
    [SerializeField] private Shoes shoes;
    [SerializeField] private Smash smash;
    private PlayerParam playerParam;
    private Dictionary<string, Vector2> obstacles;

    private void Awake()
    {
        initialText = "Clicking Mugcup, \nChange Next Stage \nClicking Telescope, \nGo To Next Stage";
        colliders.Add(mug); colliders.Add(telescope);
        onClicked += () => sublineText.rectTransform.sizeDelta = new Vector2(145, 70);
        onClicked += DisplayStageParamOnTV;
        GenerateStageParam();
    }

    public void DisplayStageParamOnTV()
    {
        tvText.text = $"StageType: {stageParams[selectedStage].Type.ToString()}\n" +
                      $"Enemy: \n" +
                      $"-{stageParams[selectedStage].SpawnEnemyList.SpawnObjects[0].name} " +
                      $"AppearRate: {stageParams[selectedStage].SpawnEnemyList.SpawnObjects[0].appearanceProbability}\n" +
                      $"-{stageParams[selectedStage].SpawnEnemyList.SpawnObjects[1].name} " +
                      $"AppearRate: {stageParams[selectedStage].SpawnEnemyList.SpawnObjects[1].appearanceProbability}\n" +
                      $"-{stageParams[selectedStage].SpawnEnemyList.SpawnObjects[2].name} " +
                      $"AppearRate: {stageParams[selectedStage].SpawnEnemyList.SpawnObjects[2].appearanceProbability}";
    }

    public void OnClickMug()
    {
        if (selectedStage == stageParams.Count - 1) selectedStage = 0;
        else selectedStage++;
        DisplayStageParamOnTV();
    }

    public void OnClickTelescope()
    {
        SceneManager.sceneLoaded += SetParamToNextScene;
        string gun = new LoadJson().LoadEquipmentGunParam().NowEquipingGun;
        playerParam = new PlayerParam(new LoadAsset().LoadObject<Gun>("Gun", gun), smash, shoes, new LoadJson().LoadMenuParam().Parameter("Life"));
        obstacles = new StageMaker().MakeStage();
        GameObject.Find("Canvas").transform.Find("Back").gameObject.SetActive(false);
        colliders.ForEach(x => x.enabled = false);
        StartCoroutine(TransferFieldScene());
    }

    private void SetParamToNextScene(Scene scene, LoadSceneMode _mode)
    {
        SceneManager.sceneLoaded -= SetParamToNextScene;
        ParamReceiver receiver = GameObject.Find("ParamReceiver").GetComponent<ParamReceiver>();
        receiver.spawnEnemyList = stageParams[selectedStage].SpawnEnemyList;
        receiver.spawnItemList = stageParams[selectedStage].SpawnItemList;
        receiver.playerParam = playerParam;
        receiver.mainObstacles = obstacles;
    }

    private IEnumerator TransferFieldScene()
    {
        for (int i = 0; i < 5; i++)
        {
            tvText.text = $"Ok, \nBegin Preparations \nTransporting You To The Destination \n{5 - i}";
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene($"{stageParams[selectedStage].Type.ToString()}FieldScene");
    }

    private void GenerateStageParam()
    {
        float day = new LoadJson().LoadMenuParam().Parameter("Day");
        int rank = (int)(day * 0.2f);
        List<EnemyParam> loads = new List<EnemyParam>();
        new LoadAsset().LoadParamsAll("Enemy").ToList().ForEach(x => loads.Add(x as EnemyParam));
        List<EnemyParam> enemies = loads.Where(x => x.Rank <= rank).ToList();
        EnemyParam[] normalEnemies = enemies.Where(x => x.Type == EnemyParam.EnemyType.Normal).ToArray();
        EnemyParam[] miniEnemies = enemies.Where(x => x.Type == EnemyParam.EnemyType.Mini).ToArray();

        for (int n = 0; n < 3; n++)
        {
            List<string> enemyNames = new List<string>();
            int mini = Random.Range(0, 3);
            for (int i = 0; i < 3; i++)
            {
                if (i == mini) enemyNames.Add(miniEnemies[Random.Range(0, enemies.Count(x => x.Type == EnemyParam.EnemyType.Mini))].Name);
                else enemyNames.Add(normalEnemies[Random.Range(0, enemies.Count(x => x.Type == EnemyParam.EnemyType.Normal))].Name);
            }

            SpawnObjectList.SpawnObject[] spawnObjects = new SpawnObjectList.SpawnObject[enemyNames.Count];
            int percentage = 100;
            spawnObjects[0] = new SpawnObjectList.SpawnObject(enemyNames[0], Random.Range(1, 81));
            percentage -= spawnObjects[0].appearanceProbability;
            spawnObjects[1] = new SpawnObjectList.SpawnObject(enemyNames[1], Random.Range(1, percentage));
            percentage -= spawnObjects[1].appearanceProbability;
            spawnObjects[2] = new SpawnObjectList.SpawnObject(enemyNames[2], percentage);

            stageParams.Add(new StageParam(StageParam.StageType.Town, new SpawnObjectList(1.5f, spawnObjects), spawnItemList));
        }
    }
}
