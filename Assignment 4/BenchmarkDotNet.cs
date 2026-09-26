using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;




namespace Assignment_4
{

    [MemoryDiagnoser]
   public class ScheduleBenchmark
    {
        [Params( 100000)]
        public int Iterations;

        [Benchmark]
        public string StringConcatenation() 
        { string result = ""; 
            for (int i = 0; i < Iterations; i++) 
            {
                result += "Session - Date - 180 minutes\n"; 
            } 
            return result;
        }

        [Benchmark]
        public string StringBuilderConcatenation()
        { 
            StringBuilder result = new StringBuilder(); 
            for (int i = 0; i < Iterations; i++) 
            {
                result.AppendLine("Session - Date - 180 minutes"); 
            } 
            return result.ToString(); 
        }
    }


}
