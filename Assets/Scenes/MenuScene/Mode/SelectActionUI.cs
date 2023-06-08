using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SelectActionUI : MonoBehaviour, IPointerClickHandler
{
    protected CameraController cameraController;
    public UnityAction onClicking;
    public UnityAction onClicked;

    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    public void SetVisible()
    {
        GetComponent<Text>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetInvisible()
    {
        GetComponent<Text>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public abstract void OnPointerClick(PointerEventData eventData);
}
