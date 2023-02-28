using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tools;

namespace BingoCard
{
    public class BingoGroup
    {
        private readonly MainWindow window;
        public readonly Button[] Buttons;
        public BingoGroup(MainWindow window, params Button[] buttons) =>
            (this.window, Buttons) = (window, buttons);
        public bool Contains(Button button) => Buttons.Contains(button);
        public int NumberSelected
        {
            get
            {
                int result = 0;
                Buttons.ForEach(button => button.DoIf(b => window.Selected(b), b => result++));
                return result;
            }
        }
        public bool AllSelected => NumberSelected == Buttons.Length;
        public bool AllButOne => NumberSelected == Buttons.Length - 1;
        public bool Selected
        {
            get
            {
                foreach (Button button in Buttons)
                    if (button.Background != MainWindow.BingoColor) return false;
                return true;
            }
        }
        public bool InAnyOther(Button button)
        {
            List<BingoGroup> groups = new();
            window.BingoGroups.ForEach(group => group.DoIf(g => g != this, g => groups.Add(g)));
            foreach (BingoGroup group in groups) if (group.Contains(button) && group.Selected) return true;
            return false;
        }
        public void Select()
        {
            if (AllSelected) Buttons.ForEach(button => button.Background = MainWindow.BingoColor);
        }
        public void Deselect()
        {
            if (AllButOne)
            {
                foreach (Button button in Buttons)
                {
                    if (!InAnyOther(button))
                        button.Background = window.Selected(button) ?
                            MainWindow.SelectColor : MainWindow.DeselectColor;
                }
            }
        }
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public static bool operator ==(BingoGroup a, BingoGroup b)
        {
            for (int i = 0; i < a.Buttons.Length; i++)
                if (a.Buttons[i] != b.Buttons[i]) return false;
            return true;
        }
        public static bool operator !=(BingoGroup a, BingoGroup b) => !(a == b);
    }
}