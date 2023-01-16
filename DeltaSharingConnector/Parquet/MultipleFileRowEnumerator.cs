using System;
using System.Collections;
using System.Collections.Generic;
using Parquet;
using Parquet.Rows;
using Parquet.Schema;

namespace DeltaSharingConnector.Parquet
{
    internal class MultipleFileRowEnumerator : IEnumerator<Row?>
    {
        private IEnumerator<Row>? _currentEnumerator;
        private Row? _current;
        private readonly Queue<string> _paths = new Queue<string>();
        private readonly ParquetSchema _schema;

        public ParquetSchema Schema => _schema;

        public MultipleFileRowEnumerator(IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                _paths.Enqueue(path);
            }

            if (!ReadNextFile(out _schema))
            {
                throw new ArgumentException("No schema could be loaded for given paths.");
            }
        }

        private bool ReadNextFile(out ParquetSchema schema)
        {
            if (_paths.TryDequeue(out string path))
            {
                var table = ParquetReader.ReadTableFromFileAsync(path).Result;
                schema = table.Schema;
                _currentEnumerator = table.GetEnumerator();
                return true;
            }

            schema = new ParquetSchema();
            return false;
        }

        #region Implementation of IEnumerator

        public bool MoveNext()
        {
            if (_currentEnumerator == null) return false;

            var hasNext = _currentEnumerator.MoveNext();
            _current = _currentEnumerator.Current;
            if (hasNext) return hasNext;

            return ReadNextFile(out _) && MoveNext();
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        public Row? Current => _current;

        object? IEnumerator.Current => Current;

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            _currentEnumerator?.Dispose();
            _current = null;
        }

        #endregion
    }
}
