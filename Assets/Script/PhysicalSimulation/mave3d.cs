using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class mave3d : MonoBehaviour
{
    // Start is called before the first frame update
    private Mesh mesh;
    private const int demension=100;
    public Material mat;
    Vector3[] vertices = new Vector3[demension * demension];
    Vector3[] preVertices = new Vector3[demension * demension];
    Vector3[] newVertices = new Vector3[demension * demension];
    int[] triangles = new int[6 *( demension-1) * (demension-1)];
    protected MeshFilter meshFilter;
    private Vector3[] CreateVertices()
    {
        for(int j=0;j<demension;j++)
        {
            for(int i=0;i<demension;i++)
            {
                preVertices[j*demension + i]=vertices[j * demension + i] = new Vector3(i / 10.0f, 0, j / 10.0f);
            }
        }
        return vertices;
    }
    private int[] CreateTriangles()
    {
       
        for(int i=0;i<demension-1;++i)
        {
            for(int j=0;j<demension-1;++j)
            {
                int idx = (j * (demension-1) + i)*6;
                int vidx =(j * demension + i);
                triangles[idx] = vidx;
                triangles[idx + 1] = vidx + demension;
                triangles[idx + 2] = vidx + demension + 1;
                triangles[idx + 3] = vidx;
                triangles[idx + 4] = vidx + demension + 1;
                triangles[idx + 5] = vidx + 1;
            }
        }
        return triangles;
    }

    void Start()
    {
        Application.targetFrameRate = 10;
        mesh = new Mesh();
        MeshRenderer meshRender = GetComponent<MeshRenderer>();

        meshRender.material = mat;
        mesh.name = gameObject.name;
       
        
    
        mesh.vertices =CreateVertices();
        mesh.triangles= CreateTriangles();

  
     
        mesh.RecalculateBounds();

     
 

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
    

        float left=0, right=0, up=0, down=0;
 
        for(int i=0;i<demension;++i)
        {
            for (int j=0;j<demension;++j)
            {
                float Force = 0;


                if (i - 1 >= 0) left = vertices[j* demension + i-1].y;
                if (i + 1 < demension) right = vertices[j * demension + i+1].y;
                if (j - 1 >= 0) down = vertices[(j-1) * demension + i].y;
                if (j + 1 < demension) up = vertices[(j+1) * demension + i ].y;
      

                if (i==(demension)/2&&j==(demension)/2)
                {
                    Force = Mathf.Sin(2*Time.time)*2;
                }
                int idx = j * demension + i;
                float cur = vertices[idx].y;
                float pre = preVertices[idx].y;
                float speed = 0.6f;
                float height = 2 * cur - pre + (left + right + up + down - 4 * cur) * speed * speed + Force;
                newVertices[idx]= new Vector3(i / 10.0f, height, j / 10.0f);
            }
        }
        mesh.vertices = newVertices;
        preVertices = (Vector3 [])vertices.Clone();
        vertices = (Vector3[])newVertices.Clone();
    
        mesh.RecalculateNormals();
    }
}
