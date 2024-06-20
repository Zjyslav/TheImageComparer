using System.Windows.Controls;
using TheImageComparer.UI.ViewModels;

namespace TheImageComparer.UI.Views
{
    public partial class VoteStatsView : UserControl, IView
    {
        private readonly VoteStatsViewModel _viewModel;

        public VoteStatsView(VoteStatsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
