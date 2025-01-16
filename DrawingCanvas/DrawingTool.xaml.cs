using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DrawingCanvas
{
    public partial class DrawingTool : UserControl
    {
        public DrawingTool()
        {
            InitializeComponent();

            // UserControl의 DataContext가 아직 없다면 새 ViewModel 할당
            if (this.DataContext == null)
            {
                this.DataContext = new DrawingToolViewModel();
            }

            // 로드된 후 UserControl에 포커스 설정 (키 입력 가능)
            this.Loaded += (s, e) =>
            {
                // 실제로 포커스를 가져와야 KeyDown 이벤트 발생
                Keyboard.Focus(this);
            };
        }

        /// <summary>
        /// 키를 눌러 사각형 그리기 모드 활성화 (1,2 키)
        /// </summary>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            // ViewModel에 접근
            if (DataContext is DrawingToolViewModel vm)
            {
                if (e.Key == Key.D1 || e.Key == Key.NumPad1)
                {
                    // '1'키 -> StartDrawingCommand(1)
                    vm.StartDrawingCommand.Execute(1);
                    MessageBox.Show("1번 사각형 그리기 모드 ON");
                }
                else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
                {
                    // '2'키 -> StartDrawingCommand(2)
                    vm.StartDrawingCommand.Execute(2);
                    MessageBox.Show("2번 사각형 그리기 모드 ON");
                }
            }
        }

        /// <summary>
        /// 마우스 왼쪽 버튼 Down => 사각형 그리기 시작
        /// </summary>
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is DrawingToolViewModel vm)
            {
                if (vm.CanDraw)
                {
                    var startPoint = e.GetPosition((Canvas)sender);
                    vm.StartDrawingAt(startPoint.X, startPoint.Y);
                }
            }
        }

        /// <summary>
        /// 마우스 Move => 드래그 중이면 사각형 크기 업데이트
        /// </summary>
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (DataContext is DrawingToolViewModel vm)
            {
                if (e.LeftButton == MouseButtonState.Pressed && vm.CanDraw && vm.CurrentRectangle != null)
                {
                    var currentPoint = e.GetPosition((Canvas)sender);
                    vm.UpdateRectangle(vm.CurrentRectangle.X, vm.CurrentRectangle.Y, currentPoint.X, currentPoint.Y);
                }
            }
        }

        /// <summary>
        /// 마우스 Left 버튼 Up => 사각형 그리기 완료
        /// </summary>
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is DrawingToolViewModel vm)
            {
                if (vm.CanDraw && vm.CurrentRectangle != null)
                {
                    var endPoint = e.GetPosition((Canvas)sender);
                    vm.FinishDrawing(endPoint.X, endPoint.Y);
                }
            }
        }

        /// <summary>
        /// UserControl 외부에서 직접 ImagePath를 설정할 수 있는 프로퍼티
        /// </summary>
        public string ImagePath
        {
            get => (DataContext as DrawingToolViewModel)?.ImagePath;
            set
            {
                if (DataContext is DrawingToolViewModel vm)
                {
                    vm.ImagePath = value;
                }
            }
        }
    }
}
