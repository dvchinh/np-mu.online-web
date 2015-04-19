namespace LifeSpringFixUPC
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.lSCVFile = new System.Windows.Forms.Label();
            this.tConnection = new System.Windows.Forms.TextBox();
            this.lResult = new System.Windows.Forms.ListView();
            this.Info = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lStatus = new System.Windows.Forms.Label();
            this.tSQL = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.lCustomerFilename = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tInserted = new System.Windows.Forms.Label();
            this.tDuplicated = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tExpired = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cRequireSecurity = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Import Coupons";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lSCVFile
            // 
            this.lSCVFile.AutoSize = true;
            this.lSCVFile.Location = new System.Drawing.Point(38, 38);
            this.lSCVFile.Name = "lSCVFile";
            this.lSCVFile.Size = new System.Drawing.Size(35, 13);
            this.lSCVFile.TabIndex = 1;
            this.lSCVFile.Text = "label1";
            // 
            // tConnection
            // 
            this.tConnection.Location = new System.Drawing.Point(21, 69);
            this.tConnection.Multiline = true;
            this.tConnection.Name = "tConnection";
            this.tConnection.Size = new System.Drawing.Size(421, 80);
            this.tConnection.TabIndex = 2;
            this.tConnection.Text = resources.GetString("tConnection.Text");
            // 
            // lResult
            // 
            this.lResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Info});
            this.lResult.Location = new System.Drawing.Point(16, 188);
            this.lResult.Name = "lResult";
            this.lResult.Size = new System.Drawing.Size(426, 358);
            this.lResult.TabIndex = 3;
            this.lResult.UseCompatibleStateImageBehavior = false;
            this.lResult.View = System.Windows.Forms.View.Details;
            this.lResult.SelectedIndexChanged += new System.EventHandler(this.lResult_SelectedIndexChanged);
            // 
            // Info
            // 
            this.Info.Text = "Info";
            this.Info.Width = 412;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(153, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Select CSV";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(18, 162);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(0, 13);
            this.lStatus.TabIndex = 5;
            // 
            // tSQL
            // 
            this.tSQL.Location = new System.Drawing.Point(448, 12);
            this.tSQL.Multiline = true;
            this.tSQL.Name = "tSQL";
            this.tSQL.Size = new System.Drawing.Size(661, 534);
            this.tSQL.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(234, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Select Customer CSV";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lCustomerFilename
            // 
            this.lCustomerFilename.AutoSize = true;
            this.lCustomerFilename.Location = new System.Drawing.Point(38, 53);
            this.lCustomerFilename.Name = "lCustomerFilename";
            this.lCustomerFilename.Size = new System.Drawing.Size(35, 13);
            this.lCustomerFilename.TabIndex = 8;
            this.lCustomerFilename.Text = "label1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(319, 157);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "Import Customer";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(461, 551);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Inserted";
            // 
            // tInserted
            // 
            this.tInserted.AutoSize = true;
            this.tInserted.Location = new System.Drawing.Point(529, 551);
            this.tInserted.Name = "tInserted";
            this.tInserted.Size = new System.Drawing.Size(0, 13);
            this.tInserted.TabIndex = 11;
            // 
            // tDuplicated
            // 
            this.tDuplicated.AutoSize = true;
            this.tDuplicated.Location = new System.Drawing.Point(728, 551);
            this.tDuplicated.Name = "tDuplicated";
            this.tDuplicated.Size = new System.Drawing.Size(0, 13);
            this.tDuplicated.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(660, 551);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Duplicated";
            // 
            // tExpired
            // 
            this.tExpired.AutoSize = true;
            this.tExpired.Location = new System.Drawing.Point(896, 551);
            this.tExpired.Name = "tExpired";
            this.tExpired.Size = new System.Drawing.Size(0, 13);
            this.tExpired.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(828, 551);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Expired";
            // 
            // cRequireSecurity
            // 
            this.cRequireSecurity.AutoSize = true;
            this.cRequireSecurity.Location = new System.Drawing.Point(338, 46);
            this.cRequireSecurity.Name = "cRequireSecurity";
            this.cRequireSecurity.Size = new System.Drawing.Size(104, 17);
            this.cRequireSecurity.TabIndex = 16;
            this.cRequireSecurity.Text = "Require Secuirty";
            this.cRequireSecurity.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 573);
            this.Controls.Add(this.cRequireSecurity);
            this.Controls.Add(this.tExpired);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tDuplicated);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tInserted);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lCustomerFilename);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tSQL);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lResult);
            this.Controls.Add(this.tConnection);
            this.Controls.Add(this.lSCVFile);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lSCVFile;
        private System.Windows.Forms.TextBox tConnection;
        private System.Windows.Forms.ListView lResult;
        private System.Windows.Forms.ColumnHeader Info;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.TextBox tSQL;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lCustomerFilename;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label tInserted;
        private System.Windows.Forms.Label tDuplicated;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label tExpired;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cRequireSecurity;
    }
}

