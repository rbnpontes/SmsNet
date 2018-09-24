using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.UI
{
	public partial class AuthForm
	{
		private void MakeSignInLayout()
		{
			name_label.Visible	= false;
			name_field.Visible	= false;
			email_label.Visible = false;
			email_field.Visible = false;
		}
		private void MakeSignUpLayout()
		{
			name_label.Visible	= true;
			name_field.Visible	= true;
			email_label.Visible = true;
			email_field.Visible = true;
		}
	}
}
