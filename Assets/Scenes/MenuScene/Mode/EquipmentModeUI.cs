using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentModeUI : SelectModeUI
{
    [SerializeField] private Collider bigWrench;
    private EquipmentGunParam equipGuns;
    private int selectedNumber;
    private List<GunParam> gunParams = new List<GunParam>();

    private void Awake()
    {
        SetGunUI();
        initialText = "Clicking Gun, \nYou Can Select Gun And See Parameter\n" +
                      "Clicking Big Wrench, \nYou Can Equip Selected Gun";
    }

    private void SetGunUI()
    {
        equipGuns = new LoadJson().LoadEquipmentGunParam();
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
            gunUI.onClicking += SelectEquipNumber;
            gunUI.equipNumber = i;
            gunParams.Add(gun.GetComponent<Gun>().Param);
            colliders.Add(gun.GetComponent<Collider>());
        }
        colliders.Add(bigWrench);
    }

    public void UpdateGunUI()
    {
        gunParams.Clear();
        colliders.Clear();
        SetGunUI();
    }

    private void UpdateSublineText(int number)
    {
        string equip = number == equipGuns.equipNumber ? "NowEquiping!" : "";
        string text = $"{gunParams[number].Name} {equip}\n" +
                      $"Reach: {gunParams[number].Reach} Range:{gunParams[number].Range}\n" +
                      $"Attack: {gunParams[number].Attack} Bullet: {gunParams[number].BulletMax} " +
                      $"Reload: {gunParams[number].ReloadSpeed}\n" +
                      $"Knockback: {gunParams[number].Knockback} " +
                      $"Critical: {gunParams[number].CriticalMin} ~ {gunParams[number].CriticalMin + gunParams[number].CriticalAdd}";
        sublineText.text = text;
    }

    private void SelectEquipNumber(int number)
    {
        selectedNumber = number;
    }

    public void ChangeEquipGun()
    { 
        equipGuns.equipNumber = selectedNumber;
        UpdateSublineText(equipGuns.equipNumber);
        new LoadJson().SaveEquipmentGunParam(equipGuns);
    }
}
