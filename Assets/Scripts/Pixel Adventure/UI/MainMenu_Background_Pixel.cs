using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Background_Pixel : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Vector2 backgroundSpeed;

    private void Update()
    {
        mesh.material.mainTextureOffset += backgroundSpeed * Time.deltaTime;
    }
}
