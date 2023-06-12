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
    [SerializeField] private float dayPerRank;

    [SerializeField] private TextMeshPro tvText;
    [SerializeField] private Collider keyBoard;
    [SerializeField] private Collider mouse;
    [SerializeField] private Collider note;

    public StageParam prevStageParam;
    private StageParam savedStageParam;
    private List<StageParam> stageParams = new List<StageParam>();
    private int selectedStage = 0;
    [SerializeField] private Shoes shoes;
    [SerializeField] private Smash smash;
    private PlayerParam playerParam;

    private void Awake()
    {
        initialText = "";
        colliders.Add(keyBoard); colliders.Add(mouse); colliders.Add(note);
        savedStageParam = new LoadJson().LoadSavedStageParam();
        onClicked += () => 
        {
            headlineText.rectTransform.sizeDelta = new Vector2(0, 0);
            sublineText.rectTransform.sizeDelta = new Vector2(0, 0);
        };
        onClicked += DisplayStageParamOnTV;
        GenerateStageParam();
    }

    public void DisplayStageParamOnTV()
    {
        string frontText; StageParam param;
        if (selectedStage == -1) frontText = $"Clicking Note, You Can Save Previous Stage\n";
        else frontText = $"Clicking KeyBoard, You Can Select Stage\nClicking Mouse, You Can Decide Next Stage\n";

        if (selectedStage == -1) param = savedStageParam;
        else param = stageParams[selectedStage];

        if (param == null || param.SpawnEnemyList == null) tvText.text = frontText;
        else
        {
            tvText.text = frontText +
            $"StageType: {param.Type.ToString()}\n" +
            $"Enemy: \n" +
            $"-{param.SpawnEnemyList.SpawnObjects[0].name} " +
            $"AppearRate: {param.SpawnEnemyList.SpawnObjects[0].appearanceProbability}\n" +
            $"-{param.SpawnEnemyList.SpawnObjects[1].name} " +
            $"AppearRate: {param.SpawnEnemyList.SpawnObjects[1].appearanceProbability}\n" +
            $"-{param.SpawnEnemyList.SpawnObjects[2].name} " +
            $"AppearRate: {param.SpawnEnemyList.SpawnObjects[2].appearanceProbability}\n" +
            $"Item: \n" +
            $"-{param.SpawnItemList.SpawnObjects[0].name} " +
            $"AppearRate: {param.SpawnItemList.SpawnObjects[0].appearanceProbability}\n" +
            $"-{param.SpawnItemList.SpawnObjects[1].name} " +
            $"AppearRate: {param.SpawnItemList.SpawnObjects[1].appearanceProbability}\n" +
            $"-{param.SpawnItemList.SpawnObjects[2].name} " +
            $"AppearRate: {param.SpawnItemList.SpawnObjects[2].appearanceProbability}";
        }
    }

    public void OnClickKeyBoard()
    {
        if (selectedStage == stageParams.Count - 1) selectedStage = -1;
        else selectedStage++;
        DisplayStageParamOnTV();
    }

    public void OnClickMouse()
    {
        if (selectedStage == -1 && savedStageParam == null) return;
        SceneManager.sceneLoaded += SetParamToNextScene;
        string gun = new LoadJson().LoadEquipmentGunParam().NowEquipingGun;
        playerParam = new PlayerParam(new LoadAsset().LoadObject<Gun>("Gun", gun), smash, shoes, new LoadJson().LoadMenuParam().Parameter("Life"));
        if (selectedStage != -1) stageParams[selectedStage].SetObstacles(new StageMaker().MakeStage(out string[] subObstacles), subObstacles);
        GameObject.Find("Canvas").transform.Find("Back").gameObject.SetActive(false);
        colliders.ForEach(x => x.enabled = false);
        StartCoroutine(TransferFieldScene());
    }

    public void OnClickNote()
    {
        if (prevStageParam == null) return;
        savedStageParam = prevStageParam;
        new LoadJson().SaveSavedStageParam(savedStageParam);
        DisplayStageParamOnTV();
    }

    private void SetParamToNextScene(Scene scene, LoadSceneMode _mode)
    {
        SceneManager.sceneLoaded -= SetParamToNextScene;
        ParamReceiver receiver = GameObject.Find("ParamReceiver").GetComponent<ParamReceiver>();
        if (selectedStage == -1) receiver.stageParam = savedStageParam;
        else receiver.stageParam = stageParams[selectedStage];
        receiver.playerParam = playerParam;
    }

    private IEnumerator TransferFieldScene()
    {
        for (int i = 0; i < 5; i++)
        {
            tvText.text = $"Ok, \nBegin Preparations \nTransporting You To The Destination \n{5 - i}";
            yield return new WaitForSeconds(1);
        }
        if (selectedStage == -1) SceneManager.LoadScene($"{savedStageParam.Type.ToString()}FieldScene");
        else SceneManager.LoadScene($"{stageParams[selectedStage].Type.ToString()}FieldScene");
    }

    private void GenerateStageParam()
    {
        float day = new LoadJson().LoadMenuParam().Parameter("Day");
        int rank = (int)(day * dayPerRank);
        List<EnemyParam> loadEnemies = new List<EnemyParam>();
        List<ItemParam> loadItems = new List<ItemParam>();
        new LoadAsset().LoadParamsAll("Enemy").ToList().ForEach(x => loadEnemies.Add(x as EnemyParam));
        new LoadAsset().LoadParamsAll("Item").ToList().ForEach(x => loadItems.Add(x as ItemParam));
        List<EnemyParam> enemies = loadEnemies.Where(x => x.Rank <= rank).ToList();
        List<ItemParam> items = loadItems.Where(x => x.Rank <= rank).ToList();
        EnemyParam[] normalEnemies = enemies.Where(x => x.Type == EnemyParam.EnemyType.Normal).ToArray();
        EnemyParam[] miniEnemies = enemies.Where(x => x.Type == EnemyParam.EnemyType.Mini).ToArray();
        ItemParam[] pointItems = items.Where(x => x.Type == ItemParam.ItemType.Point).ToArray();
        ItemParam[] lifeItems = items.Where(x => x.Type == ItemParam.ItemType.Life).ToArray();
        ItemParam[] randomItems = items.Where(x => x.Type == ItemParam.ItemType.Random).ToArray();

        for (int n = 0; n < 3; n++)
        {
            List<string> enemyNames = new List<string>();
            int mini = Random.Range(0, 3);
            for (int i = 0; i < 3; i++)
            {
                if (i == mini) enemyNames.Add(miniEnemies[Random.Range(0, enemies.Count(x => x.Type == EnemyParam.EnemyType.Mini))].Name);
                else enemyNames.Add(normalEnemies[Random.Range(0, enemies.Count(x => x.Type == EnemyParam.EnemyType.Normal))].Name);
            }
            SpawnObjectList.SpawnObject[] spawnEnemies = new SpawnObjectList.SpawnObject[enemyNames.Count];
            int percentage = 100;
            spawnEnemies[0] = new SpawnObjectList.SpawnObject(enemyNames[0], Random.Range(1, 81));
            percentage -= spawnEnemies[0].appearanceProbability;
            spawnEnemies[1] = new SpawnObjectList.SpawnObject(enemyNames[1], Random.Range(1, percentage));
            percentage -= spawnEnemies[1].appearanceProbability;
            spawnEnemies[2] = new SpawnObjectList.SpawnObject(enemyNames[2], percentage);

            SpawnObjectList.SpawnObject[] spawnItems = new SpawnObjectList.SpawnObject[3];
            for (int i = 0; i < 3; i++)
            {
                int rnd = Random.Range(0, 10);
                if (0 <= rnd && rnd <= 4)
                {
                    int index = Random.Range(0, pointItems.Length);
                    spawnItems[i] = new SpawnObjectList.SpawnObject(pointItems[index].Name, pointItems[index].Drop);
                }
                else if (5 <= rnd && rnd <= 7)
                {
                    int index = Random.Range(0, lifeItems.Length);
                    spawnItems[i] = new SpawnObjectList.SpawnObject(lifeItems[index].Name, lifeItems[index].Drop);
                }
                else
                {
                    int index = Random.Range(0, randomItems.Length);
                    spawnItems[i] = new SpawnObjectList.SpawnObject(randomItems[index].Name, randomItems[index].Drop);
                }
            }

            stageParams.Add(new StageParam(0, spawnEnemies.ToList().Select(x => x.name).ToList(),
                                              spawnEnemies.ToList().Select(x => x.appearanceProbability).ToList(),
                                              spawnItems.ToList().Select(x => x.name).ToList(),
                                              spawnItems.ToList().Select(x => x.appearanceProbability).ToList()));
        }
    }
}
