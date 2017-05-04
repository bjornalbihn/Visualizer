using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iterative
{
	public class Pool : MonoBehaviour 
	{
		[SerializeField] private List<IterativeElement> _poolPrefabs;
		[SerializeField] private int _prefill;

		private List<IterativeElement> _elements = new List<IterativeElement>();

		private void Awake()
		{
			if (_prefill>0)
			{
				for (int i = 0; i<_prefill; i++)
				{
					IterativeElement element = CreateElement();
					element.Deactivate();
				}
			}
		}

		private IterativeElement CreateElement()
		{
			IterativeElement element = Instantiate(_poolPrefabs[Random.Range(0,_poolPrefabs.Count)]);
			_elements.Add(element);
			element.Activate();
			return element;
		}

		public void ReturnAll()
		{
			for (int i = 0; i<_elements.Count; i++)
			{
				_elements[i].Deactivate();
			}
		}

		public IterativeElement Spawn()
		{
			for (int i = 0; i<_prefill; i++)
			{
				if (_elements[i].Active == false)
				{
					_elements[i].Activate();
					return _elements[i];
				}
			}

			return CreateElement();
		}
	}
}