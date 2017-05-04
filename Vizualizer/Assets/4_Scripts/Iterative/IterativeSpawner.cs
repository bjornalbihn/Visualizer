using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iterative
{
	public class IterativeSpawner : MonoBehaviour 
	{
		[SerializeField] private Transform[] _spawnPoints;
		[SerializeField] private Pool _pool;

		[SerializeField] private int _maxDepth;
		[SerializeField] private int _maxAmount;

		private int _amount;

		void Update () 
		{
			if (Input.GetKeyDown(KeyCode.L))
				Pulse();
		}

		private void Pulse()
		{
			_pool.ReturnAll();
			_amount = 0;

			foreach (Transform spawnPoint in _spawnPoints)
			{
				IterativeElement element = _pool.Spawn();
				element.Setup(this, 0);
			}
		}

		public IterativeElement SpawnElement(int depth)
		{
			if (_amount < _maxAmount && depth < _maxDepth)
			{
				return _pool.Spawn();
				_amount ++;
			}
			else
				return null;
		}
	}
}