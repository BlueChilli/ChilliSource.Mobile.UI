using System.Windows.Input;
using ChilliSource.Mobile.Core;
using Xamarin.Forms;

namespace Examples
{
    public class StyledNavigationBarExamplePageViewModel : ObservableObject {

        public StyledNavigationBarExamplePageViewModel(string title, string subTitle) 
        {
            this.Title = title;
            this.SubTitle = subTitle;

            this.LeftButtonText = this.LeftButtonVisibilityText;
            this.RightButtonText = this.RightButtonVisibilityText;

            LeftButtonVisibilityCommand = new Command(() =>
            {
                IsLeftToolBarItemVisible = !IsLeftToolBarItemVisible;
                LeftButtonText = LeftButtonVisibilityText;
            });

            RightButtonVisibilityCommand = new Command(() =>
            {
                IsRightToolBarItemVisible = !IsRightToolBarItemVisible;
                RightButtonText = RightButtonVisibilityText;
            });

            ChangeTitleCommand = new Command(() =>
            {
                Title = "Title changed";
            });

            ChangeSubTitleCommand = new Command(() =>
            {
                SubTitle = "SubTitle changed";
            });
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { this.SetProperty(ref _title, value); }
        }

        private string _subtitle;
        public string SubTitle 
        {
            get { return _subtitle; }
            set { this.SetProperty(ref _subtitle, value);}
        }


        bool _leftVisible = true;
        bool _rightVisible = true;

        public bool IsLeftToolBarItemVisible
        {
            get { return _leftVisible; }
            set { SetProperty(ref _leftVisible, value); }
        }

        public bool IsRightToolBarItemVisible
        {
            get { return _rightVisible; }
            set { SetProperty(ref _rightVisible, value); }
        }

        private string _leftButtonText;
        public string LeftButtonText 
        {
            get { return _leftButtonText; }
            set { this.SetProperty(ref _leftButtonText, value); }
        }

        private string _rightButtonText;
        public string RightButtonText
        {
            get { return _rightButtonText; }
            set { this.SetProperty(ref _rightButtonText, value); }
        }

        public string LeftButtonVisibilityText => _leftVisible ? "Hide Left Toolbar" : "Show Left Toolbar";

        public string RightButtonVisibilityText => _rightVisible ? "Hide Right Toolbar" : "Show Right Toolbar";

        public ICommand LeftButtonVisibilityCommand { get; set; }

        public ICommand RightButtonVisibilityCommand { get; set; }

        public ICommand ChangeTitleCommand { get; set; }

        public ICommand ChangeSubTitleCommand { get; set; }
    }
}