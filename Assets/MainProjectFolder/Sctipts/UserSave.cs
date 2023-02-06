public class UserSave
{
    static public UserSave EMPTY => new UserSave
    {
        dummy = "dummy_empty_save"
    };

    public string dummy;
}

public class UserConfigs
{
    static public UserConfigs DEFAULT => new UserConfigs
    {
        bgmVolume = 0.5f
    };

    public float bgmVolume;
}