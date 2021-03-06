// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;

using Bion.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bion.Test.Text
{
    [TestClass]
    public class NumberConverterTests
    {
        [TestMethod]
        public void RoundTrip_SevenBitTerminated()
        {
            BufferedRoundTrip("SevenBit.bin",
                (writer) =>
                {
                    for (int i = 0; i < 100000; ++i)
                    {
                        NumberConverter.WriteSevenBitTerminated(writer, (uint)i);
                    }
                },
                (reader) =>
                {
                    int expected = 0;
                    while (!reader.EndOfStream)
                    {
                        int value = (int)NumberConverter.ReadSevenBitTerminated(reader);
                        Assert.AreEqual(expected, value);
                        expected++;
                    }

                    Assert.AreEqual(100000, expected);
                }
            );
        }

        [TestMethod]
        public void RoundTrip_SixBitTerminated()
        {
            BufferedRoundTrip("SixBit.bin",
                (writer) =>
                {
                    for (int i = 0; i < 100000; ++i)
                    {
                        NumberConverter.WriteSixBitTerminated(writer, (uint)i);
                    }
                },
                (reader) =>
                {
                    int expected = 0;
                    while (!reader.EndOfStream)
                    {
                        int value = (int)NumberConverter.ReadSixBitTerminated(reader);
                        Assert.AreEqual(expected, value);
                        expected++;
                    }

                    Assert.AreEqual(100000, expected);
                }
            );
        }

        [TestMethod]
        public void RoundTrip_SevenBitFixed()
        {
            int fixedLength = 5;
            BufferedRoundTrip("SevenBitFixed.bin",
                (writer) =>
                {
                    for (int i = 0; i < 100000; ++i)
                    {
                        NumberConverter.WriteSevenBitFixed(writer, (uint)i, fixedLength);
                    }
                },
                (reader) =>
                {
                    int expected = 0;
                    while (!reader.EndOfStream)
                    {
                        int value = (int)NumberConverter.ReadSevenBitFixed(reader, fixedLength);
                        Assert.AreEqual(expected, value);
                        expected++;
                    }

                    Assert.AreEqual(100000, expected);
                }
            );
        }

        [TestMethod]
        public void RoundTrip_IntBlock()
        {
            int[] block = new int[128];
            int blockIndex;

            BufferedRoundTrip("IntBlock.bin",
                (writer) =>
                {
                    blockIndex = 0;

                    for (int i = 0; i < 100000; ++i)
                    {
                        block[blockIndex++] = i;
                        if (blockIndex == 128)
                        {
                            NumberConverter.WriteIntBlock(writer, block, blockIndex);
                            blockIndex = 0;
                        }
                    }

                    NumberConverter.WriteIntBlock(writer, block, blockIndex);
                },
                (reader) =>
                {
                    int expected = 0;
                    while (!reader.EndOfStream)
                    {
                        int count = NumberConverter.ReadIntBlock(reader, block, block.Length);
                        for(int i = 0; i < count; ++i)
                        {
                            Assert.AreEqual(expected, block[i]);
                            expected++;
                        }
                    }

                    Assert.AreEqual(100000, expected);
                }
            );
        }

        [TestMethod]
        public void RoundTrip_Int()
        {
            BufferedRoundTrip("Int.bin",
                (writer) =>
                {
                    for (int i = 0; i < 10000; ++i)
                    {
                        NumberConverter.Write(writer, i);
                    }
                },
                (reader) =>
                {
                    int expected = 0;
                    while (!reader.EndOfStream)
                    {
                        int value = NumberConverter.ReadInt32(reader);
                        Assert.AreEqual(expected, value);
                        expected++;
                    }

                    Assert.AreEqual(10000, expected);
                }
            );
        }

        private static void BufferedRoundTrip(string fileName, Action<BufferedWriter> write, Action<BufferedReader> read)
        {
            long bytes;

            using (BufferedWriter writer = new BufferedWriter(File.Create(fileName)))
            {
                Assert.AreEqual(0, writer.BytesWritten);
                write(writer);
                bytes = writer.BytesWritten;
            }

            Assert.AreEqual(new FileInfo(fileName).Length, bytes);

            using (BufferedReader reader = new BufferedReader(File.OpenRead(fileName)))
            {
                Assert.IsFalse(reader.EndOfStream);
                read(reader);
                Assert.IsTrue(reader.EndOfStream);
                Assert.AreEqual(bytes, reader.BytesRead);
            }
        }
    }
}
