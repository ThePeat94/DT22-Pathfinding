using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    /// <summary>
    /// This is the actual clean implementation of BFS. Use this as a reference.
    /// </summary>
    public class BreadthFirstClean
    {
        public LinkedList<Node> FindWayToNode(Node startNode, Node targetNode)
        {
            var nodePredecessors = this.ExploreGraph(startNode, targetNode);
            var linkedPath = this.FindPath(targetNode, nodePredecessors);
            return linkedPath;
        }

        private LinkedList<Node> FindPath(Node targetNode, Dictionary<Node, Node> nodePredecessors)
        {
            var linkedPath = new LinkedList<Node>();
            if (!nodePredecessors.ContainsKey(targetNode))
                return linkedPath;
            
            var currentPathNode = nodePredecessors[targetNode];

            while (currentPathNode != null)
            {
                linkedPath.AddFirst(currentPathNode);
                currentPathNode = nodePredecessors[currentPathNode];
            }

            return linkedPath;
        }

        private Dictionary<Node, Node> ExploreGraph(Node startNode, Node targetNode)
        {
            var frontier = new Queue<Node>();
            frontier.Enqueue(startNode);
            var nodePredecessor = new Dictionary<Node, Node>();
            nodePredecessor.Add(startNode, null);

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Dequeue();
                foreach (var edge in currentNode.Edges)
                {
                    if (nodePredecessor.ContainsKey(edge.EndNode) || edge.EndNode.IsBlocked)
                        continue;

                    frontier.Enqueue(edge.EndNode);
                    nodePredecessor.Add(edge.EndNode, currentNode); // Save the current node as the neighbors predecessor
                }

                if (nodePredecessor.ContainsKey(targetNode))
                    return nodePredecessor;
            }
            return nodePredecessor;
        }
    }
}