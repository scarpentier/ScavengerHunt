using System;
using System.Collections.Generic;

namespace ScavengerHunt.Web
{
    public static class Extensions
    {
        public static T FindOrDefault<T>(this List<T> list, Predicate<T> predicate, T defaultT)
        {
            int index = list.FindIndex(predicate);
            return (index != -1) ? list[index] : defaultT;
        }
    }
}
