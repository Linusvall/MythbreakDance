using System.Collections.Generic;
using UnityEngine;

public class PlayMode : MonoBehaviour
{
    [SerializeField] private List<AudioClip> songArray = new List<AudioClip>();
    [SerializeField] private BeatmapGenerator beatmapManager;
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private GameObject[] spawnPositionArrayPlayer1;
    [SerializeField] private GameObject[] spawnPositionArrayPlayer2;


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
                    SpawnPrefab(1);
                    SpawnPrefab(2);
                    
                    currentIndex++;
                }
            }
        }
    }

    private void SpawnPrefab(int playerIndex)
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
        danceArrow.transform.position = new Vector3(GetCorrectPositionX(currentBeatmap.beatEvents[currentIndex].direction, playerIndex), danceArrow.transform.position.y, danceArrow.transform.position.z);
        danceArrow.GetComponent<DanceArrow>().SetTarget(GetCorrectPosition(currentBeatmap.beatEvents[currentIndex].direction, playerIndex));
    }

    private float GetTime()
    {
        return Time.realtimeSinceStartup - startTime;
    }

    private float GetCorrectPositionX(string directionName, int playerIndex)
    {
        if (playerIndex == 1)
        {
            for (int i = 0; i < spawnPositionArrayPlayer1.Length; i++)
            {
                if (spawnPositionArrayPlayer1[i].GetComponent<CatchBox>().direction == directionName)
                {
                    return spawnPositionArrayPlayer1[i].transform.position.x;
                }
            }
        }
        else
        {
            for (int i = 0; i < spawnPositionArrayPlayer2.Length; i++)
            {
                if (spawnPositionArrayPlayer2[i].GetComponent<CatchBox>().direction == directionName)
                {
                    return spawnPositionArrayPlayer2[i].transform.position.x;
                }
            }
        }
        return 0;
    }

    private Vector3 GetCorrectPosition(string directionName, int playerIndex)
    {
        if(playerIndex == 1)
        {
            for (int i = 0; i < spawnPositionArrayPlayer1.Length; i++)
            {
                if (spawnPositionArrayPlayer1[i].GetComponent<CatchBox>().direction == directionName)
                {
                    return spawnPositionArrayPlayer1[i].transform.position;
                }
            }
        }
        else
        {
            for (int i = 0; i < spawnPositionArrayPlayer2.Length; i++)
            {
                if (spawnPositionArrayPlayer2[i].GetComponent<CatchBox>().direction == directionName)
                {
                    return spawnPositionArrayPlayer2[i].transform.position;
                }
            }
        }
        
        return new Vector3(0,0,0);
    }
}
