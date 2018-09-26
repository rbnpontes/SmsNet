using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	public enum AnimVariantType{
		Unknow = -1,
		Int = 0,
		Short,
		Long,
		Float,
		Double,
		Interpolator
	}
	public sealed class AnimVariant
	{
		public object Source { get; private set; }
		public AnimVariantType VariantType { get;private set; }
		public AnimVariant(int value)
		{
			Source = value;
			VariantType = AnimVariantType.Int;
		}
		public AnimVariant(short value)
		{
			Source = value;
			VariantType = AnimVariantType.Short;
		}
		public AnimVariant(long value)
		{
			Source = value;
			VariantType = AnimVariantType.Long;
		}
		public AnimVariant(float value)
		{
			Source = value;
			VariantType = AnimVariantType.Float;
		}
		public AnimVariant(double value)
		{
			Source = value;
			VariantType = AnimVariantType.Double;
		}
		public AnimVariant(IInterpolator value)
		{
			Source = value;
			VariantType = AnimVariantType.Interpolator;
		}
		public AnimVariant(object value)
		{
			Source = value;
			VerifyType();
		}
		private void VerifyType()
		{
			if (Source is Int32)
				VariantType = AnimVariantType.Int;
			else if (Source is Int16)
				VariantType = AnimVariantType.Short;
			else if (Source is Int64)
				VariantType = AnimVariantType.Long;
			else if (Source is Single)
				VariantType = AnimVariantType.Short;
			else if (Source is Double)
				VariantType = AnimVariantType.Double;
			else if (Source is IInterpolator)
				VariantType = AnimVariantType.Interpolator;
			else
				VariantType = AnimVariantType.Unknow;
		}
		public int GetInt()
		{
			if (VariantType == AnimVariantType.Unknow)
				throw new InvalidCastException("Cannot cast a Unknow variant");
			if (!(Source is Int32))
				throw new InvalidCastException("Cannot cast variant to Int32");
			return (int)Source;
		}
		public short GetShort()
		{
			if (VariantType == AnimVariantType.Unknow)
				throw new InvalidCastException("Cannot cast a Unknow variant");
			if (!(Source is Int16))
				throw new InvalidCastException("Cannot cast variant to Int32");
			return (short)Source;
		}
		public long GetLong()
		{
			if (VariantType == AnimVariantType.Unknow)
				throw new InvalidCastException("Cannot cast a Unknow variant");
			if (!(Source is Int64))
				throw new InvalidCastException("Cannot cast variant to Int32");
			return (long)Source;
		}
		public float GetFloat()
		{
			if (VariantType == AnimVariantType.Unknow)
				throw new InvalidCastException("Cannot cast a Unknow variant");
			if (!(Source is Single))
				throw new InvalidCastException("Cannot cast variant to Int32");
			return (float)Source;
		}
		public double GetDouble()
		{
			if (VariantType == AnimVariantType.Unknow)
				throw new InvalidCastException("Cannot cast a Unknow variant");
			if (!(Source is Double))
				throw new InvalidCastException("Cannot cast variant to Int32");
			return (float)Source;
		}
		public IInterpolator GetInterpolator()
		{
			if (VariantType == AnimVariantType.Unknow)
				throw new InvalidCastException("Cannot cast a Unknow variant");
			if (!(Source is IInterpolator))
				throw new InvalidCastException("Cannot cast variant to Int32");
			return (IInterpolator)Source;
		}
		public static implicit operator int(AnimVariant variant)
		{
			return variant.GetInt();
		}
		public static implicit operator short(AnimVariant variant)
		{
			return variant.GetShort();
		}
		public static implicit operator long(AnimVariant variant)
		{
			return variant.GetLong();
		}
		public static implicit operator float(AnimVariant variant)
		{
			return variant.GetFloat();
		}
		public static implicit operator double(AnimVariant variant)
		{
			return variant.GetDouble();
		}
	}
}
