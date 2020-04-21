using System;
using System.Collections.Generic;
using System.Text;
using Project_3___Press_Project;
using System.Linq;
using NUnit.Framework;


namespace TestPressProject
{
    static class ListComparer
    {
        public static void AssertAreEqual<T>(List<T> expected, List<T> actual)
        {
            int indexCounter = 0;
            foreach(var item in expected)
            {
                Assert.AreEqual(item, actual[indexCounter]);
                indexCounter = indexCounter + 1;
            }
        }
    }
}
