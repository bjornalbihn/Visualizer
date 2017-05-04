using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Iterative
{
	public class IterativeElement : PooledObject 
	{
		[SerializeField] private Transform[] _spawnPoints;
		[SerializeField] private bool _collisionCheck;

		private int _depth;
		public List<IterativeElement> Children {private set; get;}

		public override void Activate ()
		{
			base.Activate ();
			Children = new List<IterativeElement>();
		}
			
		public void Setup(IterativeSpawner spawner, int depth)
		{
			_depth = depth;
			for(int i = 0; i<_spawnPoints.Length; i++)
			{
				IterativeElement child = spawner.SpawnElement(depth);
				if (child != null)
				{
					Transform spawnPoint = _spawnPoints[i];
					Children.Add(child);
					child.transform.position = spawnPoint.position;
					child.transform.rotation = spawnPoint.rotation;
					child.transform.localScale = spawnPoint.localScale;

					child.Setup(spawner, depth +1);
				}					
			}		
		}

		public void KillAllChildren()
		{
			for(int i = 0; i<Children.Count; i++)
			{
				Children[i].KillAllChildren();
			}

			Deactivate();
		}
	}
}

