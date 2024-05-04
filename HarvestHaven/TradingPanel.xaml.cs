using HarvestHaven.Entities;
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

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for TradingPanel.xaml
    /// </summary>
    public partial class TradingPanel : StackPanel
    {
        public Trade trade;

        public TradingPanel(Trade trade)
        {
            this.trade = trade;
            InitializeComponent();
        }

    }
}
