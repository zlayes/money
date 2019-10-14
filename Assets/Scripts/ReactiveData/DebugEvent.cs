using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Money
{
	public class DebugEvent : MonoBehaviour
	{
		[SerializeField] BoolData booData;

		void Update () 
		{
			if(Input.GetKeyDown(KeyCode.D))
			{
				if( booData.onValueChanged == null )
				{
					print ("0");
					return;
				}

				System.Delegate[] all = booData.onValueChanged.GetInvocationList ();
				for (int i = 0; i < all.Length ; i++) {
					print (all [i].Method);
				}
			}
		}
	}
}