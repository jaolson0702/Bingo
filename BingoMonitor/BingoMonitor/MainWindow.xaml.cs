using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Tools;

namespace BingoMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CalledNumbers calledNumbersWindow = new CalledNumbers(string.Empty);
        private UncalledNumbers uncalledNumbersWindow = new UncalledNumbers(GetNumbers(false));
        private int previous;

        public MainWindow()
        {
            InitializeComponent();
            Title = "Bingo Monitor";
            Reset();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the game?", "Reset Game?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Reset();
        }

        private void callNumberButton_Click(object sender, RoutedEventArgs e)
        {
            string called = Bingo.Get.Call();
            calledNumberLabel.Content = called;
            if (previous != 0)
                previousCalledNumberLabel.Content = $"Previous Number: {Bingo.ToString(previous)}";
            if (Bingo.Get.UncalledNumbers.Count == 0) callNumberButton.IsEnabled = false;
            availabilityLabel.Content = $"Number of calls left: {Bingo.Get.UncalledNumbers.Count}";
            previous = int.Parse(called[2..]);
            calledNumbersWindow.messageLabel.Content = GetNumbers(true);
            uncalledNumbersWindow.messageLabel.Content = GetNumbers(false);
        }

        private void Reset()
        {
            Bingo.Get.Reset();
            previous = 0;
            calledNumberLabel.Content = "BINGO";
            availabilityLabel.Content = $"Number of calls left: {Bingo.Get.UncalledNumbers.Count}";
            calledNumbersWindow.messageLabel.Content = GetNumbers(true);
            uncalledNumbersWindow.messageLabel.Content = GetNumbers(false);
            previousCalledNumberLabel.Content = "";
            callNumberButton.IsEnabled = true;
        }

        private static string GetNumbers(bool called)
        {
            string bResult = "B: ", iResult = "I: ", nResult = "N: ", gResult = "G: ", oResult = "O: ";
            List<int> list = called ? Bingo.Get.CalledNumbers : Bingo.Get.UncalledNumbers;
            foreach (int item in list)
            {
                if (item != 0)
                {
                    if (Bingo.LetterOf(item) == 'B')
                        bResult += bResult == "B: " ? item.ToString() : $", {item}";
                    if (Bingo.LetterOf(item) == 'I')
                        iResult += iResult == "I: " ? item.ToString() : $", {item}";
                    if (Bingo.LetterOf(item) == 'N')
                        nResult += nResult == "N: " ? item.ToString() : $", {item}";
                    if (Bingo.LetterOf(item) == 'G')
                        gResult += gResult == "G: " ? item.ToString() : $", {item}";
                    if (Bingo.LetterOf(item) == 'O')
                        oResult += oResult == "O: " ? item.ToString() : $", {item}";
                }
            }
            string message = bResult != "B: " ? bResult + "\n\n" : string.Empty;
            message += iResult != "I: " ? iResult + "\n\n" : string.Empty;
            message += nResult != "N: " ? nResult + "\n\n" : string.Empty;
            message += gResult != "G: " ? gResult + "\n\n" : string.Empty;
            message += oResult != "O: " ? oResult + "\n\n" : string.Empty;
            string result = string.Empty;
            Predicate<List<int>> condition = called ? li => li.Count <= 75 : li => li.Count > 0;
            if (condition(list))
            {
                bool conditionResult = called ? message == string.Empty : list.Count == 75;
                result += conditionResult ? "No numbers have been called yet." : $"{(called ? "Called Numbers" : "Uncalled Numbers")}:\n\n" + message;
            }
            else
                result += "All numbers have been called.";
            return result;
        }

        private void calledNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            calledNumbersWindow.Close();
            calledNumbersWindow = new CalledNumbers(calledNumbersWindow.messageLabel.Content.ToString());
            calledNumbersWindow.Show();
        }

        private void uncalledNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            uncalledNumbersWindow.Close();
            uncalledNumbersWindow = new UncalledNumbers(uncalledNumbersWindow.messageLabel.Content.ToString());
            uncalledNumbersWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            calledNumbersWindow.Close();
            uncalledNumbersWindow.Close();
        }
    }
}