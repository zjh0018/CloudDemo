using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData activeSave;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        Load();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        activeSave.isJumpInSky = PlayerController.instance.isJumpInSky;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);

        stream.Close();
        Debug.Log("Saved");
    }
    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;

            stream.Close();
            Debug.Log("Loaded");
        }
    }
}
[System.Serializable]
public class SaveData
{
    public string saveName = "PlayerData";
    public bool isJumpInSky;
}
