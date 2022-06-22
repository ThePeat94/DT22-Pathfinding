namespace Pathfinding
{
        [System.Serializable]
        public class Edge
        {
                public Node StartNode { get; set; }
                public Node EndNode { get; set; }

                public int Weight { get; set; }
        }
}