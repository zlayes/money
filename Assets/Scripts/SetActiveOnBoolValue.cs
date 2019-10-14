using UnityEngine;

namespace Money
{
	public class SetActiveOnBoolValue : MonoBehaviour
	{
		[SerializeField] BoolData boolData;
		[SerializeField] bool activeValue;
		[SerializeField] GameObject targetGameObject;

		void Awake () 
		{
			boolData.onValueChanged += OnValueChanged;
			OnValueChanged (boolData.Value);
		}

		void OnDestroy () 
		{
			if( boolData != null )
				boolData.onValueChanged -= OnValueChanged;
		}

		void OnValueChanged(bool boolValue)
		{
			targetGameObject.SetActive (boolValue == activeValue);
		}
	}
}