using UnityEngine;
public struct Vector3Double
{
    public double x;
    public double y;
    public double z;
    public Vector3Double(Vector3 value)
    {
        x = value.x;
        y = value.y;
        z = value.z;
    }

    public float X { get { return (float)x; } }
    public float Y { get { return (float)y; } }
    public float Z { get { return (float)z; } }
    public Vector3 ToVector3() { return new Vector3(X, Y, Z);}

    public static implicit operator Vector3Double(Vector3 value)
    {
        return new Vector3Double(value);
    }

    public override string ToString(){
        return string.Format("{{{0},{1},{2}}}",x,y,z);
    }
}