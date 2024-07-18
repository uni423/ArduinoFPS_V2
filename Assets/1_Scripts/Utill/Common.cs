public enum Unit_Type
{
    Rabbit_Normal,
    Rabbit_Baby,
    Rabbit_Strong,
    Rabbit_Evolve,
    Rabbit_BulkUp,
}

public enum GameStep
{
    Loading, 
    Main, 
    PC_Main_WaitPlayerConnet, 
    PC_Main_StageSelect, 
    Mobile_Main_PlaySelect, 
    Mobile_Main_StageSelect, 
    Mobile_Main_RoomSelect, 
    Mobile_Main_WaitPlayerConnet, 
    Playing, 
    End, 
    Continue, 
    Pause,
}

public enum AttackType
{
    Normal, 

}

public enum PlatformType
{
    PC, 
    Mobile, 
}

public enum GamePlayerType
{
    Solo, 
    Multi, 
}