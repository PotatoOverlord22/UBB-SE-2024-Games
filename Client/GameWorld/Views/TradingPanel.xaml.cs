using System.Windows.Controls;
using GameWorld.Entities;

namespace GameWorld.Views
{
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
