using System;
using System.Collections.Generic;

[Serializable]
public class SerializedGameObjectList
{
    public List<SerializedGameObject> list ;

    public SerializedGameObjectList()
    {
        list = new List<SerializedGameObject>();
    }
}