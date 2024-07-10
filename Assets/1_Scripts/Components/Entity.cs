using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public string Key;
    //Spublic int Index;

    public Entity(string key)
    {
        this.Key = key;
    }

    public string GetKey()
    {
        return Key;
    }
}
