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

        public static Role ToRole(this int value)
        {
            Role role;
            switch (value)
            {               
                case 2:
                    role = Role.Technician;
                    break;
                case 3:
                    role = Role.Admin;
                    break;
                default:
                    role = Role.Client;
                    break;
            }
            return role;
        }
    }
}
