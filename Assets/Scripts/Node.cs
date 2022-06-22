using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private Color m_neighborColor;
        [SerializeField] private Color m_traversedColor;
        [SerializeField] private Color m_targetColor;
        [SerializeField] private Color m_pathColor;
        [SerializeField] private Color m_startColor;
        [SerializeField] private Color m_currentlyQueuedColor;
        [SerializeField] private Color m_defaultColor;
        [SerializeField] private Color m_unreachableColor;
        [SerializeField] private Color m_blockedColor;

        private MeshRenderer m_meshRenderer;
        private Material m_blockedMaterial;

        public List<Edge> Edges { get; private set; }
        public bool IsBlocked { get; private set; }

        private void Awake()
        {
            this.Edges = new();
            this.m_meshRenderer = this.GetComponent<MeshRenderer>();
        }

        public void SetTraversedColor()
        {
            this.m_meshRenderer.material.color = this.m_traversedColor;
        }

        public void SetQueueColor()
        {
            this.m_meshRenderer.material.color = this.m_neighborColor;
        }

        public void SetCurrentSubjectColor()
        {
            this.m_meshRenderer.material.color = this.m_currentlyQueuedColor;
        }

        public void SetPathColor()
        {
            this.m_meshRenderer.material.color = this.m_pathColor;
        }

        public void SetStartColor()
        {
            this.m_meshRenderer.material.color = this.m_startColor;
        }

        public void SetTargetColor()
        {
            this.m_meshRenderer.material.color = this.m_targetColor;
        }

        public void SetUnreachableColor()
        {
            this.m_meshRenderer.material.color = this.m_unreachableColor;
        }

        public void ResetColor()
        {
            if (this.IsBlocked)
                return;
            this.m_meshRenderer.material.color = this.m_defaultColor;
        }
        
        public void ToggleBlocked()
        {
            this.IsBlocked = !this.IsBlocked;
            this.m_meshRenderer.material.color = this.IsBlocked ? this.m_blockedColor : this.m_defaultColor;
        }
    }
}