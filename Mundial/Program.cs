﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mundial
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var intro = new Intro();
            intro.ShowDialog();
            if (intro.DialogResult == DialogResult.OK) 
            {
                Application.Run(new Form1());
            }
        }
    }
}
