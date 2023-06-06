using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectEquipmentGunUI : MonoBehaviour, IPointerClickHandler
{
    public UnityAction<int> onClicking;
    public int equipNumber;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClicking.Invoke(equipNumber);
    }
}
