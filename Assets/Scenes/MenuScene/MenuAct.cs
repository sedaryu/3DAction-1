using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuAct : MonoBehaviour
{
    private GameResulter resulter;
    private MainMenu mainMenu;

    private void Awake()
    {
        Time.timeScale = 1;

        resulter = GetComponent<GameResulter>();
        mainMenu = GetComponent<MainMenu>();
    }

    // Start is called before the first frame update
    async void Start()
    {
        Task result = await resulter.ResultItems();
        mainMenu.SetMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
