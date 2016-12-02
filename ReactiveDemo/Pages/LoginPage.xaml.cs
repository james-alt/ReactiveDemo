using ReactiveUI;
using ReactiveDemo.Core;
using Xamarin.Forms;
using System;
using System.Reactive.Linq;

namespace ReactiveDemo
{
	public partial class LoginPage 
		: ContentPage, IViewFor<LoginViewModel>	
	{
		public LoginPage()
		{
			InitializeComponent();
			ViewModel = new LoginViewModel();

			this.Bind(ViewModel, vm => vm.Username, v => v.Username.Text);
			this.Bind(ViewModel, vm => vm.Password, v => v.Password.Text);
			this.BindCommand(ViewModel, vm => vm.Login, v => v.Login);

			this.WhenAnyValue(x => x.ViewModel.IsLoading)
				.Subscribe(isBusy =>
					{
						Username.IsEnabled = !isBusy;
						Password.IsEnabled = !isBusy;
						LoadingIndicator.IsRunning = isBusy;
					});
		}

		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create(
				nameof(ViewModel),
				typeof(LoginViewModel),
				typeof(LoginPage),
				null, 
				BindingMode.OneWay);

		public LoginViewModel ViewModel
		{
			get { return (LoginViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (LoginViewModel)value; }
		}
	}
}
