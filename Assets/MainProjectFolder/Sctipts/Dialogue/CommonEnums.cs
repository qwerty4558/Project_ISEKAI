public enum EWorkTable : int
{
    None = 0,
    Carpentry = 1,
    Smithy = 2,
    Alchemy = 3,
}

public enum EShopDialogState : int
{
    ErrorNotSet = 0,
    FirstEnter = 1,
    FirstReject1 = 2,
    FirstReject2 = 3,
    FirstReject3 = 4,
    SecondEnter = 5,
    SecondReject1 = 6,
    SecondReject2 = 7,
    SecondReject3 = 8,
    Accept = 9,
}

//--- UI ---
[System.Flags]
public enum EUIMangerFlags : int
{
    None = 0,

    HUD = 1 << 0,
    Dialog = 1 << 1,

    ShopCraftPopup = 1 << 10,

    All = -1
}
[System.Flags]
public enum EUIHudFlags : int
{
    None = 0,

    Clock = 1 << 0,
    DDay = 1 << 1,

    All = -1
}
[System.Flags]
public enum EUIDialogFlags : int
{
    None = 0,

    DialogBox = 1 << 0,
    CharacterLeft = 1 << 1,
    CharacterRight = 1 << 2,

    YesButton = 1 << 7,
    NoButton = 1 << 8,
    ByeButton = 1 << 9,

    All = -1
}
[System.Flags]
public enum EUIShopCraftPopupFlags : int
{
    None = 0,

    Inventory = 1 << 0,
    CraftPanel = 1 << 1,
    ResultPanel = 1 << 2,

    All = -1
}