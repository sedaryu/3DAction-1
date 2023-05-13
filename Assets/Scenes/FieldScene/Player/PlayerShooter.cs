using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerShooter : MonoBehaviour
{
    //targetingEnemies‚É•ß‘¨‚µ‚½“G‚ÌITargetable‚ğŠi”[
    public List<ITargetable> targetingEnemies = new List<ITargetable>();

    public void Fire(int bullet, float knockback, float attack, ParticleSystem gunEffect)
    {
        if (targetingEnemies.Count <= 0) return;

        RemoveDestroyedEnemyInLockOn(); //”jŠü‚³‚ê‚½“G‚ª•ß‘¨ƒŠƒXƒg‚É‚¢‚½ê‡A‚±‚Ìƒƒ\ƒbƒh‚ÅƒŠƒXƒg‚©‚çíœ
        if (bullet <= 0) return; //c’e‚ª‚È‚¢ê‡AUŒ‚‚Å‚«‚È‚¢
        Transform enemy = HittingEnemy(transform.position, targetingEnemies, knockback, attack); //UŒ‚‚ğÀs
        transform.LookAt(enemy); //UŒ‚‚µ‚½“G‚Ì•ûŒü‚ğU‚èŒü‚­
        gunEffect.Play(); //ƒGƒtƒFƒNƒg‚ğÄ¶
    }

    public abstract Transform HittingEnemy(Vector3 position, List<ITargetable> targets, float knockback, float attack);

    //“G‚Ì•ß‘¨
    public void EnemyEnterTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Add(target); //“G‚ÌITargetableƒNƒ‰ƒX‚ğæ“¾
    }

    //“G‚Ì•ß‘¨‰ğœ
    public void EnemyExitTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Remove(target); //Collider‚Ì”ÍˆÍ‚©‚çŠO‚ê‚½ê‡AƒŠƒXƒg‚©‚çœŠO
    }

    //”jŠü‚³‚ê‚½“G‚ğƒŠƒXƒg‚©‚çœŠO
    private void RemoveDestroyedEnemyInLockOn()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
