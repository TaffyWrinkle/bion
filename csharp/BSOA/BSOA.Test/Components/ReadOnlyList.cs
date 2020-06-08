﻿using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace BSOA.Test
{
    public static class ReadOnlyList
    {
        // List of T (for ex: NumberColumn<int>)
        public static void VerifySame<T>(IReadOnlyList<T> expected, IReadOnlyList<T> actual, bool quick = false)
        {
            // Verify Counts match
            Assert.Equal(expected.Count, actual.Count);

            // Verify indexers work and return the same result
            for (int i = 0; i < actual.Count; ++i)
            {
                Assert.Equal(expected[i], actual[i]);
            }

            if (!quick)
            {
                int index;

                // Verify typed enumerator (MoveNext, Current, Reset)
                using (IEnumerator<T> typed = actual.GetEnumerator())
                {
                    index = 0;
                    while (typed.MoveNext())
                    {
                        Assert.Equal(expected[index], typed.Current);
                        index++;
                    }

                    typed.Reset();
                    index = 0;
                    while (typed.MoveNext())
                    {
                        Assert.Equal(expected[index], typed.Current);
                        index++;
                    }
                }

                // Verify untyped enumerator
                IEnumerator untyped = ((IEnumerable)actual).GetEnumerator();
                index = 0;
                while (untyped.MoveNext())
                {
                    Assert.Equal(expected[index], untyped.Current);
                    index++;
                }

                untyped.Reset();
                index = 0;
                while (untyped.MoveNext())
                {
                    Assert.Equal(expected[index], untyped.Current);
                    index++;
                }
            }
        }

        public static void VerifySame<T>(IList<T> expected, IList<T> actual, bool quick = false)
        {
            // Verify Counts match
            Assert.Equal(expected.Count, actual.Count);

            // Verify indexers work and return the same result
            for (int i = 0; i < actual.Count; ++i)
            {
                Assert.Equal(expected[i], actual[i]);
            }

            if (!quick)
            {
                int index;

                // Verify typed enumerator (MoveNext, Current, Reset)
                using (IEnumerator<T> typed = actual.GetEnumerator())
                {
                    index = 0;
                    while (typed.MoveNext())
                    {
                        Assert.Equal(expected[index], typed.Current);
                        index++;
                    }

                    typed.Reset();
                    index = 0;
                    while (typed.MoveNext())
                    {
                        Assert.Equal(expected[index], typed.Current);
                        index++;
                    }
                }

                // Verify untyped enumerator
                IEnumerator untyped = ((IEnumerable)actual).GetEnumerator();
                index = 0;
                while (untyped.MoveNext())
                {
                    Assert.Equal(expected[index], untyped.Current);
                    index++;
                }

                untyped.Reset();
                index = 0;
                while (untyped.MoveNext())
                {
                    Assert.Equal(expected[index], untyped.Current);
                    index++;
                }
            }
        }
    }
}