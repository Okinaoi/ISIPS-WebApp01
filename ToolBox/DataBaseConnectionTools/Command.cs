using System;
using System.Collections.Generic;
using System.Data;

namespace ToolBox.DataBaseConnectionTools
{
    public class Command
    {
        internal string Query { get; private set; }
        internal bool IsStoredProcedure { get; private set; }
        internal IDictionary<string, object> Parameters { get; private set; }

        public Command(bool isStoredProcedure = false)
        {
            IsStoredProcedure = isStoredProcedure;
            Parameters = new Dictionary<string, object>();
        }

        public Command(string query, bool isStoredProcedure = false)
        {
            Query = query;
            IsStoredProcedure = isStoredProcedure;
            Parameters = new Dictionary<string, object>();
        }

        public void AddParameter(string parameterName, object value)
        {
            Parameters.Add(parameterName, value ?? DBNull.Value);
        }

        public void AddParameter(string parameterName, object value, SqlDbType type)
        {
            Parameters.Add(parameterName, new ToolParameter(value ?? DBNull.Value, type));
        }
    }
}
