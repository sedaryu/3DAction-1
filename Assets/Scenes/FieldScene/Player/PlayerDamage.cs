using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

/// <summary>
/// プレイヤーがダメージを受けた際の処理を実行する目的のクラス
/// </summary>
public class PlayerDamage : Act
{
    public event Func<bool> isNormal;

    //色
    private SkinnedMeshRenderer skinRenderer;
    private Color32 initialColor;
    private Color32 transparentColor;

    public PlayerDamage()
    {
        skinRenderer = _status.transform.Find("body").GetComponent<SkinnedMeshRenderer>();
        initialColor = skinRenderer.sharedMaterial.color;
        transparentColor = new Color32(initialColor.r, 0, 0, initialColor.a);
    }

    public void Damage(float damage)
    {
        if (!isNormal.Invoke()) return;
        OnTrigger("Damage", damage);
        Task _ = InvincibleTime();
    }

    private async Task InvincibleTime()
    {
        status.GoToInvincibleStateIfPossible();
        skinRenderer.sharedMaterial.color = transparentColor;
        await Task.Delay(2000);
        status.GoToNormalStateIfPossible();
        skinRenderer.sharedMaterial.color = initialColor;
    }
}
