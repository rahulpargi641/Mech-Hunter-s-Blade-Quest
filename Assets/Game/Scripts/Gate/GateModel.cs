using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateModel
{
    public float OpenDuration { get; private set; }
    public float OpenTargetY { get; private set; }
    public GateModel()
    {
        OpenDuration = 2f;
        OpenTargetY = -2f;
    }
}
