// EditBox and EditBoxAdorner taken from this example WPF example:
// https://github.com/Microsoft/WPF-Samples/tree/master/Sample%20Applications/ExpenseIt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using StardewCropCalculatorLibrary;

namespace StardewCropCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Week> calendar = new List<Week>();
        string profitSummary;

        public MainWindow()
        {
            InitializeComponent();

            // Create display calendar
            calendar.Add(new Week());
            calendar.Add(new Week());
            calendar.Add(new Week());
            calendar.Add(new Week());

            dgCalendar.ItemsSource = calendar;

            // Create display profit summary
            tbProfitSummary.Text = "";
        }

        public class Week
        {
            public string Monday { get; set; }
                    
            public string Tuesday { get; set; }
                    
            public string Wednesday { get; set; }
                    
            public string Thursday { get; set; }
                    
            public string Friday { get; set; }
                    
            public string Saturday { get; set; }
                    
            public string Sunday { get; set; }
        }

        private void addCropButton_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current;
            var expenseReport = (ExpenseReport)app.FindResource("ExpenseData");
            expenseReport?.LineItems.Add(new LineItem());
        }

        private void DeleteCrop_Click(object sender, RoutedEventArgs e)
        {
            //Border border = FindVisualParent<Border>(sender as Button);
            //int rowIndex = Grid.GetRow(border);
            //var rowProp = border.GetValue(Grid.RowProperty);
            //Debug.WriteLine(rowProp);

            //var app = Application.Current;
            //var expenseReport = (ExpenseReport)app.FindResource("ExpenseData");
            //expenseReport?.LineItems.RemoveAt(0);

            //Grid grid = FindVisualParent<Grid>(sender as Button);
            //grid.RowDefinitions.RemoveAt(2);

            //expensesItemsControl.ItemsSource;


            //Button senderButton = (Button)sender;
            //LineItem lineItem = null;

            //FrameworkElement currentObject = senderButton;
            //while (1 == 1)
            //{
            //    currentObject = currentObject.Parent as FrameworkElement;
            //    if (currentObject.GetType() == typeof(System.Windows.Controls.Primitives.Popup))
            //    {
            //        lineItem = (currentObject as System.Windows.Controls.Primitives.Popup).PlacementTarget as LineItem;
            //        break;
            //    }
            //}
            ////Remove from ObservableCollection<JobView>:
            //JobViews.Remove(lineItem);
        }

        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        private void computeButton_Click(object sender, RoutedEventArgs e)
        {
            // Populate crop list.
            List<Crop> crops = new List<Crop>();
            var app = Application.Current;
            var expenseReport = (ExpenseReport)app.FindResource("ExpenseData");
            if (expenseReport != null)
            {
                foreach (var item in expenseReport.LineItems)
                {
                    crops.Add(new Crop(item.Name, item.TimeToMature, item.TimeBetweenHarvests, item.Cost, item.Sell));
                }
            }

            // Calculate optimal schedule.
            int numDays = 28;
            PlantScheduleFactory scheduleFactory = new PlantScheduleFactory(numDays);

            PlantSchedule bestSchedule;
            var maxWealthMultiple = scheduleFactory.GetBestSchedule(crops, out bestSchedule);

            bool[] plantingDays = scheduleFactory.GetPlantingDays();

            // Debug print solution.
            Debug.WriteLine("\nmaxWealthMultiple: " + maxWealthMultiple + "\n");
            string scheduleStr = bestSchedule.ToString();

            // Populate display calender.
            calendar = new List<Week>();
            Week week = new Week();

            for (int day = 1; day <= numDays; ++day)
            {
                StringBuilder detailsBuilder = new StringBuilder();
                detailsBuilder.AppendLine("Day " + day + ":");
                detailsBuilder.AppendLine("-----");
                detailsBuilder.AppendLine();

                if (bestSchedule.GetCrop(day) != null)
                {
                    if (plantingDays[day])
                    {
                        detailsBuilder.Append(bestSchedule.GetCrop(day).name.ToUpper());
                    }
                    else
                    {
                        detailsBuilder.Append("(");
                        detailsBuilder.Append(bestSchedule.GetCrop(day).name);
                        detailsBuilder.Append(")");
                    }

                    detailsBuilder.AppendLine();
                }

                string details = detailsBuilder.ToString();

                switch (day % 7)
                {
                    case 1:
                        week.Monday = details;
                        break;
                    case 2:
                        week.Tuesday = details;
                        break;
                    case 3:
                        week.Wednesday = details;
                        break;
                    case 4:
                        week.Thursday = details;
                        break;
                    case 5:
                        week.Friday = details;
                        break;
                    case 6:
                        week.Saturday = details;
                        break;
                    case 0:
                        week.Sunday = details;
                        calendar.Add(week);
                        week = new Week();
                        break;

                    default:
                        throw new Exception("Math is wrong in the day case statement!");
                }

                if (day == numDays && day % 7 != 0)
                    calendar.Add(week);
            }

            // Populate display profit summary.
            profitSummary = "Following this schedule will increase your money by:  " + maxWealthMultiple.ToString("0.00") + "x";

            dgCalendar.ItemsSource = calendar;
            tbProfitSummary.Text = profitSummary;

        }
    }



    /// <summary>
    ///     EditBox is a custom cotrol that can switch between two modes:
    ///     editing and normal. When it is in editing mode, the content is
    ///     displayed in a TextBox that provides editing capbability. When
    ///     the EditBox is in normal, its content is displayed in a TextBlock
    ///     that is not editable.
    ///     This control is designed to be used in with a GridView View.
    /// </summary>
    public class EditBox : Control
    {
        #region Static Constructor

        /// <summary>
        ///     Static constructor
        /// </summary>
        static EditBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditBox),
                new FrameworkPropertyMetadata(typeof(EditBox)));
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Called when the tree for the EditBox has been generated.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var textBlock = GetTemplateChild("PART_TextBlockPart") as TextBlock;
            Debug.Assert(textBlock != null, "No TextBlock!");

            _textBox = new TextBox();
            _adorner = new EditBoxAdorner(textBlock, _textBox);
            var layer = AdornerLayer.GetAdornerLayer(textBlock);
            layer.Add(_adorner);

            _textBox.KeyDown += OnTextBoxKeyDown;
            _textBox.LostKeyboardFocus +=
                OnTextBoxLostKeyboardFocus;

            //Receive notification of the event to handle the column 
            //resize. 
            HookTemplateParentResizeEvent();

            //Capture the resize event to  handle ListView resize cases.
            HookItemsControlEvents();

            _listViewItem = GetDependencyObjectFromVisualTree(this,
                typeof(ListViewItem)) as ListViewItem;

            Debug.Assert(_listViewItem != null, "No ListViewItem found");
        }

        #endregion

        #region Protected Methods

        /// <summary>
        ///     If the ListViewItem that contains the EditBox is selected,
        ///     when the mouse pointer moves over the EditBox, the corresponding
        ///     MouseEnter event is the first of two events (MouseUp is the second)
        ///     that allow the EditBox to change to editing mode.
        /// </summary>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (!IsEditing && IsParentSelected)
            {
                _canBeEdit = true;
            }
        }

        /// <summary>
        ///     If the MouseLeave event occurs for an EditBox control that
        ///     is in normal mode, the mode cannot be changed to editing mode
        ///     until a MouseEnter event followed by a MouseUp event occurs.
        /// </summary>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            _isMouseWithinScope = false;
            _canBeEdit = false;
        }

        /// <summary>
        ///     An EditBox switches to editing mode when the MouseUp event occurs
        ///     for that EditBox and the following conditions are satisfied:
        ///     1. A MouseEnter event for the EditBox occurred before the
        ///     MouseUp event.
        ///     2. The mouse did not leave the EditBox between the
        ///     MouseEnter and MouseUp events.
        ///     3. The ListViewItem that contains the EditBox was selected
        ///     when the MouseEnter event occurred.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.ChangedButton == MouseButton.Right ||
                e.ChangedButton == MouseButton.Middle)
                return;

            if (!IsEditing)
            {
                if (!e.Handled && (_canBeEdit || _isMouseWithinScope))
                {
                    IsEditing = true;
                }

                //If the first MouseUp event selects the parent ListViewItem,
                //then the second MouseUp event puts the EditBox in editing 
                //mode
                if (IsParentSelected)
                    _isMouseWithinScope = true;
            }
        }

        #endregion

        #region Public Properties

        #region Value

        /// <summary>
        ///     ValueProperty DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(EditBox),
                new FrameworkPropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the value of the EditBox
        /// </summary>
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        #endregion

        #region IsEditing

        /// <summary>
        ///     IsEditingProperty DependencyProperty
        /// </summary>
        public static DependencyProperty IsEditingProperty =
            DependencyProperty.Register(
                "IsEditing",
                typeof(bool),
                typeof(EditBox),
                new FrameworkPropertyMetadata(false));

        /// <summary>
        ///     Returns true if the EditBox control is in editing mode.
        /// </summary>
        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            private set
            {
                SetValue(IsEditingProperty, value);
                _adorner.UpdateVisibilty(value);
            }
        }

        #endregion

        #region IsParentSelected

        /// <summary>
        ///     Gets whether the ListViewItem that contains the
        ///     EditBox is selected.
        /// </summary>
        private bool IsParentSelected
        {
            get
            {
                if (_listViewItem == null)
                    return false;
                return _listViewItem.IsSelected;
            }
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        ///     When an EditBox is in editing mode, pressing the ENTER or F2
        ///     keys switches the EditBox to normal mode.
        /// </summary>
        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (IsEditing && (e.Key == Key.Enter || e.Key == Key.F2))
            {
                IsEditing = false;
                _canBeEdit = false;
            }
        }

        /// <summary>
        ///     If an EditBox loses focus while it is in editing mode,
        ///     the EditBox mode switches to normal mode.
        /// </summary>
        private void OnTextBoxLostKeyboardFocus(object sender,
            KeyboardFocusChangedEventArgs e)
        {
            IsEditing = false;
        }

        /// <summary>
        ///     Sets IsEditing to false when the ListViewItem that contains an
        ///     EditBox changes its size
        /// </summary>
        private void OnCouldSwitchToNormalMode(object sender,
            RoutedEventArgs e)
        {
            IsEditing = false;
        }

        /// <summary>
        ///     Walk the visual tree to find the ItemsControl and
        ///     hook its some events on it.
        /// </summary>
        private void HookItemsControlEvents()
        {
            _itemsControl = GetDependencyObjectFromVisualTree(this,
                typeof(ItemsControl)) as ItemsControl;
            if (_itemsControl != null)
            {
                //Handle the Resize/ScrollChange/MouseWheel 
                //events to determine whether to switch to Normal mode
                _itemsControl.SizeChanged +=
                    OnCouldSwitchToNormalMode;
                _itemsControl.AddHandler(ScrollViewer.ScrollChangedEvent,
                    new RoutedEventHandler(OnScrollViewerChanged));
                _itemsControl.AddHandler(MouseWheelEvent,
                    new RoutedEventHandler(OnCouldSwitchToNormalMode), true);
            }
        }

        /// <summary>
        ///     If an EditBox is in editing mode and the content of a ListView is
        ///     scrolled, then the EditBox switches to normal mode.
        /// </summary>
        private void OnScrollViewerChanged(object sender, RoutedEventArgs args)
        {
            if (IsEditing && Mouse.PrimaryDevice.LeftButton ==
                MouseButtonState.Pressed)
            {
                IsEditing = false;
            }
        }

        /// <summary>
        ///     Walk visual tree to find the first DependencyObject
        ///     of the specific type.
        /// </summary>
        private DependencyObject
            GetDependencyObjectFromVisualTree(DependencyObject startObject,
                Type type)
        {
            //Walk the visual tree to get the parent(ItemsControl) 
            //of this control
            var parent = startObject;
            while (parent != null)
            {
                if (type.IsInstanceOfType(parent))
                    break;
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent;
        }

        /// <summary>
        ///     When the size of the column containing the EditBox changes
        ///     and the EditBox is in editing mode, switch the mode to normal mode
        /// </summary>
        private void HookTemplateParentResizeEvent()
        {
            var parent = TemplatedParent as FrameworkElement;
            if (parent != null)
            {
                parent.SizeChanged +=
                    OnCouldSwitchToNormalMode;
            }
        }

        #endregion

        #region private variable

        private EditBoxAdorner _adorner;
        //A TextBox in the visual tree
        private FrameworkElement _textBox;
        //Specifies whether an EditBox can switch to editing mode. 
        //Set to true if the ListViewItem that contains the EditBox is 
        //selected, when the mouse pointer moves over the EditBox
        private bool _canBeEdit;
        //Specifies whether an EditBox can switch to editing mode.
        //Set to true when the ListViewItem that contains the EditBox is 
        //selected when the mouse pointer moves over the EditBox.
        private bool _isMouseWithinScope;
        //The ListView control that contains the EditBox
        private ItemsControl _itemsControl;
        //The ListViewItem control that contains the EditBox
        private ListViewItem _listViewItem;

        #endregion
    }

    /// <summary>
    ///     An adorner class that contains a TextBox to provide editing capability
    ///     for an EditBox control. The editable TextBox resides in the
    ///     AdornerLayer. When the EditBox is in editing mode, the TextBox is given a size
    ///     it with desired size; otherwise, arrange it with size(0,0,0,0).
    /// </summary>
    internal sealed class EditBoxAdorner : Adorner
    {
        /// <summary>
        ///     Inialize the EditBoxAdorner.
        /// </summary>
        public EditBoxAdorner(UIElement adornedElement, UIElement adorningElement) : base(adornedElement)
        {
            var textBox = adorningElement as TextBox;
            if (textBox == null) throw new ArgumentException("adorningElement is not a TextBox.");
            _textBox = textBox;

            _visualChildren = new VisualCollection(this);

            BuildTextBox();
        }

        #region Public Methods

        /// <summary>
        ///     Specifies whether a TextBox is visible
        ///     when the IsEditing property changes.
        /// </summary>
        /// <param name="isVisible"></param>
        public void UpdateVisibilty(bool isVisible)
        {
            _isVisible = isVisible;
            InvalidateMeasure();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        ///     Override to measure elements.
        /// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            _textBox.IsEnabled = _isVisible;
            //if in editing mode, measure the space the adorner element 
            //should cover.
            if (_isVisible)
            {
                AdornedElement.Measure(constraint);
                _textBox.Measure(constraint);

                //since the adorner is to cover the EditBox, it should return 
                //the AdornedElement.Width, the extra 15 is to make it more 
                //clear.
                return new Size(AdornedElement.DesiredSize.Width + ExtraWidth,
                    _textBox.DesiredSize.Height);
            }
            return new Size(0, 0);
        }

        /// <summary>
        ///     override function to arrange elements.
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_isVisible)
            {
                _textBox.Arrange(new Rect(0, 0, finalSize.Width,
                    finalSize.Height));
            }
            else // if is not is editable mode, no need to show elements.
            {
                _textBox.Arrange(new Rect(0, 0, 0, 0));
            }
            return finalSize;
        }

        /// <summary>
        ///     override property to return infomation about visual tree.
        /// </summary>
        protected override int VisualChildrenCount => _visualChildren.Count;

        /// <summary>
        ///     override function to return infomation about visual tree.
        /// </summary>
        protected override Visual GetVisualChild(int index) => _visualChildren[index];

        #endregion

        #region Private Methods

        /// <summary>
        ///     Initialize necessary properties and hook necessary events on TextBox,
        ///     then add it into tree.
        /// </summary>
        private void BuildTextBox()
        {
            _canvas = new Canvas();
            _canvas.Children.Add(_textBox);
            _visualChildren.Add(_canvas);

            //Bind Text onto AdornedElement.
            var binding = new Binding("Text")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = AdornedElement
            };

            _textBox.SetBinding(TextBox.TextProperty, binding);

            // when layout finishes.
            _textBox.LayoutUpdated += OnTextBoxLayoutUpdated;
        }

        /// <summary>
        ///     When Layout finish, if in editable mode, update focus status
        ///     on TextBox.
        /// </summary>
        private void OnTextBoxLayoutUpdated(object sender, EventArgs e)
        {
            if (_isVisible) _textBox.Focus();
        }

        #endregion

        #region Private Variables

        // Visual children
        private readonly VisualCollection _visualChildren;

        // The TextBox that this adorner covers.
        private readonly TextBox _textBox;

        // Whether the EditBox is in editing mode which means the Adorner is visible.
        private bool _isVisible;

        // Canvas that contains the TextBox that provides the ability for it to 
        // display larger than the current size of the cell so that the entire
        // contents of the cell can be edited
        private Canvas _canvas;

        // Extra padding for the content when it is displayed in the TextBox
        private const double ExtraWidth = 15;

        #endregion
    }

}
