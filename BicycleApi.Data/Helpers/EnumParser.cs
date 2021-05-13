using System;
using BicycleApi.DBData.Entities;

namespace BicycleApi.Data.Helpers
{
	public static class EnumParser
	{
		public static string GetName(DetailType value)
		{
			return Enum.GetName(typeof(DetailType), value);
		}
	}
}
