using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <Summary>
/// プレイヤーのスマッシュ攻撃を処理するクラス
/// </Summary>
public class PlayerSmash
{
    //ステータス
    private PlayerParameter status;
    //エフェクター
    private MobEffecter effecter;

    //取得したコリダーオブジェクト
    private List<GameObject> colliders = new List<GameObject>();

    public PlayerSmash(PlayerParameter _status, MobEffecter _effecter)
    {
        status = _status;
        effecter = _effecter;
    }

    /// <summary>
    /// スマッシュコリダーに接触した際、そのコリダーのサイズを拡大し、
    /// そのコリダーをcollidersに追加する
    /// </summary>
    /// <param name="other">接触したコリダー</param>
    public void PlayerInSmashRange(Collider other)
    {
        if (other.CompareTag("Smash"))
        {
            other.transform.localScale = Vector3.one * status.SmashParam.RangeMax; //コリダーを拡大
            colliders.Add(other.transform.parent.gameObject); //追加
        }
    }

    /// <summary>
    /// スマッシュコリダーから離れた際、そのコリダーのサイズを元に戻し、
    /// そのコリダーをcollidersから除外する
    /// </summary>
    /// <param name="other">離れたコリダー</param>
    public void PlayerOutSmashRange(Collider other)
    {
        if (other.CompareTag("Smash"))
        {
            other.transform.localScale = Vector3.one * status.SmashParam.RangeMin; //コリダーのサイズを元に戻す
            colliders.Remove(other.transform.parent.gameObject); //除外
        }
    }

    /// <summary>
    /// スマッシュ攻撃を実行するためのメソッド
    /// </summary>
    /// <param name="input">入力された場合はtrue</param>
    public void Smash(bool input)
    {
        if (!input) return;
        if (status.IsNoMoveInvincible) return; //移動不可無敵の際は実行できない
        if (colliders.Count <= 0) return; //collidersに何も含まれていない場合は実行できない
        if (RemoveDestroyedCollider()) return; //破棄されたコリダーを除外
        foreach (GameObject x in colliders) 
        {
            Task _0 = x.GetComponentInChildren<SmashAct>().PlayerSmashEnemies(status.SmashParam); //コリダー内の敵全てにスマッシュ攻撃
            Task _1 = SmashTime(x.transform.position, x.transform.rotation); //
        }
    }

    /// <summary>
    /// スマッシュ攻撃の際のプレイヤーのstate遷移、
    /// アニメーション実行、エフェクト生成を行うメソッド
    /// </summary>
    /// <param name="position">エフェクトを生成する場所</param>
    /// <param name="rotation">エフェクトの角度</param>
    private async Task SmashTime(Vector3 position, Quaternion rotation)
    {
        status.Animator.SetTrigger("StartSmash");
        status.GoToNoMoveInvincibleStateIfPossible();
        await Task.Delay(TimeSpan.FromSeconds(status.SmashParam.SmashTime * 0.7f));
        //await Task.Delay((int)(status.SmashParam.SmashTime * 700));
        status.Animator.SetTrigger("FinishSmash");
        await Task.Delay(TimeSpan.FromSeconds(status.SmashParam.SmashTime * 0.3f));
        //await Task.Delay((int)(status.SmashParam.SmashTime * 300));
        effecter.InstanceEffect(status.SmashParam.SmashEffect, position, rotation);
        status.GoToNormalStateIfPossible();
    }

    //破棄されたコリダーをリストから除外
    private bool RemoveDestroyedCollider()
    {
        colliders.RemoveAll(x => x == null);
        if (colliders.Count == 0) return true;
        return false;
    }
}
