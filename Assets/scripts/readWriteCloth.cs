using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readWriteCloth : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        print(mesh.isReadable);
    }
}
