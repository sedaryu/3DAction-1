using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    //�G�t�F�N�g�̃v���n�u
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject smashHitEffect;

    public void PlayHitEffect()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
    }

    public void PlaySmashHitEffect()
    {
        Instantiate(smashHitEffect, transform.position, transform.rotation);
    }
}
