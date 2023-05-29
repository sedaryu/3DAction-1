using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IGatherable
{
    [SerializeField] private string type;
    [SerializeField] private float weight;
    [SerializeField] private float time;
    private float delta;

    //UI
    protected FilledCircleController timeCircleController;

    private void Start()
    {
        delta = time;
        timeCircleController = transform.Find("TimeCircle").gameObject.GetComponent<FilledCircleController>();
    }

    private void Update()
    {
        delta -= Time.deltaTime;
        timeCircleController.UpdateFill(delta / time);
        if (delta <= 0) Disappear();
    }

    public string Gathered(out float _weight)
    {
        GetComponent<Collider>().enabled = false;
        _weight = weight;
        Disappear();
        return type;
    }

    public void Disappear()
    { 
        Destroy(gameObject);
    }
}
