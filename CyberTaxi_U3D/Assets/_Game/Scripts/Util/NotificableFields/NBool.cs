using System;

namespace BreakTheCycle.Util.NotificableFields
{
	[Serializable]
	public class NBool : NotificableField<bool>
	{
		public NBool(bool value)
		{
			Value = value;
		}
	}
}