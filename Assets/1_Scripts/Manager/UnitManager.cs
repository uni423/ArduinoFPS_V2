using System.Collections;
using System.Collections.Generic;

public class UnitManager
{
    private List<Unit> listToUnitAgent;        

    public UnitManager() { }

    public void Initialize()
    {
        listToUnitAgent = new List<Unit>();
    }

    public void Release()
    {
        listToUnitAgent.Clear();
        listToUnitAgent = null;
    }

    public void OnUpdate(float delta)
    {        
        for (int i = 0; i < listToUnitAgent.Count; ++i)
        {
            if (listToUnitAgent[i] == null)
                continue;
            if (!listToUnitAgent[i].IsUpdate)
                continue;
            listToUnitAgent[i].OnUpdate(delta);
        }
    }

    public void OnLateUpdate(float delta)
    {
        for (int i = 0; i < listToUnitAgent.Count; ++i)
        {
            if (listToUnitAgent[i] == null)
                continue;
            if (!listToUnitAgent[i].IsUpdate)
                continue;
            listToUnitAgent[i].OnLateUpdate(delta);
        }
    }

    public Unit Get(string key)
    {
        for (int i = 0; i < listToUnitAgent.Count; ++i)
        {
            if (listToUnitAgent[i].GetKey() == key)
                return listToUnitAgent[i];
        }

        return null;
    }
    
    public List<Unit> GetList()
    {
        return listToUnitAgent;
    }

    //public List<Unit> GetList(TeamType type)
    //{

    //    List<Unit> tmp = new List<Unit>();

    //    for (int i = 0, ii = listToUnitAgent.Count; ii > i; ++i)
    //    {
    //        if (listToUnitAgent[i].teamType == type)
    //            tmp.Add(listToUnitAgent[i]);
    //    }

    //    return tmp;
    //}

    public int Count()
    {
        if (listToUnitAgent == null)
            return 0;
        return listToUnitAgent.Count;
    }

    public void Regist(Unit agent)
    {
        agent.SetUnitManager(this);

        if (!listToUnitAgent.Contains(agent))
            listToUnitAgent.Add(agent);
    }

    public void Remove(Unit agent)
    {
        agent.SetUnitManager(null);

        if (listToUnitAgent.Contains(agent))
            listToUnitAgent.Remove(agent);
    }

    public void Remove(string key)
    {
        Remove(Get(key));
    }
}