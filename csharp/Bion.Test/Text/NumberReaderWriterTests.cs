﻿using Bion.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Bion.Test.Text
{
    [TestClass]
    public class NumberReaderWriterTests
    {
        [TestMethod]
        public void RoundTrip()
        {
            string fileName = "Sample.bin";
            long bytes;

            using (NumberWriter writer = new NumberWriter(File.Create(fileName)))
            {
                for(int i = 1000; i < 1050; ++i)
                {
                    writer.WriteValue((uint)i);
                }

                bytes = writer.BytesWritten;
            }

            using (NumberReader reader = new NumberReader(File.OpenRead(fileName)))
            {
                Assert.IsFalse(reader.EndOfStream);

                for(int i = 1000; i < 1050; ++i)
                {
                    int value = (int)reader.ReadNumber();
                    Assert.AreEqual(i, value);
                }

                Assert.IsTrue(reader.EndOfStream);
                Assert.AreEqual(bytes, reader.BytesRead);
            }
        }
    }
}
