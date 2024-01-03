using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SaveableVector3
{
     public float x;
     public float y;
     public float z;

     public SaveableVector3(Vector3 startValue)
     {
          x = startValue.x;
          y = startValue.y;
          z = startValue.z;
     }

     public void SetFromVector(Vector3 vector)
     {
          x = vector.x;
          y = vector.y;
          z = vector.z;
     }

     public Vector3 GetVector3()
     {
          return new Vector3(x, y, z);
     }

     public static implicit operator SaveableVector3(Vector3 origin) => new SaveableVector3(origin);

     public static implicit operator Vector3(SaveableVector3 origin) => origin.GetVector3();
}

