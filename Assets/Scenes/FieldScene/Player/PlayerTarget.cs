using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    //targetingEnemies‚É•ß‘¨‚µ‚½“G‚ÌEnemyReferenced‚ğŠi”[
    public List<EnemyReferenced> targetingEnemies = new List<EnemyReferenced>();

    //“G‚Ì•ß‘¨
    public void EnemyEnterTarget(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyAct>() == null) return;
            targetingEnemies.Add(other.GetComponent<EnemyReferenced>()); //“G‚ÌEnemyReferencedƒNƒ‰ƒX‚ğæ“¾
        }
    }

    //“G‚Ì•ß‘¨‰ğœ
    public void EnemyExitTarget(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetingEnemies.Remove(other.GetComponent<EnemyReferenced>()); //Collider‚Ì”ÍˆÍ‚©‚çŠO‚ê‚½ê‡AƒŠƒXƒg‚©‚çœŠO
        }
    }

    //”jŠü‚³‚ê‚½“G‚ğƒŠƒXƒg‚©‚çœŠO
    private void RemoveDestroyedEnemyInLockOn()
    {
        targetingEnemies.RemoveAll(x => x == null);
    }
}
