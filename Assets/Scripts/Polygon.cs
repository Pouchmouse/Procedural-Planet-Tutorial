using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon
{
    public List<int>     m_Vertices;
    public List<Polygon> m_Neighbors;
    public Color32       m_Color;
    public bool          m_SmoothNormals;

    public Polygon(int a, int b, int c)
    {
        m_Vertices      = new List<int>() { a, b, c };
        m_Neighbors     = new List<Polygon>();
        m_SmoothNormals = true;

        // Hot Pink is an excellent default color because you'll notice instantly if 
        // you forget to set it to something else.
        m_Color = new Color32(255, 0, 255, 255);
    }

    public bool IsNeighborOf(Polygon other_poly)
    {
        int shared_vertices = 0;
        foreach (int vertex in m_Vertices)
        {
            if (other_poly.m_Vertices.Contains(vertex))
                shared_vertices++;
        }

        // A polygon and its neighbor will share exactly
        // two vertices. Ergo, if this poly shares two
        // vertices with the other, then they are neighbors.

        return shared_vertices == 2;
    }

    public void ReplaceNeighbor(Polygon oldNeighbor, Polygon newNeighbor)
    {
        for(int i = 0; i < m_Neighbors.Count; i++)
        {
            if(oldNeighbor == m_Neighbors[i])
            {
                m_Neighbors[i] = newNeighbor;
                return;
            }
        }
    }
}

// A PolySet is a set of unique Polygons. Basically it's a HashSet, but we also give it
// some handy convenience functions.

public class PolySet : HashSet<Polygon>
{
    //Given a set of Polys, calculate the set of Edges
    //that surround them.

    public EdgeSet CreateEdgeSet()
    {
        EdgeSet edgeSet = new EdgeSet();

        foreach (Polygon poly in this)
        {
            foreach (Polygon neighbor in poly.m_Neighbors)
            {
                if (this.Contains(neighbor))
                    continue;
                // If our neighbor isn't in our PolySet, then
                // the edge between us and our neighbor is one
                // of the edges of this PolySet.
                Edge edge = new Edge(poly, neighbor);
                edgeSet.Add(edge);
            }
        }
        return edgeSet;
    }

    // GetUniqueVertices calculates a list of the vertex indices used by these Polygons
    // with no duplicates.

    public List<int> GetUniqueVertices()
    {
        List<int> verts = new List<int>();
        foreach (Polygon poly in this)
        {
            foreach (int vert in poly.m_Vertices)
            {
                if (!verts.Contains(vert))
                    verts.Add(vert);
            }
        }
        return verts;
    }
}
