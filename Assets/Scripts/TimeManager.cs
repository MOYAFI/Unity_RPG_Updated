using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float lastTimeAttacked;
    private float inGameTime;

    // Update is called once per frame
    void Update()
    {
        inGameTime =  Time.time;
    }

    [ContextMenu("Save Attack Time")]
    public void SaveTime()
    {
        lastTimeAttacked = Time.time;
    }
}
