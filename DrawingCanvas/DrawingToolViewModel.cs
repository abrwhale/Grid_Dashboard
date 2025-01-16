using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace DrawingCanvas
{
    public partial class DrawingToolViewModel : ObservableObject
    {
        // 배경 이미지 경로
        [ObservableProperty]
        private string imagePath;

        // 한 번만 그리기 허용 여부
        [ObservableProperty]
        private bool canDraw;

        // 현재 드래그로 그리고 있는 사각형
        [ObservableProperty]
        private RectangleViewModel currentRectangle;

        // 만들어진 사각형 리스트
        public ObservableCollection<RectangleViewModel> Rectangles { get; } = new();

        // 키(1,2) 입력 시 실행되는 커맨드 - 예시
        [RelayCommand]
        public void StartDrawing(int key)
        {
            // key 값에 따라 다른 모드로 동작할 수도 있지만, 여기서는 단순히 "그리기 활성화"
            CanDraw = true;
        }

        // 실제 드래그 로직
        public void StartDrawingAt(double x, double y)
        {
            // 새 사각형 추가
            CurrentRectangle = new RectangleViewModel
            {
                Index = Rectangles.Count + 1,
                X = x,
                Y = y,
                Width = 0,
                Height = 0
            };
            Rectangles.Add(CurrentRectangle);
        }

        public void UpdateRectangle(double startX, double startY, double currentX, double currentY)
        {
            if (CurrentRectangle == null) return;

            // 폭/높이 계산 (양수)
            double w = System.Math.Abs(currentX - startX);
            double h = System.Math.Abs(currentY - startY);

            // 작은 좌표가 X,Y
            double minX = System.Math.Min(startX, currentX);
            double minY = System.Math.Min(startY, currentY);

            CurrentRectangle.X = minX;
            CurrentRectangle.Y = minY;
            CurrentRectangle.Width = w;
            CurrentRectangle.Height = h;
        }

        public void FinishDrawing(double endX, double endY)
        {
            // 여기서는 "한 번 그린 후 CanDraw=false"
            CanDraw = true;
            CurrentRectangle = null;
        }

        [RelayCommand]
        public void DeleteRectangle(int index)
        {
            var rect = Rectangles.FirstOrDefault(r => r.Index == index);
            if (rect != null)
            {
                Rectangles.Remove(rect);
            }
        }
    }
}
