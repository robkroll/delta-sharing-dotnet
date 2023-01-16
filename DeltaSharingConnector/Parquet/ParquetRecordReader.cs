using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Parquet;
using Parquet.Rows;

namespace DeltaSharingConnector.Parquet
{
    public class ParquetRecordReader
    {
        private readonly MultipleFileRowEnumerator _rowEnumerator;

        public ParquetRecordReader(IEnumerable<string> paths)
        {
            _rowEnumerator = new MultipleFileRowEnumerator(paths);
        }

        // private async IAsyncEnumerable<Row> ReadAsync()
        // {
        //     Table table = await ParquetReader.ReadTableFromFileAsync(_path);
        //     foreach (var row in table)
        //     {
        //         yield return row;
        //     }
        // }

        public IDataReader AsDataReader()
        {
            return new ParquetTableDataReader(_rowEnumerator.Schema, _rowEnumerator);
        }
    }
}
