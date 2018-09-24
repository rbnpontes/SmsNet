using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmsNet.UI
{
	public enum AuthType
	{
		SignIn,
		SignUp
	}
	public partial class AuthForm : Form
	{
		private AuthType mType = AuthType.SignUp;
		public AuthType AuthType { get { return mType; } set
			{
				mType = value;
				UpdateLayout();
			}
		}
		public AuthForm()
		{
			InitializeComponent();
			UpdateLayout();
		}
		protected void UpdateLayout()
		{
			switch (mType)
			{
				case AuthType.SignIn:
					MakeSignInLayout();
					break;
				case AuthType.SignUp:
					MakeSignUpLayout();
					break;
			}
		}
	}
}
