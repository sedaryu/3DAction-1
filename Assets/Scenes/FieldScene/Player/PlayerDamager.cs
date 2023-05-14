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

    public float Damage(Collider other)
    {
        if (!other.TryGetComponent<IAttackable>(out IAttackable enemy)) return 0;
        enemy.Attack();
        return enemy.Damage;
    }

    public IEnumerator WaitForRendererColorTransition(float time)
    {
        skinRenderer.sharedMaterial.color = noDamageColor;
        yield return new WaitForSeconds(time);
        skinRenderer.sharedMaterial.color = initialColor;
    }
}
