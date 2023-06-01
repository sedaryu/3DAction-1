using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointCircleController : MonoBehaviour
{
    //パラメーター
    private EnemyParameter parameter;
    //Fill
    private GameObject fill;

    private bool groggy;

    void Awake()
    {
        parameter = GetComponentInParent<EnemyParameter>();
        fill = transform.Find("HitPointCircleFill").gameObject;
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
        fill.transform.localScale = new Vector2(1 - parameter.PercentageParameter("HitPoint"), 1 - parameter.PercentageParameter("HitPoint"));
    }
}
