using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayGame : MonoBehaviour, IPointerClickHandler
{
    public class VectorJObject
    {
        [JsonInclude]
        public float? x = 0;

        [JsonInclude]
        public float? y = 0;

        [JsonInclude]
        public float? z = 0;
    }

    public static int CurrentLevel = 1;

    public static int? MaxLevel = 1;

    public static Vector3? LastCheckpoint;

    public static readonly string savePath = "save.json";

    public void Start()
    {
        if(LastCheckpoint == null || MaxLevel == null)
        {
            if (File.Exists(savePath))
            {
                var save = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(savePath));
                MaxLevel = int.Parse(save["MaxLevel"]);
                var check = JsonSerializer.Deserialize<VectorJObject?>(save["LastCheckpoint"]);
                LastCheckpoint = new Vector3(check?.x ?? 0, check?.y ?? 0, check?.z ?? 0);
                CurrentLevel = int.Parse(save["CurrentLevel"]);
            }
            else
            {
                MaxLevel = 1;
                LastCheckpoint = null;
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerControls.LastCheckpoint = LastCheckpoint;
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)MaxLevel);

    }
    public void OnApplicationQuit()
    {
        Save();
    }
    public static void Save()
    {
        var save = new Dictionary<string, string>
        {
            {"MaxLevel", MaxLevel.ToString()},
            {"LastCheckpoint", JsonSerializer.Serialize(LastCheckpoint != null ? new VectorJObject() {x = LastCheckpoint?.x, y = LastCheckpoint?.y, z = LastCheckpoint?.z} : null)},
            {"MusicVolume", AudioManager.Instance.MusicVolume.ToString() },
            {"SoundVolume", AudioManager.Instance.SoundVolume.ToString() },
            {"CurrentLevel", CurrentLevel.ToString() }
        };
        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        };

        File.WriteAllText(savePath, JsonSerializer.Serialize(save, options));
        Debug.Log(save);
    }
    public static Dictionary<string, string> Load()
    {
        return JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(savePath));
    }
}
