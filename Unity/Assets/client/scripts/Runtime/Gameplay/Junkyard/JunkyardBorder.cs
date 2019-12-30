using System;
using UnityEngine;

public class JunkyardBorder : MonoBehaviour
{
    public void Render(JunkyardRenderConfig renderConfig, Junkyard junkyard)
    {
        int widthUnits = (int) Math.Ceiling(junkyard.Width / renderConfig.WallWidth);
        int heightUnits = (int) Math.Ceiling(junkyard.Height / renderConfig.WallWidth);

        //top
        RenderWalls(renderConfig, Vector3.zero, new Vector3(renderConfig.WallWidth, 0, 0), Quaternion.Euler(0, 90, 0), widthUnits);
        //right
        RenderWalls(renderConfig, new Vector3(junkyard.Width, 0, 0), new Vector3(0, 0, renderConfig.WallWidth), Quaternion.Euler(0, 0, 0), heightUnits);
        //bottom
        RenderWalls(renderConfig, new Vector3(0, 0, junkyard.Height), new Vector3(renderConfig.WallWidth, 0, 0), Quaternion.Euler(0, 90, 0), widthUnits);
        //left
        RenderWalls(renderConfig, Vector3.zero, new Vector3(0, 0, renderConfig.WallWidth), Quaternion.Euler(0, 0, 0), heightUnits);
    }

    private void RenderWalls(JunkyardRenderConfig renderConfig, Vector3 start, Vector3 step, Quaternion rotation, int units)
    {
        for (int i = 0; i < units; i++)
        {
            Instantiate(renderConfig.Wall, start + (step * i), rotation, transform);
        }
    }
}
