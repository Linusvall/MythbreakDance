using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongBeat : MonoBehaviour
{
    [SerializeField] private List<AudioClip> songArray = new List<AudioClip>();
    [SerializeField] private BeatmapGenerator beatmapManager;
    [SerializeField] private EditMode editMode;

    private Beatmap currentBeatmap;
    private AudioSource audioSource;
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

            if (editMode != null)
            {
                editMode.StartEditing(songName);
            }
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
        if (audioSource != null && audioSource.isPlaying)
        {
            if (audioSource.time >= audioSource.clip.length - 0.1f)
                {
                beatmapManager.FinishedEditingBeatmap();
                audioSource.Stop();
                }
        }
    }
}
