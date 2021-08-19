using UnityEngine;

public struct SecureInt
{
    private int offset;
    private int value;

    public SecureInt(int value)
    {
        offset = Random.Range(-10000, 10000);
        this.value = value ^ offset;
    }

    public override bool Equals(object obj)
    {
        return (int)this == (int)obj;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return ((int)this).ToString();
    }

    public static implicit operator int(SecureInt secureInt)
    {
        return secureInt.value ^ secureInt.offset;
    }

    public static implicit operator SecureInt(int normalInt)
    {
        return new SecureInt(normalInt);
    }
}