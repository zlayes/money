using UnityEngine;
using System;


namespace Money
{
	public class BaseData<T> : ScriptableObject, ISerializationCallbackReceiver
	{
        [SerializeField] bool printLogs;
        [SerializeField] T initialValue;
        [SerializeField] T runtimeValue;
		[SerializeField] public Action<T> onValueChanged;

		public void OnBeforeSerialize ()
		{}

		public void OnAfterDeserialize ()
		{
			runtimeValue = initialValue;
		}

		public T Value
		{
			get
			{
				return runtimeValue;
			}
			set
			{
                if (printLogs)
                    Debug.Log(name + " " + runtimeValue + " => " + value);
				runtimeValue = value;
                onValueChanged?.Invoke(runtimeValue);
            }
		}
	}
}