﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            new Calculator().Start();
        }
    }

    public class Calculator
    {
        public void Start()
        {
            Console.WriteLine("Simple Calculator");
            Console.WriteLine("1 + 1 = 2");
            Console.WriteLine("1 + 2 = 3");
            Console.WriteLine("1 + 3 = 4");
        }
    }
}
