using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ProcedimentoCriminal.Reportacao.Domain
{
    public static class Extensions
    {
        public static bool IsOneOf<T>(this T self, params T[] values)
        {
            return values.Contains(self);
        }

        public static bool HasNo<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return !props(self).Any();
        }

        public static bool HasSome<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return props(self).Any();
        }

        public static bool AnyIsNullOrWhiteSpace(params string[] values)
        {
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) return true;
            }

            return false;
        }

        public static string GetEnumDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var attributes =
                fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
                return attributes.First().Description;

            return value.ToString();
        }
    }
}