using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobParam", menuName = "Custom/MobParam")]
public class MobParam : ScriptableObject
{
    public float HitPoint //0‚É‚È‚é‚Æ‚Æ‚Ç‚ß‚ðŽh‚³‚ê‚é
    {
        get => _hitPoint;
    }
    [SerializeField] private float _hitPoint;

    public float Speed //ˆÚ“®‘¬“x
    {
        get => _speed;
    }
    [SerializeField] private float _speed;
}
