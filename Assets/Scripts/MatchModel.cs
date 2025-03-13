
using System;
using System.Collections.Generic;
using TestProject.Core;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// Main model that contains data for the generated colors and the color that required to select
    /// </summary>
    public class MatchModel : IModel
    {
        public int ColorsToGenerate = 3;
        public Stack<Color> GeneratedColors;

        private Color _colorToSelect;
        
        public event Action<Color> OnColorToSelectUpdated;
        public event Action OnNewColorsGenerated;

        public bool HasFreeColors => GeneratedColors != null && GeneratedColors.Count > 0;
        
        public Color ColorToSelect
        {
            get => _colorToSelect;
            set
            {
                _colorToSelect = value;
                OnColorToSelectUpdated?.Invoke(value);
            }
        }
        
        /// <summary>
        /// Add available color to pick
        /// </summary>
        public void AddColor(Color color)
        {
            if (GeneratedColors == null)
                GeneratedColors = new Stack<Color>(ColorsToGenerate);
            
            GeneratedColors.Push(color);
        }

        /// <summary>
        /// Get available free color
        /// </summary>
        public Color GetFreeColor()
        {
            return GeneratedColors.Pop();
        }

        /// <summary>
        /// Rise OnNewColorsGenerated event to notify all subscribers that new colors for pick are available
        /// </summary>
        public void RiseOnNewColorsGenerated()
        {
            OnNewColorsGenerated?.Invoke();
        }
    }
}