using NUnit.Framework;
using System.Reflection;

namespace TestPressProject
{
    public static class MultiplePropertiesAssertion
    {
        public static void AssertAreEqual(object expected, object actual)
        {
            PropertyInfo[] expectedProperties = expected.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            PropertyInfo[] actualProperties = actual.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            int indexCounter = 0;
            foreach(var p in expectedProperties)
            {
                var pValue = p.GetValue(expected);
                var actualValue = actualProperties[indexCounter].GetValue(actual);
                Assert.AreEqual(pValue, actualValue);
                indexCounter = indexCounter + 1;
            }

        }
    }
}
