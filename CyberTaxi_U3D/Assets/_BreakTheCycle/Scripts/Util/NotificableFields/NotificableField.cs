using System;
using UnityEngine;
using UnityEngine.Events;

namespace BreakTheCycle.Util.NotificableFields
{
	[Serializable]
	public class NotificableField<T>
	{
		[SerializeField] private T _value;
		public event UnityAction<T> OnValueChanged;
		
		public T Value
		{
			get => _value;
			set
			{
				if (!Equals(_value, value))
				{
					_value = value;
					OnValueChanged?.Invoke(_value);
				}
			}
		}
	}
}