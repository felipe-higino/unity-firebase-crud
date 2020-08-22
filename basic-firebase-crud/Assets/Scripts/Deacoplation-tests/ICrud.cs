using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrud
{
	//void CREATE(DataType obj, Action<DataType> callback);
	//void UPDATE(DataType obj, DataType newObj, Action<DataType> callback);
	//void READ(DataType obj, Action<DataType> callback);
	//void DELETE(DataType obj, Action<DataType> callback);
	
	void CREATE();
	void UPDATE();
	void READ();
	void DELETE();
}
