using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualPlus.Toolkit.Controls.Layout;

namespace Hawkchat.Client
{
    public partial class MainWindow : VisualForm
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

            Environment.Exit(0);

        }
    }
}
