﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.BAL.Common
{
    public static class EnumConverter
    {
        public static T GetValueFromName<T>(this string name) where T : Enum
        {
            var type = typeof(T);

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (attribute.Name == name)
                    {
                        return (T)field.GetValue(null);
                    }
                }

                if (field.Name == name)
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentOutOfRangeException(nameof(name));
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue
                      .GetType()
                      .GetMember(enumValue.ToString())
                      .First()?
                      .GetCustomAttribute<DisplayAttribute>()?
                      .Name;
        }
    }
}
