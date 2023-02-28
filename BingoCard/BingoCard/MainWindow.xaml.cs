using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tools;

namespace BingoCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly Brush SelectColor = Brushes.Gray, DeselectColor = Brushes.LightGray,
            BingoColor = Brushes.Yellow;
        public readonly List<BingoGroup> BingoGroups = new();
        public Dictionary<Button, bool> GridButtons { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Title = "Bingo Card";
            BingoGroups.Add(new(this, grid11, grid12, grid13, grid14, grid15));
            BingoGroups.Add(new(this, grid21, grid22, grid23, grid24, grid25));
            BingoGroups.Add(new(this, grid31, grid32, grid33, grid34, grid35));
            BingoGroups.Add(new(this, grid41, grid42, grid43, grid44, grid45));
            BingoGroups.Add(new(this, grid51, grid52, grid53, grid54, grid55));
            BingoGroups.Add(new(this, grid11, grid21, grid31, grid41, grid51));
            BingoGroups.Add(new(this, grid12, grid22, grid32, grid42, grid52));
            BingoGroups.Add(new(this, grid13, grid23, grid33, grid43, grid53));
            BingoGroups.Add(new(this, grid14, grid24, grid34, grid44, grid54));
            BingoGroups.Add(new(this, grid15, grid25, grid35, grid45, grid55));
            BingoGroups.Add(new(this, grid11, grid22, grid33, grid44, grid55));
            BingoGroups.Add(new(this, grid15, grid24, grid33, grid42, grid51));
            ClearCard();
            PopulateButtons();
            grid33.Background = SelectColor;
        }

        private void PopulateButtons()
        {
            GenerateNumbers(1, 15, grid11, grid12, grid13, grid14, grid15);
            GenerateNumbers(16, 30, grid21, grid22, grid23, grid24, grid25);
            GenerateNumbers(31, 45, grid31, grid32, grid34, grid35);
            GenerateNumbers(46, 60, grid41, grid42, grid43, grid44, grid45);
            GenerateNumbers(61, 75, grid51, grid52, grid53, grid54, grid55);
        }

        private static void GenerateNumbers(int low, int high, params Button[] buttons)
        {
            List<int> numbers = new();
            Random random = new();
            buttons.ForEach(button =>
            {
                int number;
                do
                {
                    number = random.Next(low, high + 1);
                } while (numbers.Contains(number));
                numbers.Add(number);
            });
            numbers.ForEachIndex(index => buttons[index].Content = numbers[index].ToString());
        }

        private void ClearCard()
        {
            GridButtons = new();
            GridButtons.Add(grid11, false);
            GridButtons.Add(grid12, false);
            GridButtons.Add(grid13, false);
            GridButtons.Add(grid14, false);
            GridButtons.Add(grid15, false);
            GridButtons.Add(grid21, false);
            GridButtons.Add(grid22, false);
            GridButtons.Add(grid23, false);
            GridButtons.Add(grid24, false);
            GridButtons.Add(grid25, false);
            GridButtons.Add(grid31, false);
            GridButtons.Add(grid32, false);
            GridButtons.Add(grid33, true);
            GridButtons.Add(grid34, false);
            GridButtons.Add(grid35, false);
            GridButtons.Add(grid41, false);
            GridButtons.Add(grid42, false);
            GridButtons.Add(grid43, false);
            GridButtons.Add(grid44, false);
            GridButtons.Add(grid45, false);
            GridButtons.Add(grid51, false);
            GridButtons.Add(grid52, false);
            GridButtons.Add(grid53, false);
            GridButtons.Add(grid54, false);
            GridButtons.Add(grid55, false);
            GridButtons.Keys.ForEach(gridButton => gridButton.Background = gridButton == grid33 ?
            SelectColor : DeselectColor);
        }

        public bool Selected(Button button) => GridButtons[button];

        private void Select(Button button)
        {
            if (button.Background == SelectColor || button.Background == BingoColor)
                button.Background = DeselectColor;
            else
                button.Background = SelectColor;
            GridButtons[button] = !GridButtons[button];
            if (GridButtons[button])
                BingoGroups.ForEach(bingoGroup => bingoGroup.Select());
            else
            {
                foreach (BingoGroup group in BingoGroups)
                    if (group.Contains(button)) group.Deselect();
            }
        }

        private void grid11_Click(object sender, RoutedEventArgs e) => Select(grid11);

        private void grid12_Click(object sender, RoutedEventArgs e) => Select(grid12);

        private void grid13_Click(object sender, RoutedEventArgs e) => Select(grid13);

        private void grid14_Click(object sender, RoutedEventArgs e) => Select(grid14);

        private void grid15_Click(object sender, RoutedEventArgs e) => Select(grid15);

        private void grid21_Click(object sender, RoutedEventArgs e) => Select(grid21);

        private void grid22_Click(object sender, RoutedEventArgs e) => Select(grid22);

        private void grid23_Click(object sender, RoutedEventArgs e) => Select(grid23);

        private void grid24_Click(object sender, RoutedEventArgs e) => Select(grid24);

        private void grid25_Click(object sender, RoutedEventArgs e) => Select(grid25);

        private void grid31_Click(object sender, RoutedEventArgs e) => Select(grid31);

        private void grid32_Click(object sender, RoutedEventArgs e) => Select(grid32);

        private void grid34_Click(object sender, RoutedEventArgs e) => Select(grid34);

        private void grid35_Click(object sender, RoutedEventArgs e) => Select(grid35);

        private void grid41_Click(object sender, RoutedEventArgs e) => Select(grid41);

        private void grid42_Click(object sender, RoutedEventArgs e) => Select(grid42);

        private void grid43_Click(object sender, RoutedEventArgs e) => Select(grid43);

        private void grid44_Click(object sender, RoutedEventArgs e) => Select(grid44);

        private void grid45_Click(object sender, RoutedEventArgs e) => Select(grid45);

        private void grid51_Click(object sender, RoutedEventArgs e) => Select(grid51);

        private void grid52_Click(object sender, RoutedEventArgs e) => Select(grid52);

        private void grid53_Click(object sender, RoutedEventArgs e) => Select(grid53);

        private void grid54_Click(object sender, RoutedEventArgs e) => Select(grid54);

        private void grid55_Click(object sender, RoutedEventArgs e) => Select(grid55);

        private void fourCornersCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            BingoGroups.Add(new(this, grid11, grid15, grid51, grid55));
            foreach (Button button in BingoGroups[^1].Buttons)
            {
                Select(button);
                Select(button);
            }
        }

        private void fourCornersCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Button[] buttons = BingoGroups[^1].Buttons;
            BingoGroups.RemoveAt(BingoGroups.Count - 1);
            foreach (Button button in buttons)
            {
                Select(button);
                Select(button);
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear your card?", "Clear Card?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                ClearCard();
        }

        private void newCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want a new card?", "New Card?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ClearCard();
                PopulateButtons();
            }
        }
    }
}
