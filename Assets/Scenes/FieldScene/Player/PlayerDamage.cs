using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    //ステータス
    private PlayerStatus status;
    //色
    private SkinnedMeshRenderer skinRenderer;
    private Color32 initialColor;
    private Color32 transparentColor;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        skinRenderer = transform.Find("body").GetComponent<SkinnedMeshRenderer>();
        initialColor = skinRenderer.material.color;
        transparentColor = new Color32(initialColor.r, 0, 0, initialColor.a);
    }

    public void EnemyAttackPlayer(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!status.IsDamagable) return;
            EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemy.IsFinishable) return;
            status.Damage(enemy.Param.Attack);
            StartCoroutine(InvincibleTime());
        }
    }

    private IEnumerator InvincibleTime()
    {
        status.GoToInvincibleStateIfPossible();
        skinRenderer.material.color = transparentColor;
        yield return new WaitForSeconds(2f);
        status.GoToNormalStateIfPossible();
        skinRenderer.material.color = initialColor;
    }
}
