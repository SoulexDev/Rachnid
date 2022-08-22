using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager
{
    public static string fileExtension = ".json";
    public static string savePath => Application.persistentDataPath + "/saves/";
    public static bool Save(string saveName, object saveData)
    {
        //BinaryFormatter formatter = GetBinaryFormatter();

        if(!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        string path = savePath + saveName + fileExtension;

        string json = JsonUtility.ToJson(saveData);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);

        //FileStream file = File.Create(path);
        //formatter.Serialize(file, saveData);
        //file.Close();
        return true;
    }
    public static object Load(string savename)
    {
        string path = savePath + savename + fileExtension;
        if (!File.Exists(path))
        {
            return null;
        }
        //BinaryFormatter formatter = GetBinaryFormatter();
        //FileStream file = File.Open(path, FileMode.Open);
        try
        {
            //object save = formatter.Deserialize(file);
            //file.Close();
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            object save = JsonUtility.FromJson<SaveData>(json);
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", path);
            //file.Close();
            return null;
        }
    }
    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector surrogateSelector = new SurrogateSelector();

        Vector3SerializationSurrogate vector3SS = new Vector3SerializationSurrogate();
        QuaternionSerializationSurrogate quaternionSS = new QuaternionSerializationSurrogate();

        surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3SS);
        surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSS);

        formatter.SurrogateSelector = surrogateSelector;
        return formatter;
    }
}