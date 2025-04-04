using System.Collections.Generic;
using UnityEngine;

public class PlayMode : MonoBehaviour
{
    [SerializeField] private List<AudioClip> songArray = new List<AudioClip>();
    [SerializeField] private BeatmapGenerator beatmapManager;
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private GameObject[] spawnPositionArrayPlayer1;

    [SerializeField] private Material leftArrowMaterial;
    [SerializeField] private Material rightArrowMaterial;
    [SerializeField] private Material upArrowMaterial;
    [SerializeField] private Material downArrowMaterial;

    private Beatmap currentBeatmap;
    private AudioSource audioSource;
    private float startTime;
    private int currentIndex = 0;
    private bool isPlaying;

    [SerializeField] private string songToPlay;
    [SerializeField] private float waitToPlay;
    [SerializeField] private Animator animator1;
    [SerializeField] private Animator animator2;




    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySong(songToPlay);

        if(animator1 != null)
        {
            animator1.Play("Unreal Take");
        }

        if (animator2 != null)
        {
            animator2.Play("Unreal Take");
        }
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
                    
                    currentIndex++;
                }
            }

            if(audioSource.time >= audioSource.clip.length - 0.1f)
            {
                //Ending game here
                isPlaying = false;
            }
        }
    }

    private void SpawnPrefab(int playerIndex)
    {
        GameObject danceArrow = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        danceArrow.GetComponent<DanceArrow>().SetDirection(currentBeatmap.beatEvents[currentIndex].direction);
        Renderer renderer = danceArrow.GetComponentInChildren<Renderer>();

        switch (currentBeatmap.beatEvents[currentIndex].direction)
        {
            case "Up":
                renderer.material = upArrowMaterial;
                break;
            case "Down":
                renderer.material = downArrowMaterial;
                break;
            case "Right":
                renderer.material = rightArrowMaterial;
                break;

            case "Left":
                renderer.material = leftArrowMaterial;
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
            for (int i = 0; i < spawnPositionArrayPlayer1.Length; i++)
            {
                if (spawnPositionArrayPlayer1[i].GetComponent<CatchBox>().direction == directionName)
                {
                    return spawnPositionArrayPlayer1[i].transform.position.x;
                }
            }
        
            
        
        return 0;
    }

    private Vector3 GetCorrectPosition(string directionName, int playerIndex)
    {
        
            for (int i = 0; i < spawnPositionArrayPlayer1.Length; i++)
            {
                if (spawnPositionArrayPlayer1[i].GetComponent<CatchBox>().direction == directionName)
                {
                    return spawnPositionArrayPlayer1[i].transform.position;
                }
            }
        
        
        
        return new Vector3(0,0,0);
    }
}
