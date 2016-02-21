using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder {
	public static List<Hex> findPath(Hex start, Hex goal) {
		Dictionary<Hex,int> distances = new Dictionary<Hex,int> ();
		Dictionary<Hex,Hex> previous = new Dictionary<Hex,Hex> ();

		List<Hex> queue = new List<Hex> { start };
		distances [start] = 0;

		while (queue.Count > 0) {
			queue.Sort ((a, b) => (distances[a] + Hex.Distance (a, goal)) - (distances[b] + Hex.Distance (b, goal)));
			Hex current = queue [0];
			queue.RemoveAt (0);
			if (current == goal) {
				List<Hex> path = new List<Hex> {goal};
				while (previous.ContainsKey (path [path.Count - 1])) {
					path.Add (previous [path [path.Count - 1]]);
				}
				path.Reverse ();
				return path;
			}
			for (int i = 0; i < 6; i++) {
				Hex neighbor = Hex.Neighbor (current, i);
				if (!distances.ContainsKey (neighbor) || distances [current] + 1 < distances [neighbor]) {
					distances [neighbor] = distances [current] + 1;
					previous [neighbor] = current;
					queue.Add (neighbor);
				}
			}
		}
		throw new UnityException ();
	}
}
