using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTransform {
    [SerializeField]
    SerializableVector3 _position;
    [SerializeField]
    SerializableQuaternion _rotation;
    [SerializeField]
    SerializableVector3 _scale;
    [SerializeField]
    string _id;

    public Vector3 Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public Quaternion Rotation
    {
        get { return _rotation; }
        set { _rotation = value; }
    }

    public Vector3 Scale
    {
        get { return _scale; }
        set { _scale = value; }
    }

    public string ID
    {
        get { return _id; }
        set { _id = value; }
    }

}
