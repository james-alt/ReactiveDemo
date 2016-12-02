using System.Threading.Tasks;
using ReactiveUI;

namespace ReactiveDemo.Core
{
	public class LoginViewModel
		: ReactiveObject
	{
		public LoginViewModel()
		{
			var canLogin = this.WhenAnyValue(
				p => p.Username,
				p => p.Password,
				(usernameValue, passwordValue) =>
				{
					return !string.IsNullOrWhiteSpace(usernameValue)
								  && !string.IsNullOrWhiteSpace(passwordValue);
				});

			Login = ReactiveCommand.CreateFromTask(
				async () =>
				{
					await Task.Delay(4000).ConfigureAwait(false);
				}, 
				canLogin);

			Login.IsExecuting.ToProperty(this, p => p.IsLoading, out isLoading);
		}

		#region Commands

		public ReactiveCommand Login { get; protected set; }

		#endregion

		#region Properties

		string username;
		public string Username
		{
			get { return username; }
			set { this.RaiseAndSetIfChanged(ref username, value); }
		}

		string password;
		public string Password
		{
			get { return password; }
			set { this.RaiseAndSetIfChanged(ref password, value); }
		}

		readonly ObservableAsPropertyHelper<bool> isLoading;
		public bool IsLoading
		{
			get { return isLoading.Value; }
		}

		#endregion
	}
}
