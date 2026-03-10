using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SabaaAdminDB
{
    public partial class Form1 : Form
    {
        Panel panel;
        Label lblTitle;
        TextBox txtEmail;
        TextBox txtPassword;
        Button btnLogin;

        Timer timer;
        float angle = 0f;

        public Form1()
        {
            InitializeUI();
            InitializeAnimation();
        }

        // ================= UI =================
        private void InitializeUI()
        {
            // Form
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(14, 22, 33);
            this.DoubleBuffered = true;

            // Panel (Card)
            panel = new Panel();
            panel.Size = new Size(260, 260);
            panel.BackColor = Color.FromArgb(22, 32, 42);
            panel.Location = new Point(
                (this.ClientSize.Width - panel.Width) / 2,
                (this.ClientSize.Height - panel.Height) / 2
            );
            this.Controls.Add(panel);

            // Title
            lblTitle = new Label();
            lblTitle.Text = "Login";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(34, 230, 211);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 50;
            panel.Controls.Add(lblTitle);

            // Email
            txtEmail = new TextBox();
            txtEmail.Size = new Size(200, 30);
            txtEmail.Location = new Point(30, 70);
            txtEmail.BackColor = Color.FromArgb(14, 22, 33);
            txtEmail.ForeColor = Color.Gray;
            txtEmail.Text = "Email";
            panel.Controls.Add(txtEmail);

            txtEmail.GotFocus += (s, e) =>
            {
                if (txtEmail.Text == "Email")
                {
                    txtEmail.Text = "";
                    txtEmail.ForeColor = Color.White;
                }
            };
            txtEmail.LostFocus += (s, e) =>
            {
                if (txtEmail.Text == "")
                {
                    txtEmail.Text = "Email";
                    txtEmail.ForeColor = Color.Gray;
                }
            };

            // Password
            txtPassword = new TextBox();
            txtPassword.Size = new Size(200, 30);
            txtPassword.Location = new Point(30, 115);
            txtPassword.BackColor = Color.FromArgb(14, 22, 33);
            txtPassword.ForeColor = Color.Gray;
            txtPassword.Text = "Password";
            txtPassword.UseSystemPasswordChar = false;
            panel.Controls.Add(txtPassword);

            txtPassword.GotFocus += (s, e) =>
            {
                if (txtPassword.Text == "Password")
                {
                    txtPassword.Text = "";
                    txtPassword.ForeColor = Color.White;
                    txtPassword.UseSystemPasswordChar = true;
                }
            };
            txtPassword.LostFocus += (s, e) =>
            {
                if (txtPassword.Text == "")
                {
                    txtPassword.UseSystemPasswordChar = false;
                    txtPassword.Text = "Password";
                    txtPassword.ForeColor = Color.Gray;
                }
            };

            // Button
            btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Size = new Size(200, 35);
            btnLogin.Location = new Point(30, 175);
            btnLogin.BackColor = Color.FromArgb(34, 230, 211);
            btnLogin.ForeColor = Color.Black;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(30, 200, 190);
            btnLogin.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(20, 180, 170);
            panel.Controls.Add(btnLogin);
        }

        // ================= Animation =================
        private void InitializeAnimation()
        {
            timer = new Timer();
            timer.Interval = 30;
            timer.Tick += (s, e) =>
            {
                angle += 0.8f;
                this.Invalidate();
            };
            timer.Start();
        }

        // ================= Drawing =================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = this.ClientSize.Width / 2;
            int cy = this.ClientSize.Height / 2;
            int radius = 160;

            // ---- Glow (fake but close) ----
            for (int i = 6; i >= 1; i--)
            {
                using (Pen glow = new Pen(
                    Color.FromArgb(15, 34, 230, 211),
                    i * 2))
                {
                    glow.StartCap = LineCap.Round;
                    glow.EndCap = LineCap.Round;

                    g.DrawEllipse(
                        glow,
                        cx - radius,
                        cy - radius,
                        radius * 2,
                        radius * 2
                    );
                }
            }

            // ---- Segmented Circle ----
            using (Pen pen = new Pen(Color.FromArgb(34, 230, 211), 3))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                int segments = 36;
                float sweep = 6f;
                float gap = 4f;

                g.TranslateTransform(cx, cy);
                g.RotateTransform(angle);
                g.TranslateTransform(-cx, -cy);

                for (int i = 0; i < segments; i++)
                {
                    float start = i * (sweep + gap);
                    g.DrawArc(
                        pen,
                        cx - radius,
                        cy - radius,
                        radius * 2,
                        radius * 2,
                        start,
                        sweep
                    );
                }

                g.ResetTransform();
            }
        }
    }
}
