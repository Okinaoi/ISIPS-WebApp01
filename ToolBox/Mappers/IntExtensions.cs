using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolBox.Mappers
{
    public static class IntExtensions
    {
        public static bool ToBoolean(this int value)
        {
            return value == 1 ? true : false;
        }

        public static string ToRole(this int value)
        {
            return ((Role)value).ToString();
        }
    }
}
