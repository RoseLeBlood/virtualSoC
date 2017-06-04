using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcsos.Komponent
{
    enum SoftFloatFormat
    {
        Half = 16,
        Single = 32,
        Double = 64,
        Quad = 128,
    }
    public class SoftFloat
    {
        private int m_MantisdeSize;
        private int m_ExponentSize;
        private int m_Bias;

        public int Bits { get { return m_MantisdeSize + m_ExponentSize; } }

        public SoftFloat(int mantisse, int expo, int bias)
        {
            m_MantisdeSize = mantisse;
            m_ExponentSize = expo;
            m_Bias = bias;
        }
        public SoftFloat(byte[] value)
        {
            FromBytes(value);
        }
        public virtual byte[] ToBytes()
        {
            return null;
        }
        public  virtual void FromBytes(byte[] value)
        {

        }
    }


    public class HalfFloat : SoftFloat
    {
        public HalfFloat() : base(11, 5, 15) { }
    }
    public class SingleFloat : SoftFloat
    {
        public SingleFloat() : base(24,8,127) { }
        public SingleFloat(byte[] v) : base(v) { }

        public override byte[] ToBytes()
        {
            return null;
        }
        public override void FromBytes(byte[] value)
        {

        }
    }
    public class DoubleFloat : SoftFloat
    {
        public DoubleFloat() : base(53, 11, 1023) { }
    }
    public class QuadFloat : SoftFloat
    {
        public QuadFloat() : base(113, 15, 16383) { }
    }
}
