using TestProject.Core;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ColorPickerView : BaseView
    {
        [SerializeField]
        private TextMeshProUGUI _textObj;
        
        private MatchModel _matchModel;
        
        void Start()
        {
            _matchModel = Dispatcher.GetModel<MatchModel>();
            _matchModel.OnColorToSelectUpdated += OnColorToSelectUpdated;
            
            MatchController matchController = Dispatcher.GetController<MatchController>();
            matchController.GenerateRandomColors();
        }

        private void OnColorToSelectUpdated(Color color)
        {
            _textObj.text = $"Select corresponding color: {color.ToString()}";
        }

        private void OnDestroy()
        {
            _matchModel.OnColorToSelectUpdated -= OnColorToSelectUpdated;
        }
    }
}