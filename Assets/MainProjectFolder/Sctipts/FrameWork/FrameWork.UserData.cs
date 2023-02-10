using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class FrameWork : MonoBehaviour
{
    public class UserData
    {
        const string EXTENSION = ".json";
        const string SAVE_FILE_NAME = "usersave";
        const string CONFIGS_FILE_NAME = "userconfig";

        static UTF8Encoding UTF8 = new UTF8Encoding(false);

        static string PATH_SAVE =>Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME + EXTENSION);
        static string PATH_CONFIG => Path.Combine(Application.persistentDataPath, CONFIGS_FILE_NAME + EXTENSION);

        public UserSave save = null;
        public UserConfigs configs = null;

        private UserData() { }

        static public UserData CreateInstanceFromFile()
        {
            var inst = new UserData();

            if(File.Exists(PATH_SAVE) == false)
            {
                
            }
            if(File.Exists(PATH_CONFIG) == false)
            {

            }
            inst.save = JsonConvert.DeserializeObject<UserSave>(File.ReadAllText(PATH_SAVE, UTF8));
            inst.configs = JsonConvert.DeserializeObject<UserConfigs>(File.ReadAllText(PATH_CONFIG, UTF8)); 

            return inst;
        }

        public bool SaveUserSave()
        {
            try
            {
                if(File.Exists(PATH_SAVE) == true)
                {
                    string oldSavePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME + "old" + EXTENSION);
                    if(File.Exists(oldSavePath) == true)
                    {
                        File.Delete(oldSavePath);
                    }

                    File.Move(PATH_SAVE, oldSavePath);


                }
                string jsonSave = JsonConvert.SerializeObject(save, Formatting.Indented);   

                File.WriteAllText(PATH_SAVE, jsonSave, UTF8);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool SaveUserConfigs()
        {
            try
            {
                if (File.Exists(PATH_CONFIG) == true)
                {
                    string oldConfigsPath = Path.Combine(Application.persistentDataPath, PATH_CONFIG + "old" + EXTENSION);
                    if (File.Exists(oldConfigsPath) == true)
                    {
                        File.Delete(oldConfigsPath);
                    }

                    File.Move(PATH_CONFIG, oldConfigsPath);


                }
                string jsonSave = JsonConvert.SerializeObject(save, Formatting.Indented);

                File.WriteAllText(PATH_CONFIG, jsonSave, UTF8);
            }
            catch
            {
                return false;
            }
            return true;
        }
        void CreateEmptySave()
        {
            Debug.Assert(File.Exists(PATH_SAVE) == false, "이미 세이브 파일이 존재한다. 왜 호출되었지? 로직에러.");

            UserSave emptySave = UserSave.EMPTY;

            string jsonSave = JsonConvert.SerializeObject(emptySave, Formatting.Indented);
            File.WriteAllText(PATH_SAVE, jsonSave, UTF8);
        }
        void CreateDefaultConfigs()
        {
            Debug.Assert(File.Exists(PATH_CONFIG) == false, "이미 유저 설정 파일이 존재한다. 왜 호출되었지? 로직에러.");

            UserConfigs defaultConfigs = UserConfigs.DEFAULT;

            string jsonConfigs = JsonConvert.SerializeObject(defaultConfigs, Formatting.Indented);
            File.WriteAllText(PATH_CONFIG, jsonConfigs, UTF8);
        }
    }
    [System.NonSerialized] public UserData userData = null;
}