using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParam
{
    public Gun Gun { get; private set; }
    public Smash Smash { get; private set; }
    public Shoes Shoes { get; private set; }
    public float Life { get; private set; }

    public PlayerParam(Gun gun, Smash smash, Shoes shoes, float life)
    { 
        Gun = gun;
        Smash = smash;
        Shoes = shoes;
        Life = life;
    }
}
