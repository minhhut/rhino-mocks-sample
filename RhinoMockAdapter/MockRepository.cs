using System;
using System.Collections.Generic;
using Moq;

namespace RhinoMockAdapter
{
    public static class MockRepository
    {
        private static readonly IDictionary<int, Mock> mockCollections = new Dictionary<int, Mock>();

        public static Mock<T> GetMockOf<T>(T obj) where T : class
        {
            if (!mockCollections.ContainsKey(obj.GetHashCode()))
            {
                throw new ArgumentOutOfRangeException("Could not find object from mock collection");
            }
            
            if (!mockCollections.TryGetValue(obj.GetHashCode(), out var mock))
            {
                throw new Exception("Not found");
            }
            
            var castObj = (Mock<T>)mock;
            return castObj;
        }

        public static T GenerateStub<T>() where T : class
        {
            return GenerateMock<T>();
        }

        public static T GenerateMock<T>() where T : class
        {
            var mock = new Mock<T>();            
            mockCollections.Add(mock.Object.GetHashCode(), mock);
            return mock.Object;
        }
    }
}