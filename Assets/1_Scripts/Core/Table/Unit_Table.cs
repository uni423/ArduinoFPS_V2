using System;

public class Unit_Table : TableBase
{
    //Enemy_ID	#design	Enemy_Type	Enemy_Model	Enemy_Base_Hp	Enemy_Move	Enemy_Point	Enemy_Param1	Enemy_Param2

    public enum Columns
    {
        Unit_ID,
        design_1,
        Unit_Type,
        Unit_Model,
        Unit_Hp,
        Unit_Move,
        Unit_Point,
        Unit_Param1,
        Unit_Param2,
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void SetKey()
    {
        key = id.ToString();
    }

    public virtual int id { get { return (int)this[(int)Columns.Unit_ID]; } }
    public virtual string design_1 { get { return this[(int)Columns.design_1]; } }
    public virtual Unit_Type type { get { return (Unit_Type)Enum.Parse(typeof(Unit_Type), this[(int)Columns.Unit_Type]); } }
    public virtual string model { get { return this[(int)Columns.Unit_Model]; } }
    public virtual int hp { get { return (int)this[(int)Columns.Unit_Hp]; } }
    public virtual int move { get { return (int)this[(int)Columns.Unit_Move]; } }
    public virtual int point { get { return (int)this[(int)Columns.Unit_Point]; } }
    public virtual int param1 { get { return (int)this[(int)Columns.Unit_Param1]; } }
    public virtual int param2 { get { return (int)this[(int)Columns.Unit_Param2]; } }
}
