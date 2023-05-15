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

    public float Damage(Collider other, float time)
    {
        if (!other.TryGetComponent<IAttackable>(out IAttackable enemy)) return 0;
        float damage = enemy.Attack();
        if (damage == 0) return 0;
        StartCoroutine(WaitForRendererColorTransition(time));
        return damage;
    }

    private IEnumerator WaitForRendererColorTransition(float time)
    {
        skinRenderer.sharedMaterial.color = noDamageColor;
        yield return new WaitForSeconds(time);
        skinRenderer.sharedMaterial.color = initialColor;
    }
}
