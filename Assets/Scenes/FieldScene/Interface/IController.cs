using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    Vector3 InputMoving();

    bool InputFiring();

    bool InputReloading();

    bool InputSmashing();
}
