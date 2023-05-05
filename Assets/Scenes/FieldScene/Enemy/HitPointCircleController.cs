using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointCircleController : MonoBehaviour
{
    //パラメーター
    private EnemyStatus status;
    //Fill
    private GameObject fill;

    private bool groggy;

    void Awake()
    {
        status = GetComponentInParent<EnemyStatus>();
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

        if (status.IsSmashable && !groggy)
        { 
            groggy = true;
            ChangeGroggyColor();
        } 
    }

    private void FillControll()
    {
        fill.transform.localScale = new Vector2(1 - status.Param.HitPoint / status.Param.HitPointMax, 1 - status.Param.HitPoint / status.Param.HitPointMax);
    }

    private void ChangeGroggyColor()
    {
        fill.GetComponent<SpriteRenderer>().color = Color.cyan;
    }
}
