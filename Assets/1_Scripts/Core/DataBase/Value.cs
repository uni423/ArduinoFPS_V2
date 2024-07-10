using System;

////==========================================================================================================================
////
////==========================================================================================================================
//public class ValueData : GameEntityBase
//{
//    public Value value;

//    public ValueData(int Key, Value value)
//            : base(Key)
//    {
//        this.value = value;
//    }
//}

//==========================================================================================================================
//
//==========================================================================================================================
public abstract class Value// : IEntity
{
	//public Value GetKey()
	//{
	//    return this;
	//}

	public abstract new Type GetType();

	public static implicit operator int(Value v)
	{
		if (!(v is Value<int>)) return 0;
		return ((Value<int>)v).value;
	}
	
	public static implicit operator float(Value v)
	{
		if (!(v is Value<float>)) return 0f;
		return ((Value<float>)v).value;
	}

	public static implicit operator bool(Value v)
	{		
		return ((Value<bool>)v).value;
	}
	
	public static implicit operator string(Value v)
	{
		return v.ToString();
	}

	public static implicit operator long(Value v)
	{
		if (!(v is Value<long>)) return 0L;
		return ((Value<long>)v).value;
	}

	public static implicit operator byte(Value v)
	{
		if (!(v is Value<byte>)) return 0;
		return ((Value<byte>)v).value;
	}

	public static implicit operator int[](Value v)
	{
		if (!(v is Value<int[]>)) return null;
		return ((Value<int[]>)v).value;
	}

	public static implicit operator Value(int v)
	{
		return new Value<int>(v);
	}
	
	public static implicit operator Value(float v)
	{
		return new Value<float>(v);
	}

	public static implicit operator Value(bool v)
	{
		return new Value<bool>(v);
	}
	
	public static implicit operator Value(string v)
	{
		return new Value<string>(v);
	}

	public static implicit operator Value(long v)
	{
		return new Value<long>(v);
	}

	public static implicit operator Value(byte v)
	{
		return new Value<byte>(v);
	}

	public static implicit operator Value(int[] v)
	{
		return new Value<int[]>(v);
	}

	public static int TryParseInt32(string text)
	{
		int value;
		int.TryParse(text, out value);
		return value;
	}
	
	public static float TryParseFloat(string text)
	{
		float value;
		float.TryParse(text, out value);
		return value;
	}
}
//==========================================================================================================================

public class Value<T> : Value
{
	public T value;

	public Value()
	{
		this.value = default(T);
	}
	
	public Value(T value)
	{
		this.value = value;
	}
	
	public override string ToString()
	{
		return this.value.ToString();
	}

	public override Type GetType()
	{
		return this.value.GetType();
	}
}
//==========================================================================================================================
