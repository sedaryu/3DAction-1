using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class SmashAct : MonoBehaviour
{
    private Smasher smasher;

    private CollisionDetecter targetCollisionDetecter;

    private MeshRenderer meshRenderer;

    private IEnumerator coroutine;

    void Awake()
    {
        smasher = GetComponent<Smasher>();
        targetCollisionDetecter = GetComponent<CollisionDetecter>();
    }
}
