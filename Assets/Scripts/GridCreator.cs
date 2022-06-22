using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class GridCreator : MonoBehaviour
    {
        [SerializeField] private int m_width;
        [SerializeField] private int m_height;
        [SerializeField] private GameObject m_cubePrefab;

        private Dictionary<Vector3, Node> m_nodeGrid;

        public Dictionary<Vector3, Node> NodeGrid => m_nodeGrid;


        private Vector3[] m_neighborDirs = new[]
        {
            Vector3.right,
            Vector3.left,
            Vector3.forward,
            Vector3.back,
        };

        private void Awake()
        {
            this.m_nodeGrid = new();
        }

        private void Start()
        {
            CreateNodeGrid();
            LinkNodesWithNeighbors();
        }

        public void ResetNodeColors()
        {
            foreach (var (key, node) in this.m_nodeGrid)
            {
                node.ResetColor();
            }
        }
        
        private void CreateNodeGrid()
        {
            for (var x = 0; x < m_width; x++)
            {
                for (var y = 0; y < m_height; y++)
                {
                    var targetPos = new Vector3(x, 0, y);
                    var node = Instantiate(m_cubePrefab, targetPos, Quaternion.identity).GetComponent<Node>();
                    node.name = $"Node ({targetPos})";
                    this.m_nodeGrid.Add(targetPos, node);
                }
            }
        }

        private void LinkNodesWithNeighbors()
        {
            foreach (var kvp in m_nodeGrid)
            {
                foreach (var dir in m_neighborDirs)
                {
                    var potentialNeighborPosition = kvp.Key + dir;
                    if (m_nodeGrid.ContainsKey(potentialNeighborPosition))
                    {
                        kvp.Value.Edges.Add(new Edge()
                        {
                            StartNode = kvp.Value,
                            EndNode = m_nodeGrid[potentialNeighborPosition]
                        });
                    }
                }
            }
        }
    }
}