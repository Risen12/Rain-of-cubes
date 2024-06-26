using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private const string ColorTag = "_Color";

    public void SetRandomColor(Cube cube)
    {
        Renderer renderer = cube.GetRenderer();
        renderer.material.SetColor(ColorTag, Random.ColorHSV());
    }

    public void SetDefaultColor(Cube cube)
    {
        Renderer renderer = cube.GetRenderer();
        renderer.material.SetColor(ColorTag, Color.white);
    }
}
