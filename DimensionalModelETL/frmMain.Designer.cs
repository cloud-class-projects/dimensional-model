namespace DimensionalModelETL
{
    partial class frmMain
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
            this.btnASTHistory = new System.Windows.Forms.Button();
            this.btnASTCurrent = new System.Windows.Forms.Button();
            this.btnBlobHistory = new System.Windows.Forms.Button();
            this.btnBlobDistinct = new System.Windows.Forms.Button();
            this.btnMapAST = new System.Windows.Forms.Button();
            this.btnMapBlob = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnASTHistory
            // 
            this.btnASTHistory.Enabled = false;
            this.btnASTHistory.Location = new System.Drawing.Point(9, 18);
            this.btnASTHistory.Margin = new System.Windows.Forms.Padding(2);
            this.btnASTHistory.Name = "btnASTHistory";
            this.btnASTHistory.Size = new System.Drawing.Size(142, 30);
            this.btnASTHistory.TabIndex = 0;
            this.btnASTHistory.Text = "Load AST History";
            this.btnASTHistory.UseVisualStyleBackColor = true;
            // 
            // btnASTCurrent
            // 
            this.btnASTCurrent.Location = new System.Drawing.Point(9, 89);
            this.btnASTCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.btnASTCurrent.Name = "btnASTCurrent";
            this.btnASTCurrent.Size = new System.Drawing.Size(142, 30);
            this.btnASTCurrent.TabIndex = 1;
            this.btnASTCurrent.Text = "Load AST Current";
            this.btnASTCurrent.UseVisualStyleBackColor = true;
            // 
            // btnBlobHistory
            // 
            this.btnBlobHistory.Enabled = false;
            this.btnBlobHistory.Location = new System.Drawing.Point(9, 52);
            this.btnBlobHistory.Margin = new System.Windows.Forms.Padding(2);
            this.btnBlobHistory.Name = "btnBlobHistory";
            this.btnBlobHistory.Size = new System.Drawing.Size(142, 30);
            this.btnBlobHistory.TabIndex = 2;
            this.btnBlobHistory.Text = "Load Blob History";
            this.btnBlobHistory.UseVisualStyleBackColor = true;
            // 
            // btnBlobCurrent
            // 
            this.btnBlobDistinct.Enabled = false;
            this.btnBlobDistinct.Location = new System.Drawing.Point(9, 123);
            this.btnBlobDistinct.Margin = new System.Windows.Forms.Padding(2);
            this.btnBlobDistinct.Name = "btnBlobCurrent";
            this.btnBlobDistinct.Size = new System.Drawing.Size(142, 30);
            this.btnBlobDistinct.TabIndex = 3;
            this.btnBlobDistinct.Text = "Load Blob Distinct Product";
            this.btnBlobDistinct.UseVisualStyleBackColor = true;
            // 
            // btnMapAST
            // 
            this.btnMapAST.Location = new System.Drawing.Point(9, 158);
            this.btnMapAST.Margin = new System.Windows.Forms.Padding(2);
            this.btnMapAST.Name = "btnMapAST";
            this.btnMapAST.Size = new System.Drawing.Size(142, 30);
            this.btnMapAST.TabIndex = 4;
            this.btnMapAST.Text = "Create Product from AST";
            this.btnMapAST.UseVisualStyleBackColor = true;
            // 
            // btnMapBlob
            // 
            this.btnMapBlob.Enabled = false;
            this.btnMapBlob.Location = new System.Drawing.Point(9, 193);
            this.btnMapBlob.Margin = new System.Windows.Forms.Padding(2);
            this.btnMapBlob.Name = "btnMapBlob";
            this.btnMapBlob.Size = new System.Drawing.Size(142, 30);
            this.btnMapBlob.TabIndex = 5;
            this.btnMapBlob.Text = "Deploy HDInsight Cluster";
            this.btnMapBlob.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Loads 100 days of history.  Dev only.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Loads 100 days of history.  Dev only.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(284, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Loads Current Product Snapshot into Azure Table Storage.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 131);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(283, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Loads distinct list of Product Partition Keys into Azure Blob.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(155, 166);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(259, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Creates Product dimension from Azure Table Storage.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 201);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Creates a HDInsight Instance";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 460);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMapBlob);
            this.Controls.Add(this.btnMapAST);
            this.Controls.Add(this.btnBlobDistinct);
            this.Controls.Add(this.btnBlobHistory);
            this.Controls.Add(this.btnASTCurrent);
            this.Controls.Add(this.btnASTHistory);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.Text = "Azure Dimensional Model Control Panel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnASTHistory;
        private System.Windows.Forms.Button btnASTCurrent;
        private System.Windows.Forms.Button btnBlobHistory;
        private System.Windows.Forms.Button btnBlobDistinct;
        private System.Windows.Forms.Button btnMapAST;
        private System.Windows.Forms.Button btnMapBlob;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

