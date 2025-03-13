using TestProject.Core;
using UnityEngine;

namespace DefaultNamespace
{
    public class ColorObjectView : BaseView
    {
        [SerializeField]
        private Renderer _rendererObj;
        private MaterialPropertyBlock _propBlock;
        
        private MatchModel _matchModel;
        private MatchController _matchController;
        private Color _objectColor;
        
        private static readonly int _colorProperty = Shader.PropertyToID("_Color");

        void Start()
        {
            _propBlock = new MaterialPropertyBlock();
            
            _matchModel = Dispatcher.GetModel<MatchModel>();
            _matchController = Dispatcher.GetController<MatchController>();
            _matchModel.OnNewColorsGenerated += NewColorsGenerated;
            
            if (_matchModel.HasFreeColors)
                NewColorsGenerated();
        }

        private void NewColorsGenerated()
        {
            if (_matchModel.HasFreeColors)
            {
                _objectColor = _matchModel.GetFreeColor();
                ChangeObjectColor(_objectColor);
            }
            else
                Logger.Log(LogType.Error, $"There are no colors for the object {name}");
        }

        private void ChangeObjectColor(Color color)
        {
            _rendererObj.GetPropertyBlock(_propBlock);
            _propBlock.SetColor(_colorProperty, color);
            _rendererObj.SetPropertyBlock(_propBlock);
        }

        private void OnMouseDown()
        {
            if (_matchController.CheckColorSelection(_objectColor))
            {
                _matchController.GenerateRandomColors();
            }
            else
            {
                Logger.Log(LogType.Log, "Wrong color");
            }
        }

        private void OnDestroy()
        {
            _matchModel.OnNewColorsGenerated -= NewColorsGenerated;
        }
    }
}