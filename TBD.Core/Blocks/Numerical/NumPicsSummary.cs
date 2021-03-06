﻿using System;
using System.Collections.Generic;
using System.Data;
using TBD.Logging;
using TBD.Model;

namespace TBD.Core
{
    public class CNumSeriesPropertySummary : IDataSource
    {
        private const String SeriesNameHeaderName = "Series";

        private readonly DataTable _mainTable;

        private readonly Dictionary<CSeriesInfo, List<NumProperty>> _summary = new Dictionary<CSeriesInfo, List<NumProperty>>();

        private DataTable _dataTableCache;

        public CNumSeriesPropertySummary( params DataTableHeader[] headers )
        {
            _mainTable = new DataTable();
            InitalizeHeaders( headers );
        }

        private void InitalizeHeaders( IEnumerable<DataTableHeader> headers )
        {
            DataColumn seriesName = new DataColumn
            {
                DataType = typeof(String),
                ColumnName =SeriesNameHeaderName,
                ReadOnly = true
            };

            _mainTable.Columns.Add( seriesName );

            foreach (DataTableHeader dataTableHeader in headers)
            {
                
                DataColumn column = new DataColumn
                {
                    DataType = dataTableHeader.Type,
                    ColumnName = dataTableHeader.Name,
                    ReadOnly = true
                };

                _mainTable.Columns.Add( column );
            }
        }

        public T GetAs<T>() where T : class
        {
            T result = TryGetAs<T>();

            if (result == null)
                throw new Exception( $"Failed to get numeric summary as {typeof(T)}" );

            return result;
        }

        public T TryGetAs<T>() where T : class
        {
            if (typeof(T) == typeof(DataTable))
                return (T) (Object) GetSummaryAsDataTable();

            if (typeof(T) == typeof(CNumSeriesPropertySummary))
                return (T) (Object) ( this );

            return null;
        }


        public DataTable GetSummaryAsDataTable()
        {
            if (_dataTableCache == null)
                GenerateSummary();

            return _dataTableCache;
        }

        private void GenerateSummary()
        {
            Log.Message( "Generating table" );

            foreach (var summary in _summary)
            {
                DataRow dataRow = _mainTable.NewRow();

                dataRow[ SeriesNameHeaderName ] = summary.Key.HumanReadableName;

                //TODO: (avlomakin) handle if no column for prop 
                foreach (NumProperty property in summary.Value)
                    dataRow[ property.Name ] = property.Value;

                _mainTable.Rows.Add( dataRow );
            }

            //TODO: (avlomakin) redo to lazy generation
            _dataTableCache = _mainTable;
        }


        public void AddPropertyForSeries( CSeriesInfo info, NumProperty value )
        {
            List<NumProperty> props = _summary.GetOrCreate( info, () => new List<NumProperty>() );
            props.Add( value );
        }
    }

    public struct DataTableHeader
    {
        public DataTableHeader( Type type, String name )
        {
            Type = type;
            Name = name;
        }

        public Type Type { get;  }
        public String Name { get;  }
    }

    public struct NumProperty
    {
        public NumProperty( String name, String value )
        {
            Name = name ;
            Value = value;
        }

        public String Name { get;  }
        
        //TODO: check if prop are numerical
        public String Value { get;  }
    }
}