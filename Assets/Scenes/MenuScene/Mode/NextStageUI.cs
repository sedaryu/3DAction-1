using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextStageUI : SelectActionUI
{
    public override async void OnPointerClick(PointerEventData eventData)
    {
        onClicking?.Invoke();
        Task task = await cameraController.MoveCamera(new Vector3(0.7f, 4.9f, 0.3f), new Vector3(5, 50, 0), 3000);
        onClicked?.Invoke();
    }
}
