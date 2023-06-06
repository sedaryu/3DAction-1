using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuAct : MonoBehaviour
{
    private ResultMode result;
    private MainMode main;

    private void Awake()
    {
        Time.timeScale = 1;

        result = GetComponent<ResultMode>();
        main = GetComponent<MainMode>();
    }

    // Start is called before the first frame update
    async void Start()
    {
        Task task0 = await result.ResultItems();
        Task task1 = await main.SetMainMenu();
    }
}
