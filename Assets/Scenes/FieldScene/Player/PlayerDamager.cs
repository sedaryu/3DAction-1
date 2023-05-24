using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    //êF
    private SkinnedMeshRenderer skinRenderer;
    private Color32 initialColor;
    private Color32 noDamageColor;

    void Awake()
    {
        skinRenderer = transform.Find("body").GetComponent<SkinnedMeshRenderer>();
        initialColor = skinRenderer.sharedMaterial.color;
        noDamageColor = new Color32(initialColor.r, 0, 0, initialColor.a);
    }

    public string Damage(Collider other, float time, out float damage)
    {
        if (!other.TryGetComponent<IAttackable>(out IAttackable enemy))
        { 
            damage = 0;
            return "";
        }
        damage = enemy.Attack();
        string key = enemy.AttackKey;
        if (damage == 0) return "";
        if (key == "Life") StartCoroutine(WaitForRendererColorTransition(time));
        return key;
    }

    private IEnumerator WaitForRendererColorTransition(float time)
    {
        skinRenderer.sharedMaterial.color = noDamageColor;
        yield return new WaitForSeconds(time);
        skinRenderer.sharedMaterial.color = initialColor;
    }
}
