using System.Text;
using BenchmarkDotNet.Attributes;

namespace string_x_StringBuilder
{
    [MemoryDiagnoser(false)]
    public class Methods
    {
        [Benchmark]
        public string PrintString()
        {
            string str = "Hello";

            for (int i = 0; i < 100; i++)
            {
                str += ", World!";
            }

            return str;
        }

        [Benchmark]
        public StringBuilder PrintStringBuilder()
        {
            StringBuilder sb = new("Hello");

            for (int i = 0; i < 100; i++)
            {
                sb.Append(", World!");
            }

            return sb;
        }
    }
}

//| Method             |       Mean |     Error |    StdDev | Allocated |
//| ------------------ | ---------: | --------: | --------: | --------: |
//| PrintString        | 9,180.8 ns | 156.42 ns | 146.32 ns |  82.03 KB |
//| PrintStringBuilder |   911.2 ns |  16.32 ns |  15.26 ns |   2.49 KB |
