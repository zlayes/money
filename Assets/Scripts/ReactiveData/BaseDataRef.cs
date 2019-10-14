using UnityEngine;
using System;

namespace Money
{
    public class BaseDataRef<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] bool printLogs;
        protected T runtimeValue;
        public Action<T> onValueChanged;

        public void OnBeforeSerialize()
        { }

        public virtual void OnAfterDeserialize()
        {         
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