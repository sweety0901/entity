using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSewApp
{
    public static class Windows
    {
        public static void ShowWindow(dynamic thisForm, dynamic nextForm)
        {
            thisForm.Hide();
            nextForm.Show();
            thisForm.Close();
        }
    }
}
