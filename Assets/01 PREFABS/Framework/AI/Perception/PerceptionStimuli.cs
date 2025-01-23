using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionStimuli : MonoBehaviour
{
    private void Start()
    {
        SenseComp.RegisterStimuli(this);
    }

    private void OnDestroy()
    {
        SenseComp.UnRegisterStimuli(this);
    }
}
