using System;
using UnityEngine;
using Tools;
using System.Collections.Generic;

namespace AI
{
	//TODO: Implement IPathFinder using Dijsktra algorithm.
	public class Dijkstra : IPathFinder
	{
		// 1 <- 2 <- 3 <- 4

		private List<Vector2Int> Grid;

		private Vector2Int CurrentNode = Vector2Int.zero;
		private Dictionary<Vector2Int, Vector2Int> ancestors = new Dictionary<Vector2Int, Vector2Int>();
		private Queue<Vector2Int> frontier = new Queue<Vector2Int>();

		public Dijkstra(List<Vector2Int> GridMap) { Grid = GridMap; }

		public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
		{
			CurrentNode = start;
			frontier.Enqueue(CurrentNode);

			while(frontier.Count > 0)
			{
				CurrentNode = frontier.Dequeue();
				if(CurrentNode == goal) { break; }

				else
				{
					foreach(Vector2Int currentNeighbour in Neighbours)
					{
						if(Grid.Contains(currentNeighbour))
						{
							if(!ancestors.ContainsKey(currentNeighbour))
							{
								frontier.Enqueue(currentNeighbour);
								ancestors.Add(currentNeighbour, CurrentNode);
							}
						}
					}
				}
			}

			if (ancestors.ContainsKey(goal))
			{
				List<Vector2Int> path = new List<Vector2Int>();
				while (CurrentNode != start)
				{
					path.Add(CurrentNode);
					CurrentNode = ancestors[CurrentNode];
				}
				path.Add(CurrentNode);

				path.Reverse();

				return path;
			}
			else { throw new Exception("No path to goal!"); }
		}

		private Vector2Int[] Neighbours
		{
			get
			{
				Vector2Int[] ReturnValue =
				{
					CurrentNode + Vector2Int.up,
					CurrentNode + Vector2Int.left,
					CurrentNode + Vector2Int.right,
					CurrentNode + Vector2Int.down
				};

				return ReturnValue;
			}
		}

	}    
}
