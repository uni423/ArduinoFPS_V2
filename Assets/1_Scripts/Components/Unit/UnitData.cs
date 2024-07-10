public class UnitData
{
    public int tableID;

    public string design { protected set; get; }
    public Unit_Type type { protected set; get; }
    public string model { protected set; get; }
    public int hp { protected set; get; }
    public int move { protected set; get; }
    public int point { protected set; get; }
    public int param1 { protected set; get; }
    public int param2 { protected set; get; }

    public UnitData(int index)
    {
        this.tableID = index;
        Unit_Table characterTable = TableBase.GetUnit_Table(index.ToString());
        design = characterTable.design_1;
        type = characterTable.type;
        model = characterTable.model;
        hp = characterTable.hp;
        move = characterTable.move;
        point = characterTable.point;
        param1 = characterTable.param1;
        param2 = characterTable.param2;
    }

    public virtual void Init()
    {
    }
}
