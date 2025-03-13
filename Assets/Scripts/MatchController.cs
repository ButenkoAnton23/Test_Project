using System.Collections.Generic;
using TestProject.Core;
using UnityEngine;

namespace DefaultNamespace
{
    public class MatchController : IController
    {
        private MatchModel _matchModel;
        private UsedColorsModel _usedColors;
        
        private List<Color> _colorsToPick;
        private ColorGenerator _colorGenerator;
        
        public void Initialize()
        {
            _matchModel = Dispatcher.GetModel<MatchModel>();
            _usedColors = Dispatcher.GetModel<UsedColorsModel>();

            _colorGenerator = new ColorGenerator();
            _colorsToPick = new List<Color>(_matchModel.ColorsToGenerate);
            _usedColors.Colors = new List<Color>(_matchModel.ColorsToGenerate);
        }

        /// <summary>
        /// Generate random colors for the match models
        /// </summary>
        public void GenerateRandomColors()
        {
            _colorGenerator.GenerateColors(_colorsToPick, previousColors: _usedColors.Colors, _matchModel.ColorsToGenerate);
            
            _usedColors.Colors.Clear();
            _usedColors.Colors.AddRange(_colorsToPick);

            for (int i = 0; i < _colorsToPick.Count; ++i)
            {
                _matchModel.AddColor(_colorsToPick[i]);
            }
            
            int randomColorIndex = Random.Range(0, _colorsToPick.Count);
            _matchModel.ColorToSelect = _colorsToPick[randomColorIndex];
            
            _matchModel.RiseOnNewColorsGenerated();
        }

        /// <summary>
        /// Check if the argument color match the required to select color
        /// </summary>
        public bool CheckColorSelection(Color color)
        {
            return color.Equals(_matchModel.ColorToSelect);
        }
    }
}