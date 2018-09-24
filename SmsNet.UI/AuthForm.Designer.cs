namespace SmsNet.UI
{
	partial class AuthForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.login_label = new System.Windows.Forms.Label();
			this.login_field = new System.Windows.Forms.TextBox();
			this.pass_label = new System.Windows.Forms.Label();
			this.pass_field = new System.Windows.Forms.TextBox();
			this.name_label = new System.Windows.Forms.Label();
			this.name_field = new System.Windows.Forms.TextBox();
			this.email_label = new System.Windows.Forms.Label();
			this.email_field = new System.Windows.Forms.TextBox();
			this.submit_btn = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.login_label);
			this.panel1.Controls.Add(this.login_field);
			this.panel1.Controls.Add(this.pass_label);
			this.panel1.Controls.Add(this.pass_field);
			this.panel1.Controls.Add(this.name_label);
			this.panel1.Controls.Add(this.name_field);
			this.panel1.Controls.Add(this.email_label);
			this.panel1.Controls.Add(this.email_field);
			this.panel1.Controls.Add(this.submit_btn);
			this.panel1.Location = new System.Drawing.Point(12, 166);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(5);
			this.panel1.Size = new System.Drawing.Size(267, 245);
			this.panel1.TabIndex = 0;
			// 
			// login_label
			// 
			this.login_label.AutoSize = true;
			this.login_label.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.login_label.Location = new System.Drawing.Point(8, 5);
			this.login_label.Name = "login_label";
			this.login_label.Size = new System.Drawing.Size(33, 13);
			this.login_label.TabIndex = 5;
			this.login_label.Text = "Login";
			// 
			// login_field
			// 
			this.login_field.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.login_field.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.login_field.Location = new System.Drawing.Point(8, 21);
			this.login_field.Name = "login_field";
			this.login_field.Size = new System.Drawing.Size(254, 29);
			this.login_field.TabIndex = 6;
			// 
			// pass_label
			// 
			this.pass_label.AutoSize = true;
			this.pass_label.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pass_label.Location = new System.Drawing.Point(8, 53);
			this.pass_label.Name = "pass_label";
			this.pass_label.Size = new System.Drawing.Size(53, 13);
			this.pass_label.TabIndex = 7;
			this.pass_label.Text = "Password";
			// 
			// pass_field
			// 
			this.pass_field.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pass_field.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pass_field.Location = new System.Drawing.Point(8, 69);
			this.pass_field.Name = "pass_field";
			this.pass_field.PasswordChar = '●';
			this.pass_field.Size = new System.Drawing.Size(254, 29);
			this.pass_field.TabIndex = 8;
			// 
			// name_label
			// 
			this.name_label.AutoSize = true;
			this.name_label.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.name_label.Location = new System.Drawing.Point(8, 101);
			this.name_label.Name = "name_label";
			this.name_label.Size = new System.Drawing.Size(35, 13);
			this.name_label.TabIndex = 11;
			this.name_label.Text = "Name";
			// 
			// name_field
			// 
			this.name_field.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.name_field.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.name_field.Location = new System.Drawing.Point(8, 117);
			this.name_field.Name = "name_field";
			this.name_field.Size = new System.Drawing.Size(254, 29);
			this.name_field.TabIndex = 10;
			// 
			// email_label
			// 
			this.email_label.AutoSize = true;
			this.email_label.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.email_label.Location = new System.Drawing.Point(8, 149);
			this.email_label.Name = "email_label";
			this.email_label.Size = new System.Drawing.Size(35, 13);
			this.email_label.TabIndex = 13;
			this.email_label.Text = "E-mail";
			// 
			// email_field
			// 
			this.email_field.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.email_field.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.email_field.Location = new System.Drawing.Point(8, 165);
			this.email_field.Name = "email_field";
			this.email_field.Size = new System.Drawing.Size(254, 29);
			this.email_field.TabIndex = 12;
			// 
			// submit_btn
			// 
			this.submit_btn.Location = new System.Drawing.Point(5, 212);
			this.submit_btn.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
			this.submit_btn.Name = "submit_btn";
			this.submit_btn.Size = new System.Drawing.Size(262, 34);
			this.submit_btn.TabIndex = 9;
			this.submit_btn.Text = "Login";
			this.submit_btn.UseVisualStyleBackColor = true;
			// 
			// AuthForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(291, 423);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AuthForm";
			this.Text = "AuthForm";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel panel1;
		protected internal System.Windows.Forms.Label login_label;
		protected internal System.Windows.Forms.TextBox login_field;
		protected internal System.Windows.Forms.Label pass_label;
		protected internal System.Windows.Forms.TextBox pass_field;
		protected internal System.Windows.Forms.Button submit_btn;
		protected internal System.Windows.Forms.Label name_label;
		protected internal System.Windows.Forms.TextBox name_field;
		protected internal System.Windows.Forms.Label email_label;
		protected internal System.Windows.Forms.TextBox email_field;
	}
}