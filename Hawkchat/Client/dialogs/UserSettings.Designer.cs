namespace Hawkchat.Client
{
    partial class UserSettings
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
            VisualPlus.Structure.Border border1 = new VisualPlus.Structure.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSettings));
            this.visualTabControl1 = new VisualPlus.Toolkit.Controls.Navigation.VisualTabControl();
            this.generalTab = new System.Windows.Forms.TabPage();
            this.notificationsTab = new System.Windows.Forms.TabPage();
            this.visualTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // visualTabControl1
            // 
            this.visualTabControl1.ArrowSelectorVisible = true;
            this.visualTabControl1.ArrowSpacing = 10;
            this.visualTabControl1.ArrowThickness = 5;
            this.visualTabControl1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.visualTabControl1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.visualTabControl1.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.visualTabControl1.Border.HoverVisible = true;
            this.visualTabControl1.Border.Rounding = 6;
            this.visualTabControl1.Border.Thickness = 1;
            this.visualTabControl1.Border.Type = VisualPlus.Enumerators.ShapeType.Rounded;
            this.visualTabControl1.Border.Visible = true;
            this.visualTabControl1.Controls.Add(this.generalTab);
            this.visualTabControl1.Controls.Add(this.notificationsTab);
            this.visualTabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.visualTabControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.visualTabControl1.ItemSize = new System.Drawing.Size(100, 25);
            this.visualTabControl1.LineAlignment = System.Drawing.StringAlignment.Center;
            this.visualTabControl1.Location = new System.Drawing.Point(3, 32);
            this.visualTabControl1.MinimumSize = new System.Drawing.Size(144, 85);
            this.visualTabControl1.Name = "visualTabControl1";
            this.visualTabControl1.SelectedIndex = 0;
            this.visualTabControl1.SelectorAlignment = System.Windows.Forms.TabAlignment.Top;
            this.visualTabControl1.SelectorAlignment2 = System.Windows.Forms.TabAlignment.Bottom;
            this.visualTabControl1.SelectorThickness = 4;
            this.visualTabControl1.SelectorVisible = false;
            this.visualTabControl1.SelectorVisible2 = false;
            this.visualTabControl1.Separator = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(222)))), ((int)(((byte)(220)))));
            this.visualTabControl1.SeparatorSpacing = 2;
            this.visualTabControl1.SeparatorThickness = 2F;
            this.visualTabControl1.Size = new System.Drawing.Size(592, 292);
            this.visualTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.visualTabControl1.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualTabControl1.TabHover = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(38)))));
            this.visualTabControl1.TabIndex = 0;
            this.visualTabControl1.TabMenu = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
            this.visualTabControl1.TabNormal = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
            this.visualTabControl1.TabPageBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.visualTabControl1.TabPageBorder.Rounding = 6;
            this.visualTabControl1.TabPageBorder.Thickness = 1;
            this.visualTabControl1.TabPageBorder.Type = VisualPlus.Enumerators.ShapeType.Rounded;
            this.visualTabControl1.TabPageBorder.Visible = true;
            this.visualTabControl1.TabSelected = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(76)))), ((int)(((byte)(88)))));
            this.visualTabControl1.TabSelector = System.Drawing.Color.Green;
            this.visualTabControl1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualTabControl1.TextNormal = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(181)))), ((int)(((byte)(187)))));
            this.visualTabControl1.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.visualTabControl1.TextSelected = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            // 
            // generalTab
            // 
            this.generalTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.generalTab.Location = new System.Drawing.Point(4, 29);
            this.generalTab.Name = "generalTab";
            this.generalTab.Padding = new System.Windows.Forms.Padding(3);
            this.generalTab.Size = new System.Drawing.Size(584, 259);
            this.generalTab.TabIndex = 0;
            this.generalTab.Text = "General";
            // 
            // notificationsTab
            // 
            this.notificationsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.notificationsTab.Location = new System.Drawing.Point(4, 29);
            this.notificationsTab.Name = "notificationsTab";
            this.notificationsTab.Padding = new System.Windows.Forms.Padding(3);
            this.notificationsTab.Size = new System.Drawing.Size(584, 259);
            this.notificationsTab.TabIndex = 1;
            this.notificationsTab.Text = "Notifications";
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.Border.HoverVisible = true;
            this.Border.Rounding = 6;
            this.Border.Thickness = 3;
            this.Border.Type = VisualPlus.Enumerators.ShapeType.Rectangle;
            this.Border.Visible = true;
            this.ClientSize = new System.Drawing.Size(597, 324);
            this.Controls.Add(this.visualTabControl1);
            border1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            border1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            border1.HoverVisible = false;
            border1.Rounding = 6;
            border1.Thickness = 1;
            border1.Type = VisualPlus.Enumerators.ShapeType.Rounded;
            border1.Visible = false;
            this.Image.Border = border1;
            this.Image.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image")));
            this.Image.Point = new System.Drawing.Point(5, 7);
            this.Image.Size = new System.Drawing.Size(16, 16);
            this.Image.Visible = true;
            this.Name = "UserSettings";
            this.Text = "Settings";
            this.visualTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualPlus.Toolkit.Controls.Navigation.VisualTabControl visualTabControl1;
        private System.Windows.Forms.TabPage generalTab;
        private System.Windows.Forms.TabPage notificationsTab;
    }
}