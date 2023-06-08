using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour, IClickUI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnActive()
    { 
        gameObject.SetActive(true);
    }

    public void OnClicked()
    {
        Debug.Log("ON!!");
    }
}
