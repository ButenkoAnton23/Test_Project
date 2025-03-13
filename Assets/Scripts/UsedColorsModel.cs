using System.Collections.Generic;
using TestProject.Core;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// Model for already used colors in a previous round
    /// </summary>
    public class UsedColorsModel : IModel
    {
        public List<Color> Colors;
    }
}