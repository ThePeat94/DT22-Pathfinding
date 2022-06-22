using System;
using UnityEngine;

namespace Pathfinding
{
    public class Cursor : MonoBehaviour
    {
        private Pathfinding m_pathfinding;
        private Node m_startNode;
        
        private void Start()
        {
            this.m_pathfinding = FindObjectOfType<Pathfinding>();
        }

        private void Update()
        {
            if (this.m_pathfinding.IsFinding)
                return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                
                RaycastHit hit; 
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                if (Physics.Raycast(ray, out hit,100.0f))
                {

                    if (!hit.transform.TryGetComponent<Node>(out var node))
                        return;

                    if (this.m_startNode == null)
                    {
                        this.m_startNode = node;
                        this.m_startNode.SetStartColor();
                        return;
                    }
                    
                    FindObjectOfType<GridCreator>().ResetNodeColors();
                    node.SetTargetColor();
                    this.m_startNode.SetStartColor();
                    this.StartCoroutine(this.m_pathfinding.FindWayToNode(this.m_startNode, node));
                    this.m_startNode = null;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                RaycastHit hit; 
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                if (Physics.Raycast(ray, out hit,100.0f))
                {

                    if (!hit.transform.TryGetComponent<Node>(out var node))
                        return;
                    
                    node.ToggleBlocked();
                }
            }
        }
    }
}