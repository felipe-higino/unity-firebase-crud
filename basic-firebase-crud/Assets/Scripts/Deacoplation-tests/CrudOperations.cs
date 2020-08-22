using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrudOperations : 
    Singleton<CrudOperations>,
    ICrud
{
    [SerializeField] Component DatabaseFunctions;

    void Awake() => AwakeSingleton(this);

    public void CREATE()
    {
        (DatabaseFunctions as ICrud)?.CREATE();
    }

    public void UPDATE()
    {
        throw new NotImplementedException();
    }

    public void READ()
    {
        throw new NotImplementedException();
    }

    public void DELETE()
    {
        throw new NotImplementedException();
    }



    //public void CREATE(DataType obj, Action<DataType> callback)
    //{
    //    DatabaseFunctions.CREATE(obj, callback);
    //}

    //public void UPDATE(DataType obj, DataType newObj, Action<DataType> callback)
    //{
    //    DatabaseFunctions.UPDATE(obj, newObj, callback);
    //}

    //public void READ(DataType obj, Action<DataType> callback)
    //{
    //    DatabaseFunctions.READ(obj, callback);
    //}

    //public void DELETE(DataType obj, Action<DataType> callback)
    //{
    //    DatabaseFunctions.DELETE(obj, callback);
    //}
}
