using LevelLearn.Resource.Enums;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace LevelLearn.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum enumValue)
        {
            DescriptionAttribute descriptionAttribute = enumValue.GetType().GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DescriptionAttribute>();

            return descriptionAttribute.Description ?? enumValue.ToString();
        }

        public static string GetDescriptionLocalized(this Enum enumValue)
        {
            try
            {
                var rm = new ResourceManager(typeof(EnumResources));
                string resourceKey = enumValue.GetType().Name + "_" + enumValue;
                string resourceDisplayName = rm.GetString(resourceKey);

                return resourceDisplayName;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return enumValue.ToString();
            }

        }

    }

}
