using System;
using System.Collections.Generic;
using System.Data;
using Parquet.Rows;
using Parquet.Schema;

namespace DeltaSharingConnector.Parquet
{
    internal class ParquetTableDataReader : IDataReader
    {
        private DataField[] _dataFields;
        private IEnumerator<Row> _enumerator;
        private Row? _current;
        private bool _hasEnumeratorCurrent;
        private bool _isClosed;

        public ParquetTableDataReader(ParquetSchema schema, IEnumerator<Row?> enumerator)
        {
            _dataFields = schema.GetDataFields();
            _enumerator = enumerator;
        }

        public bool GetBoolean(int i) => _current.GetBoolean(i);
        public byte GetByte(int i) => throw new NotImplementedException();
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => throw new NotImplementedException();
        public char GetChar(int i) => throw new NotImplementedException();
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => throw new NotImplementedException();
        public DateTime GetDateTime(int i) => _current.GetDateTimeOffset(i).DateTime;
        public decimal GetDecimal(int i) => throw new NotImplementedException();
        public double GetDouble(int i) => _current.GetDouble(i);
        public float GetFloat(int i) => _current.GetFloat(i);
        public Guid GetGuid(int i) => throw new NotImplementedException();
        public short GetInt16(int i) => throw new NotImplementedException();
        public int GetInt32(int i) => _current.GetInt(i);
        public long GetInt64(int i) => throw new NotImplementedException();
        public string GetString(int i) => _current.GetString(i);

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i) => GetFieldType(i).Name;
        
        public Type GetFieldType(int i)
        {
            if (i < 0 || i >= _dataFields.Length)
                throw new IndexOutOfRangeException();

            return _dataFields[i].ClrType;
        }
        
        public string GetName(int i)
        {
            if (i < 0 || i >= _dataFields.Length)
                throw new IndexOutOfRangeException();

            return _dataFields[i].Name;
        }

        public int GetOrdinal(string name)
        {
            for (var i = 0; i < _dataFields.Length; i++)
                if (_dataFields[i].Name == name)
                    return i;

            throw new IndexOutOfRangeException(nameof(name));
        }

        public object GetValue(int i)
        {
            if (IsClosed || !_hasEnumeratorCurrent || _current == null)
                throw new InvalidOperationException("Data reader is closed or has reached the end of the enumerator");

            if (i < 0 || i >= _dataFields.Length)
                throw new IndexOutOfRangeException();

            return _current.Get<object>(i);
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i) => throw new NotImplementedException();

        public int FieldCount => _dataFields.Length;
        public object this[int i] => GetValue(i);
        public object this[string name] => GetValue(GetOrdinal(name));

        public void Dispose()
        {
            if (_enumerator != null)
            {
                _enumerator.Dispose();
                _enumerator = null;
                _current = null;
                _hasEnumeratorCurrent = false;
            }
            _isClosed = true;
        }

        public void Close()
        {
            _isClosed = true;
        }

        public DataTable GetSchemaTable()
        {
            var dt = new DataTable();
            foreach (var field in _dataFields)
            {
                dt.Columns.Add(new DataColumn(field.Name, field.ClrType));
            }
            return dt;
        }

        public bool NextResult()
        {
            return false;
        }

        public bool Read()
        {
            if (IsClosed)
                throw new InvalidOperationException("Data reader is closed");

            _hasEnumeratorCurrent = _enumerator.MoveNext();
            _current = _hasEnumeratorCurrent ? _enumerator.Current : null;
            return _hasEnumeratorCurrent;
        }

        public int Depth => 0;
        public bool IsClosed => _isClosed;
        public int RecordsAffected => -1;
    }
}
