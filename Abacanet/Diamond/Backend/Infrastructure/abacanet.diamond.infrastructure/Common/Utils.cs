using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace abacanet.diamond.infrastructure.common
{
    public static class Utils
    {
        //Returns the description enum of user status
        public static string GetEnumDescriptionUserStatus(int value)
        {
            string descricao = string.Empty;
            UserStatusEnum en = (UserStatusEnum)value;
            FieldInfo fi = en.GetType().GetField(en.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                descricao = attributes[0].Description;
            else
                descricao = Enum.GetName(typeof(UserStatusEnum), value);

            return descricao;
        }
    }
}
