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

    Mobile_Main_PlaySelect, 
    Mobile_Main_RoomSelect, 
    
    Mobile_Main_WaitPlayerConnet, 
    PC_Main_WaitPlayerConnet, 
    
    PC_Main_StageSelect, 
    Mobile_Main_WaitStageSelect, 
    Mobile_Main_StageSelect, 

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
    Common, 
}

public enum GamePlayerType
{
    Solo, 
    Multi, 
}

public enum RabbitSoundType
{
    Spawn, 
    Hit, 
    Dead, 
}