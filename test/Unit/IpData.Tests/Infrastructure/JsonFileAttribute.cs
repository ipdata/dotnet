using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace IpData.Tests.Infrastructure
{
    public class JsonFileAttribute : DataAttribute
    {
        private readonly string _filePath;

        public JsonFileAttribute(string filePath)
        {
            _filePath = filePath;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            _ = testMethod ?? throw new ArgumentNullException(nameof(testMethod));

            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

            if (File.Exists(path))
            {
                yield return new object[] { File.ReadAllText(path) };
            }
            else
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }
        }
    }
}