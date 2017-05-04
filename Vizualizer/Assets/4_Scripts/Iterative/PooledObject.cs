using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iterative
{
	public class PooledObject : MonoBehaviour 
	{
		public bool Active {private set; get;}

		public virtual void Activate()
		{
			Active = true;
			gameObject.SetActive(true);
		}
			
		public void Deactivate()
		{
			Active = false;
			gameObject.SetActive(false);
		}
	}
}

