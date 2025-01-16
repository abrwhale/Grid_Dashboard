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

            // UserControl�� DataContext�� ���� ���ٸ� �� ViewModel �Ҵ�
            if (this.DataContext == null)
            {
                this.DataContext = new DrawingToolViewModel();
            }

            // �ε�� �� UserControl�� ��Ŀ�� ���� (Ű �Է� ����)
            this.Loaded += (s, e) =>
            {
                // ������ ��Ŀ���� �����;� KeyDown �̺�Ʈ �߻�
                Keyboard.Focus(this);
            };
        }

        /// <summary>
        /// Ű�� ���� �簢�� �׸��� ��� Ȱ��ȭ (1,2 Ű)
        /// </summary>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            // ViewModel�� ����
            if (DataContext is DrawingToolViewModel vm)
            {
                if (e.Key == Key.D1 || e.Key == Key.NumPad1)
                {
                    // '1'Ű -> StartDrawingCommand(1)
                    vm.StartDrawingCommand.Execute(1);
                    MessageBox.Show("1�� �簢�� �׸��� ��� ON");
                }
                else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
                {
                    // '2'Ű -> StartDrawingCommand(2)
                    vm.StartDrawingCommand.Execute(2);
                    MessageBox.Show("2�� �簢�� �׸��� ��� ON");
                }
            }
        }

        /// <summary>
        /// ���콺 ���� ��ư Down => �簢�� �׸��� ����
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
        /// ���콺 Move => �巡�� ���̸� �簢�� ũ�� ������Ʈ
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
        /// ���콺 Left ��ư Up => �簢�� �׸��� �Ϸ�
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
        /// UserControl �ܺο��� ���� ImagePath�� ������ �� �ִ� ������Ƽ
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
