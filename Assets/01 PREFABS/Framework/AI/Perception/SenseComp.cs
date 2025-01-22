using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SenseComp : MonoBehaviour
{
    private static List<PerceptionStimuli> registeredStimulis = new();
    private List<PerceptionStimuli> perceivableStimulis = new();

    public static void RegisterStimuli(PerceptionStimuli stimuli)
    {
        if (registeredStimulis.Contains(stimuli))
            return;

        registeredStimulis.Add(stimuli);
    }

    public static void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Remove(stimuli);
    }

    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    private void Update()
    {
        ProcessStimuli();
    }

    private void ProcessStimuli()
    {
        foreach (PerceptionStimuli stimuli in registeredStimulis)
        {
            bool isSensable = IsStimuliSensable(stimuli);
            bool isPerceivable = perceivableStimulis.Contains(stimuli);

            if (isSensable && !isPerceivable)
            {
                perceivableStimulis.Add(stimuli);
                Debug.Log($"I just sensed {stimuli.gameObject}");
            }
            else if (!isSensable && isPerceivable)
            {
                perceivableStimulis.Remove(stimuli);
                Debug.Log($"I lost track of {stimuli.gameObject}");
            }
        }
    }

    protected virtual void DrawDebug() { }

    private void OnDrawGizmos()
    {
        DrawDebug();
    }

}
