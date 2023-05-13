using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class SmashAct : MonoBehaviour, ISmashable
{
    private MeshRenderer meshRenderer;

    private IEnumerator coroutine;
}
