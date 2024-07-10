using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkUpRbUnitObject : RabbitUnitObject
{
    BulkUpRbUnit rabbit;

    public override void SetAgent(Unit _unit)
    {
        base.SetAgent(_unit);

        rabbit = unit as BulkUpRbUnit;
    }
}
