using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentUI : SelectModeUI
{
    private void Awake()
    {
        EquipmentGunParam equipGuns = new LoadJson().LoadEquipmentGunParam();
        GameObject[] gunObjects = GameObject.Find("LoadAsset").GetComponent<LoadAsset>().LoadObjects("Gun", equipGuns.guns.ToArray());
        for (int i = 0; i < gunObjects.Length; i++)
        {
            Vector3 position;
            if (i < 3) position = new Vector3(0.9f + (0.28f * i), 5.15f, 1);
            else position = new Vector3(0.9f + (0.28f * (i - 3)), 4.9f, 1);
            GameObject gun = Instantiate(gunObjects[i], position, Quaternion.Euler(0, 90, 0));
            gun.transform.localScale *= 0.75f;
            SelectEquipmentGunUI gunUI = gun.AddComponent<SelectEquipmentGunUI>();
            gunUI.onClicking += UpdateSublineText;
            gunUI.isEquiping = equipGuns.equipNumber == i ? true : false;
            colliders.Add(gun.GetComponent<Collider>());
        }
    }

    private void UpdateSublineText(string text)
    {
        sublineText.text = text;
    }
}
