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
    [SerializeField] private GameObject[] spawnPositionArray;


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
                if(GetTime() >= currentBeatmap.beatEvents[currentIndex].time - 2f)
                {
                    GameObject danceArrow = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
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

                         case "Left":
                            danceArrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                            break;

                        default:
                            Debug.Log("Invalid direction");
                            break;
                    }
                    danceArrow.transform.position = new Vector3(GetCorrectPositionX(currentBeatmap.beatEvents[currentIndex].direction), danceArrow.transform.position.y, danceArrow.transform.position.z);
                    danceArrow.GetComponent<DanceArrow>().SetTarget(GetCorrectPosition(currentBeatmap.beatEvents[currentIndex].direction));

                    currentIndex++;
                }
            }
        }
    }

    private float GetTime()
    {
        return Time.realtimeSinceStartup - startTime;
    }

    private float GetCorrectPositionX(string directionName)
    {
        for(int i = 0; i < spawnPositionArray.Length; i++)
        {
            if(spawnPositionArray[i].GetComponent<CatchBox>().direction == directionName)
            {
                return spawnPositionArray[i].transform.position.x;
            }
        }
        return 0;
    }

    private Vector3 GetCorrectPosition(string directionName)
    {
        for (int i = 0; i < spawnPositionArray.Length; i++)
        {
            if (spawnPositionArray[i].GetComponent<CatchBox>().direction == directionName)
            {
                return spawnPositionArray[i].transform.position;
            }
        }
        return new Vector3(0,0,0);
    }
}
