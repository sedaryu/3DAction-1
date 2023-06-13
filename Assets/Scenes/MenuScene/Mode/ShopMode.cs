using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopMode : SelectModeUI
{
    [SerializeField] private Collider eagle;
    [SerializeField] private Collider pan;
    private BackUI backUI;

    [SerializeField]private Animator eagleAnimator;

    private MenuParam param;
    private List<GameObject> guns;
    private GameObject displayGun;
    private int selectedItem = 0;

    private void Awake()
    {
        colliders.Add(eagle); colliders.Add(pan);
        backUI = transform.parent.transform.Find("Back").GetComponent<BackUI>();
        guns = new LoadAsset().LoadObjectsAll("Gun").ToList();
        eagle.transform.position = new Vector3(0, 0, 0);
        onClicking += () =>
        {
            eagle.transform.position = new Vector3(-0.968f, 5.574f, -1.179f);
            eagleAnimator.SetTrigger("Attack");
        };
        onClicked += () =>
        {
            headlineText.rectTransform.sizeDelta = new Vector2(150, 50);
            headlineText.rectTransform.anchoredPosition = new Vector3(-65, 110, 0);
            headlineText.text = guns[selectedItem].name;
            sublineText.fontSize = 7;
            sublineText.rectTransform.sizeDelta = new Vector2(350, 70);
            sublineText.rectTransform.anchoredPosition = new Vector3(5, 5, 0);
            param = new LoadJson().LoadMenuParam();
            sublineText.text = $"Clicking Eagle, You Can Change Item\n" +
                               $"Clicking Item, You Can Buy It\n" + 
                               $"You Have {param.Parameter("Point")} Point : " +
                               $"This Item Cost {guns[selectedItem].GetComponent<Gun>().Param.Prise}";
            displayGun = Instantiate(guns[selectedItem], new Vector3(-1.57f, 6.07f, -1.08f), Quaternion.Euler(0, -90, 0));
        };
    }

    public void OnClickEagleHead()
    {
        eagleAnimator.SetTrigger("Hit");
        selectedItem++;
        if (selectedItem == guns.Count) selectedItem = 0;
        UpdateText();
        Destroy(displayGun);
        displayGun = Instantiate(guns[selectedItem], new Vector3(-1.57f, 6.07f, -1.08f), Quaternion.Euler(0, -90, 0));
    }

    public void OnClickPan()
    {
        GunParam gun = guns[selectedItem].GetComponent<Gun>().Param;
        if (param.Parameter("Point") < gun.Prise)
        { headlineText.text = "Not Enough Point"; return; }
        EquipmentGunParam equip = new LoadJson().LoadEquipmentGunParam();
        if (equip.guns.Count >= 6)
        { equip.guns.RemoveAt(0); equip.guns.Add(gun.name); }
        else equip.guns.Add(gun.name);
        new LoadJson().SaveEquipmentGunParam(equip);
        eagleAnimator.SetTrigger("Attack");
    }

    private void UpdateText()
    {
        GunParam gun = guns[selectedItem].GetComponent<Gun>().Param;
        headlineText.text = gun.Name;
        sublineText.text = $"Clicking Eagle, You Can Change Item\n" +
                           $"Clicking Item, You Can Buy It\n" +
                           $"You Have {param.Parameter("Point")} Point : " +
                           $"This Item Cost {gun.Prise}";
    }
}
