using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointCircleController : MonoBehaviour
{
    //パラメーター
    private EnemyStatus status;
    //Fill
    private Transform fill;

    void Awake()
    {
        status = GetComponentInParent<EnemyStatus>();
        fill = transform.Find("HitPointCircleFill");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FillControll();
    }

    private void FillControll()
    {
        fill.localScale = new Vector2(1 - status.Param.HitPoint / status.Param.HitPointMax, 1 - status.Param.HitPoint / status.Param.HitPointMax);
    }
}
