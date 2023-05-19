using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Serializer
{
    public static T Load<T>(string filename) where T : class
    {
        if (File.Exists(filename))
        {
            try
            {
                using (StreamReader stream = new StreamReader(filename))
                {
                    string json = stream.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        return default(T);
    }

    public static void Save<T>(string filename, T data) where T : class
    {
        using (StreamWriter stream = new StreamWriter(filename))
        {
            string json = JsonConvert.SerializeObject(data);
            stream.Write(json);
        }
    }
}

