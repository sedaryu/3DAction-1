using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IGatherable
{
    [SerializeField] private ItemParam param;
    private float delta;

    //UI
    protected FilledCircleController timeCircleController;

    private void Start()
    {
        delta = param.Time;
        timeCircleController = transform.Find("TimeCircle").gameObject.GetComponent<FilledCircleController>();
    }

    private void Update()
    {
        delta -= Time.deltaTime;
        timeCircleController.UpdateFill(delta / param.Time);
        if (delta <= 0) Disappear();
    }

    public ItemParam Gathered(out float weight)
    {
        GetComponent<Collider>().enabled = false;
        weight = param.Weight;
        Disappear();
        return param;
    }

    public void Disappear()
    { 
        Destroy(gameObject);
    }
}
