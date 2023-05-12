using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.WebUI.Helpers
{
    public static class LinqExtension
    {

        //public static IEnumerable<T> Flatten<T>( this IEnumerable<T> e, Func<T, IEnumerable<T>> f) => e.SelectMany(c => f(c).Flatten(f)).Concat(e);

        public static IEnumerable<T> Flatten<T>(this T source, Func<T, IEnumerable<T>> selector)
        {
            return selector(source).SelectMany(c => Flatten(c, selector))
                                   .Concat(new[] { source });
        }
    }
}