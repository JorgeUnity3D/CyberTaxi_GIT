using System;

namespace BreakTheCycle.Util.NotificableFields
{
	[Serializable]
	public class NInt : NotificableField<int>
	{
		public NInt(int value)
		{
			Value = value;
		}
	}
}