using Unity.VisualScripting;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void SetRandomColor(Cube cube)
    {
        Renderer renderer = cube.GetRenderer();
        renderer.material.color = Random.ColorHSV();

    }

    public void SetDefaultColor(Cube cube)
    {
        Renderer renderer = cube.GetRenderer();
        renderer.material.color = Color.white;
    }
}
