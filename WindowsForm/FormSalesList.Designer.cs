﻿
namespace WindowsForm
{
    partial class FormSalesList
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
            this.dataGridViewSaleList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleList)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSaleList
            // 
            this.dataGridViewSaleList.AllowUserToAddRows = false;
            this.dataGridViewSaleList.AllowUserToDeleteRows = false;
            this.dataGridViewSaleList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewSaleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSaleList.Location = new System.Drawing.Point(255, 38);
            this.dataGridViewSaleList.Name = "dataGridViewSaleList";
            this.dataGridViewSaleList.ReadOnly = true;
            this.dataGridViewSaleList.RowTemplate.Height = 25;
            this.dataGridViewSaleList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSaleList.Size = new System.Drawing.Size(793, 475);
            this.dataGridViewSaleList.TabIndex = 16;
            // 
            // FormSalesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1060, 525);
            this.Controls.Add(this.dataGridViewSaleList);
            this.Name = "FormSalesList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sales List";
            this.Load += new System.EventHandler(this.FormSalesList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSaleList;
    }
}