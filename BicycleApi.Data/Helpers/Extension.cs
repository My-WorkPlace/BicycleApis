using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace BicycleApi.Data.Helpers
{
	public static class  Extension
	{
		public static IQueryable<T> Sort<T>(this IQueryable<T> source, string sortBy)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			if (string.IsNullOrEmpty(sortBy))
				throw new ArgumentNullException("sortBy");

			source = source.OrderBy(sortBy);

			return source;
		}
    }
}
