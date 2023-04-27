using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MobStatus
{
    //パラメーター
    public PlayerParam Param
    {
        get => _param;
    }
    private PlayerParam _param;

    [SerializeField] private PlayerParam initialParam;

    protected override void Awake()
    {
        base.Awake();
        _param = new PlayerParam(initialParam);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
