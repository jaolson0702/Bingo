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
using System.Windows.Shapes;

namespace BingoMonitor
{
    /// <summary>
    /// Interaction logic for CalledNumbers.xaml
    /// </summary>
    public partial class CalledNumbers : Window
    {
        public CalledNumbers(string message)
        {
            InitializeComponent();
            messageLabel.Content = message;
            Title = "Called Numbers";
        }
    }
}