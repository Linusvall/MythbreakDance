using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongBeat : MonoBehaviour
{
    [SerializeField] public List<AudioClip> songArray = new List<AudioClip>();
    [SerializeField] private BeatmapGenerator beatmapManager;
    private Beatmap currentBeatmap;
    void Start()
    {
        if (beatmapManager != null)
        {
            currentBeatmap = beatmapManager.LoadJsonFile(songArray[0].name);
        }

        for (int i = 0; i < currentBeatmap.beatEvents.Count; i++)
        {
            print(currentBeatmap.beatEvents[i].time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
