using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayerShooter : MonoBehaviour
{
    //targetingEnemies‚É•ß‘¨‚µ‚½“G‚ÌCollider‚ğŠi”[
    private List<Collider> targetingEnemies = new List<Collider>();

    public List<Collider> Fire(int bullet, float knockback, float attack, ParticleSystem gunEffect)
    {
        if (targetingEnemies.Count <= 0) return null;

        RemoveDestroyedEnemyInLockOn(); //”jŠü‚³‚ê‚½“G‚ª•ß‘¨ƒŠƒXƒg‚É‚¢‚½ê‡A‚±‚Ìƒƒ\ƒbƒh‚ÅƒŠƒXƒg‚©‚çíœ
        if (bullet <= 0) return null; //c’e‚ª‚È‚¢ê‡AUŒ‚‚Å‚«‚È‚¢
        List<Collider> enemies = HittingEnemy(targetingEnemies); //UŒ‚‚ğÀs
        List<Vector3> enemyVectors = enemies.Select(x => (x.transform.position - transform.position).normalized).ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<ITargetable>().Hit(enemyVectors[i] * knockback, attack); //ITargetable‚ÌHitƒƒ\ƒbƒh‚É’l‚ğ“n‚µÀs
        }
        LookAt(enemies[0].transform); //UŒ‚‚µ‚½“G‚Ì•ûŒü‚ğU‚èŒü‚­
        gunEffect.Play(); //ƒGƒtƒFƒNƒg‚ğÄ¶
        return enemies.Where(x => x.TryGetComponent<IGrogable>(out IGrogable grog) == true && grog.Groggy == true).ToList();
        
    }

    public abstract List<Collider> HittingEnemy(List<Collider> targets);

    public abstract void LookAt(Transform trans);

    //“G‚Ì•ß‘¨
    public void EnemyEnterTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Add(other); //“G‚ÌCollider‚ğæ“¾
    }

    //“G‚Ì•ß‘¨‰ğœ
    public void EnemyExitTarget(Collider other)
    {
        if (!other.TryGetComponent<ITargetable>(out ITargetable target)) return;
        targetingEnemies.Remove(other); //Collider‚Ì”ÍˆÍ‚©‚çŠO‚ê‚½ê‡AƒŠƒXƒg‚©‚çœŠO
    }

    //”jŠü‚³‚ê‚½“G‚ğƒŠƒXƒg‚©‚çœŠO
    private void RemoveDestroyedEnemyInLockOn()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
