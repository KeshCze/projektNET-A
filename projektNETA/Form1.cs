﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektNETA
{
    public partial class Form1 : Form
    {
        Currencies currencies;          
        public Form1()
        {
            InitializeComponent();
            currencies = new Currencies();
            currencies.PluginAdded += Currencies_PluginAdded;
        }

        private void Currencies_PluginAdded()
        {
            textBox1.Text = string.Empty;
            foreach (var item in currencies.CurrenciesList)
            {
                textBox1.Text += item.Name + "---" + item.value + "\n";
            }
        }
    }
}