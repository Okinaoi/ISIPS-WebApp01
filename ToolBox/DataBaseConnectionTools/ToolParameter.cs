using System.Data;

namespace ToolBox.DataBaseConnectionTools
{
    public class ToolParameter
    {
        private object _Value;
        private SqlDbType _Type;

        public ToolParameter(object value, SqlDbType type)
        {
            _Value = value;
            _Type = type;
        }

        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public SqlDbType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

    }
}
