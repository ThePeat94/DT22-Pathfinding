using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    /// <summary>
    /// This is used for visualization purposes. It is started as a coroutine so it can yield some delays so it can visualize
    /// how the algorithm is working. This is way slower than using the actual clean approach.
    /// </summary>
    public class BreadthFirst : Pathfinding
    {
        public override IEnumerator FindWayToNode(Node startNode, Node targetNode)
        {
            if (startNode.IsBlocked || targetNode.IsBlocked)
            {
                yield break;
            }

            this.IsFinding = true;
            
            startNode.SetStartColor();
            targetNode.SetTargetColor();
            
            // ---- EXPLORE GRID UNTIL TARGETNODE ---
            var frontier = new Queue<Node>();
            frontier.Enqueue(startNode);
            var nodePredecessor = new Dictionary<Node, Node>();
            nodePredecessor.Add(startNode, null);

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Dequeue();
                if(currentNode != startNode && currentNode != targetNode)
                    currentNode.SetCurrentSubjectColor();
                if(this.m_wait)
                    yield return new WaitForSeconds(this.m_visualizationDelay);
                foreach (var edge in currentNode.Edges)
                {
                    if (nodePredecessor.ContainsKey(edge.EndNode) || edge.EndNode.IsBlocked)
                        continue;

                    frontier.Enqueue(edge.EndNode);
                    if(edge.EndNode != startNode && edge.EndNode != targetNode)
                        edge.EndNode.SetQueueColor();
                    nodePredecessor.Add(edge.EndNode, currentNode);
                    if(this.m_wait)
                        yield return new WaitForSeconds(this.m_visualizationDelay);
                }

                if (nodePredecessor.ContainsKey(targetNode))
                    break;

                if(currentNode != startNode && currentNode != targetNode)
                    currentNode.SetTraversedColor();
                if(this.m_wait)
                    yield return new WaitForSeconds(this.m_visualizationDelay);
            }
            
            // --- END ---
            
            // --- GET NODES TO TARGETNODE STARTING FROM THE NODE ---

            if (!nodePredecessor.ContainsKey(targetNode))
            {
                targetNode.SetUnreachableColor();
                yield break;
            }
            
            var currentPathNode = nodePredecessor[targetNode];
            
            while (currentPathNode != null)
            {
                currentPathNode.SetPathColor();
                if(this.m_wait)
                    yield return new WaitForSeconds(this.m_visualizationDelay);
                currentPathNode = nodePredecessor[currentPathNode];
            }
            
            startNode.SetStartColor();
            targetNode.SetTargetColor();

            this.IsFinding = false;
            yield return null;
        }
    }
}