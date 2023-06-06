using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectEquipmentGunUI : MonoBehaviour, IPointerClickHandler
{
    public UnityAction<string> onClicking;
    public bool isEquiping;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        GunParam param = GetComponent<Gun>().Param;
        string equip = isEquiping == true ? "NowEquiping!" : "";
        string text = $"{param.Name} {equip}\n" +
                      $"Reach: {param.Reach} Range:{param.Range}\n" +
                      $"Attack: {param.Attack} Bullet: {param.BulletMax} Reload: {param.ReloadSpeed}\n" +
                      $"Knockback: {param.Knockback} Critical: {param.CriticalMin} ~ {param.CriticalMin + param.CriticalAdd}";
        onClicking.Invoke(text);
    }
}
