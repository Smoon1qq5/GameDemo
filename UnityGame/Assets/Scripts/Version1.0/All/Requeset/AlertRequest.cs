using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertRequest : BaseRequest
{
    protected override void Init()
    {
        actionCode = ActionCode.ActionNone;
        base.Init();
    }
}
