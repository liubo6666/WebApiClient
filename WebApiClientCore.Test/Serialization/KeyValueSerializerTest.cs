﻿using System;
using System.Linq;
using WebApiClientCore.Serialization;
using Xunit;


namespace WebApiClientCore.Test.Serialization
{
    public class KeyValueSerializerTest
    {
        [Fact]
        public void SerializeTest()
        {
            var obj1 = new FormatModel { Age = 18, Name = "lao九" };
            var formatter = new KeyValueSerializer();
            var options = new HttpApiOptions().KeyValueSerializeOptions;

            var kvs = formatter.Serialize("pName", obj1, options)
                .ToDictionary(item => item.Key, item => item.Value, StringComparer.OrdinalIgnoreCase);

            Assert.True(kvs.Count == 2);
            Assert.True(kvs["Name"] == "lao九");
            Assert.True(kvs["Age"] == "18");


            kvs = formatter.Serialize("pName", 30, null)
               .ToDictionary(item => item.Key, item => item.Value);

            Assert.True(kvs.Count == 1);
            Assert.True(kvs["pName"] == "30");

            var bools = formatter.Serialize("bool", true, null);
            Assert.Equal("true", bools[0].Value);

            var strings = formatter.Serialize("strings", "string", null);
            Assert.Equal("string", strings[0].Value);


            var dic = new System.Collections.Concurrent.ConcurrentDictionary<string, object>();
            dic.TryAdd("Key", "Value");

            var kvs2 = formatter.Serialize("dic", dic, options);
            Assert.True(kvs2.First().Key == "key");


            Assert.True(formatter.Serialize("null", null, null).Any());
        }
    }
}
