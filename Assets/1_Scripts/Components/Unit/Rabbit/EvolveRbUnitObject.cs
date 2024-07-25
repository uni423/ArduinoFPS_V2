using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EvolveRbUnitObject : RabbitUnitObject
{
    EvolveRbUnit rabbit;

    public override void SetAgent(Unit _unit)
    {
        base.SetAgent(_unit);

        rabbit = unit as EvolveRbUnit;
    }
}
