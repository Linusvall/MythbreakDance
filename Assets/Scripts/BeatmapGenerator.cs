using System.IO;
using UnityEngine;

public class BeatmapGenerator : MonoBehaviour
{
    private Beatmap beatmap;
    private string currentBeatmapName;



    public void AddToBeatMap(float time, string direction)
    {
        beatmap.AddEvent(time, direction);
    }

    public void SetCurrentBeatMapName(string beatmapName)
    {
        currentBeatmapName = beatmapName;
        beatmap = new Beatmap();
    }

    public void FinishedEditingBeatmap()
    {
        if (beatmap != null)
        {
            AddToJsonFile(currentBeatmapName);
        }
    }


    
    public void AddToJsonFile(string fileName)
    {
        string filePath = Application.dataPath + "/" + fileName + ".json";

        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string json = File.Exists(filePath) ? File.ReadAllText(filePath) : "{\"beatEvents\":[]}";

        Beatmap existingBeatmap = JsonUtility.FromJson<Beatmap>(json);

        if (existingBeatmap == null)
        {
            existingBeatmap = new Beatmap();
        }

        existingBeatmap.beatEvents.AddRange(beatmap.beatEvents);

        string updatedJson = JsonUtility.ToJson(existingBeatmap, true);

        File.WriteAllText(filePath, updatedJson);
    }

    public Beatmap LoadJsonFile(string fileName)
    {
        string filePath = Application.dataPath + "/" + fileName + ".json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            Beatmap beatmap = JsonUtility.FromJson<Beatmap>(json);

            return beatmap;
        }
        else
        {
            return new Beatmap();
        }
    }


}
