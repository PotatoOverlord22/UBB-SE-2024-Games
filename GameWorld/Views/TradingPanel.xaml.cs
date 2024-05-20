using System.Windows.Controls;
using GameWorld.Entities;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for TradingPanel.xaml
    /// </summary>
    public partial class TradingPanel : StackPanel
    {
        public Trade Trade;

        public TradingPanel(Trade trade)
        {
            this.Trade = trade;
            InitializeComponent();
        }
    }
}
