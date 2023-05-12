using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour, IAnimator
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetValue(string name, float speed)
    {
        animator.SetFloat("MoveSpeed", speed);
    }
}
