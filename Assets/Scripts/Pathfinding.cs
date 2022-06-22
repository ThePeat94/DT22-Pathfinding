using System.Collections;
using UnityEngine;

namespace Pathfinding
{
    public abstract class Pathfinding : MonoBehaviour
    {
        [SerializeField] protected float m_visualizationDelay;
        [SerializeField] protected bool m_wait;
        
        
        public bool IsFinding { get; protected set; }
        
        
        public abstract IEnumerator FindWayToNode(Node startNode, Node targetNode);
    }
}