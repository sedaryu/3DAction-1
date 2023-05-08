using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerDamage
{
    //ステータス
    private PlayerStatus status;
    //色
    private SkinnedMeshRenderer skinRenderer;
    private Color32 initialColor;
    private Color32 transparentColor;

    public PlayerDamage(PlayerStatus _status)
    {
        status = _status;
        skinRenderer = _status.transform.Find("body").GetComponent<SkinnedMeshRenderer>();
        initialColor = skinRenderer.material.color;
        transparentColor = new Color32(initialColor.r, 0, 0, initialColor.a);
    }

    public void EnemyAttackPlayer(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!status.IsNormal) return;
            EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemy.IsSmashable) return;
            if (status.Damage(enemy.Param.Attack)) status.GoToDieStateIfPossible();
            InvincibleTime();
        }
    }

    private async Task InvincibleTime()
    {
        status.GoToInvincibleStateIfPossible();
        skinRenderer.material.color = transparentColor;
        await Task.Delay(2000);
        status.GoToNormalStateIfPossible();
        skinRenderer.material.color = initialColor;
    }
}
