﻿using UnityEngine;
using System.Collections;

public class CollidableMoveableRotateableGridObject : CollidableMoveableGridObject, Rotateable
{
    public Globals.Direction direction;
    
    public Globals.Direction GetDirection()
    {
        return direction;
    }

    public void SetDirection(Globals.Direction dir)
    {
        direction = dir;
    }

    public void Rotate(Globals.Direction dir)
    {
        direction = dir;
    }
}
