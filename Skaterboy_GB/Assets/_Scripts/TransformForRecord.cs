using UnityEngine;

public class TransformForRecord
{
    public Vector2 positionObject;
    public Quaternion rotationObject;

    public TransformForRecord(Vector2 pos, Quaternion rot)
    {
        positionObject = pos;
        rotationObject = rot;
    }
}
