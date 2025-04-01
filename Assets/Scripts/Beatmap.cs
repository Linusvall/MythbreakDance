using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeatEvent
{
    public float time; 
    public string direction; 
}

public class Beatmap
{

    public List<BeatEvent> beatEvents = new List<BeatEvent>();

    public void AddEvent(float time, string direction)
    {
        beatEvents.Add(new BeatEvent { time = time, direction = direction });
    }
    
}
