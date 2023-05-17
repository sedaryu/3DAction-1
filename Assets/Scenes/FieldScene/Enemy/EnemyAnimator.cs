using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetFloat(string name, float param)
    {
        animator.SetFloat(name, param);
    }

    public void SetTrriger(string name)
    {
        animator.SetTrigger(name);
    }
}
