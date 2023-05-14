using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointCircleController : MonoBehaviour
{
    //パラメーター
    private EnemyParameter status;
    //Fill
    private GameObject fill;

    private bool groggy;

    void Awake()
    {
        status = GetComponentInParent<EnemyParameter>();
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
        fill.transform.localScale = new Vector2(1 - status.EnemyParam.HitPoint / status.EnemyParam.HitPointMax, 1 - status.EnemyParam.HitPoint / status.EnemyParam.HitPointMax);
    }

    private void ChangeGroggyColor()
    {
        fill.GetComponent<SpriteRenderer>().color = Color.cyan;
    }
}
