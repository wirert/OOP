﻿using System;

namespace CustomRandomList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            RandomList list = new RandomList { "1", "2", "3", "4", "5", "6" };

            Console.WriteLine(list.RandomString());
        }
    }
}
