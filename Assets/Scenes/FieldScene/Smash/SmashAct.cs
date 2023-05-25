using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class SmashAct : MonoBehaviour
{
    private Smash smasher;

    private CollisionDetecter targetCollisionDetecter;

    private MeshRenderer meshRenderer;

    private IEnumerator coroutine;

    void Awake()
    {
        smasher = GetComponent<Smash>();
        targetCollisionDetecter = GetComponent<CollisionDetecter>();
    }
}
