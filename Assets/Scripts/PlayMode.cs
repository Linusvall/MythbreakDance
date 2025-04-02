using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Windows;

public class PlayMode : MonoBehaviour
{
    [SerializeField] private List<AudioClip> songArray = new List<AudioClip>();
    [SerializeField] private BeatmapGenerator beatmapManager;
    [SerializeField] private GameObject prefabToSpawn;
    

    private Beatmap currentBeatmap;
    private AudioSource audioSource;
    private float startTime;
    private int currentIndex = 0;

    private bool isPlaying;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySong(string songName)
    {
        AudioClip clipToPlay = GetSong(songName);
        if (clipToPlay != null)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
            currentBeatmap = beatmapManager.LoadJsonFile(songName);
            startTime = Time.realtimeSinceStartup;
            isPlaying = true;

        }
    }

    public AudioClip GetSong(string songName)
    {
        foreach (AudioClip clip in songArray)
        {
            if (clip.name == songName)
            {
                return clip;
            }
        }
        return null;
    }

    private void Update()
    {

        if (isPlaying)
        {
            if (currentIndex >= 0 && currentIndex < currentBeatmap.beatEvents.Count)
            {
                if(GetTime() >= currentBeatmap.beatEvents[currentIndex].time)
                {
                    GameObject danceArrow = Instantiate(prefabToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
                    danceArrow.GetComponent<DanceArrow>().SetDirection(currentBeatmap.beatEvents[currentIndex].direction);
                   

                    switch (currentBeatmap.beatEvents[currentIndex].direction)
                    {
                        case "Up":
                            danceArrow.transform.rotation = Quaternion.Euler(0, 0, -90);
                            break;
                        case "Down":
                            danceArrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                            break;
                        case "Right":
                            danceArrow.transform.rotation = Quaternion.Euler(0, 0, -180);
                            break;
                        default:
                            Debug.Log("Invalid direction");
                            break;
                    }
                    currentIndex++;
                }
            }
        }
    }

    private float GetTime()
    {
        return Time.realtimeSinceStartup - startTime;
    }
}
