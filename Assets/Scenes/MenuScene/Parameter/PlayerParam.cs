using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParam
{
    public Gun Gun { get; private set; }
    public Smash Smash { get; private set; }
    public Shoes Shoes { get; private set; }

    public PlayerParam(Gun gun, Smash smash, Shoes shoes)
    { 
        Gun = gun;
        Smash = smash;
        Shoes = shoes;
    }
}
