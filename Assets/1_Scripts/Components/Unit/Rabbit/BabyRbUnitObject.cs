using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BabyRbUnitObject : RabbitUnitObject
{
    BabyRbUnit rabbit;

    public override void SetAgent(Unit _unit)
    {
        base.SetAgent(_unit);

        rabbit = unit as BabyRbUnit;
    }
}
