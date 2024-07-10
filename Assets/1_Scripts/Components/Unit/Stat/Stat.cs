

public delegate void OnReflashStat();
public partial class Unit
{    
    public event OnReflashStat onReflashStat;

    public float hp;
    public float speed;
    public float jump;

    public virtual void SetStat()
    {
        hp = unitData.hp;
        speed = unitData.move;
        jump = 10;
    }
}