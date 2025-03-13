using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class ColorGenerator
    {
        public void GenerateColors(List<Color> colorsToGenerate, List<Color> previousColors, int count)
        {
            do
            {
                colorsToGenerate.Clear();
                
                for (int i = 0; i < count; i++)
                {
                    Color color = new Color(Random.value, Random.value, Random.value);
                    colorsToGenerate.Add(color);
                }
            } 
            while (AreColorsSimilar(colorsToGenerate, previousColors));
        }
        
        bool AreColorsSimilar(List<Color> colors1, List<Color> colors2)
        {
            for (int i = 0; i < colors1.Count; i++)
            {
                for (int j = 0; j < colors2.Count; ++j)
                {
                    if (IsColorClose(colors1[i], colors2[j]))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        bool IsColorClose(Color c1, Color c2, float threshold = 0.1f)
        {
            return Mathf.Abs(c1.r - c2.r) < threshold &&
                   Mathf.Abs(c1.g - c2.g) < threshold &&
                   Mathf.Abs(c1.b - c2.b) < threshold;
        }
    }
}