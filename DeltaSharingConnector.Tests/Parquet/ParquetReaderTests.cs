using System;
using System.Diagnostics;
using DeltaSharingConnector.Parquet;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace DeltaSharingConnector.Tests.Parquet
{
    [TestFixture]
    public class ParquetReaderTests
    {
        [Test]
        public void Test()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Parquet\table_reader_test_data.parquet");
            var reader = new ParquetRecordReader(new [] { path });

            var sb = new StringBuilder();
            using var dataReader = reader.AsDataReader();
            int limit = 10;

            int count = 0;
            while (dataReader.Read())
            {
                if (count++ >= limit) break;

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    sb.Append(Convert.ToString(dataReader.GetValue(i)));
                    if (i < dataReader.FieldCount - 1) sb.Append(", ");
                }

                sb.Append(Environment.NewLine);
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
