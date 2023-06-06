using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextStageUI : SelectModeUI
{
    [SerializeField] private TextMeshPro tvText;
    [SerializeField] private Collider mug;
    [SerializeField] private Collider telescope;

    private void Awake()
    {
        initialText = "Clicking Mugcup, \nChange Next Stage \nClicking Telescope, \nGo To Next Stage";
        colliders.Add(mug); colliders.Add(telescope);
        onClicked += SetSublineTextTransform;
    }

    private void SetSublineTextTransform()
    {
        sublineText.rectTransform.sizeDelta = new Vector2(145, 70);
    }

    public void OnClickMug()
    {
        Debug.Log("Mug");
    }

    public void OnClickTelescope()
    {
        Debug.Log("Telescope");
    }
}
