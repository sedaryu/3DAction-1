using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackUI : MonoBehaviour, IPointerClickHandler
{
    public UnityAction onBack;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        SetInvisible();
        onBack?.Invoke();
    }

    public void BackToMainMode()
    {
        SetInvisible();
        onBack?.Invoke();
    }
}
