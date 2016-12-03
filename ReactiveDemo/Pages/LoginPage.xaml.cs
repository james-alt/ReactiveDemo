using ReactiveUI;
using ReactiveDemo.Core;
using Xamarin.Forms;
using System;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using ReactiveUI.XamForms;

namespace ReactiveDemo
{
	public partial class LoginPage 
		: ReactiveContentPage<LoginViewModel>
	{
		public LoginPage()
		{
			InitializeComponent();
			ViewModel = new LoginViewModel();
			this.BindingContext = ViewModel;

			this.WhenActivated ((disposables) => {
				this.Bind (ViewModel, vm => vm.Username, v => v.Username.Text).DisposeWith (disposables);
				this.Bind (ViewModel, vm => vm.Password, v => v.Password.Text).DisposeWith (disposables);
				this.BindCommand (ViewModel, vm => vm.Login, v => v.Login).DisposeWith (disposables);

				this.WhenAnyValue (x => x.ViewModel.IsLoading)
					.Subscribe (isBusy => {
						Username.IsEnabled = !isBusy;
						Password.IsEnabled = !isBusy;
						LoadingIndicator.IsRunning = isBusy;
					})
					.DisposeWith (disposables);
			});

		}
	}
}
