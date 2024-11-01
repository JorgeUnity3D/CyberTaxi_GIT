using System;

namespace BreakTheCycle.Util.NotificableFields
{
	[Serializable]
	public class NFloat : NotificableField<float>
    {
		public NFloat(float value)
		{
			Value = value;
		}
	}
}