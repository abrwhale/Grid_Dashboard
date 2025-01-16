using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace DrawingCanvas
{
    public partial class RectangleViewModel : ObservableObject
    {
        [ObservableProperty]
        private int index;

        [ObservableProperty]
        private double x;

        [ObservableProperty]
        private double y;

        [ObservableProperty]
        private double width;

        [ObservableProperty]
        private double height;

        [ObservableProperty]
        private bool isSelected;

        // 기본 사각형 테두리 색상
        [ObservableProperty]
        private Brush strokeColor = Brushes.Red;
    }
}
