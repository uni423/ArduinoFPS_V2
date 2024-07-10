using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveRbUnitObject : RabbitUnitObject
{
    EvolveRbUnit rabbit;

    public override void SetAgent(Unit _unit)
    {
        base.SetAgent(_unit);

        rabbit = unit as EvolveRbUnit;
        cachedRigidbody.useGravity = false;
    }
}
