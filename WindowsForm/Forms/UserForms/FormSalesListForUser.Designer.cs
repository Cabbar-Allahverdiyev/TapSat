﻿
namespace WindowsForm.Forms.UserForms
{
    partial class FormSalesListForUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSalesListForUser));
            this.dataGridViewSaleList = new System.Windows.Forms.DataGridView();
            this.buttonAxtar = new System.Windows.Forms.Button();
            this.comboBoxDays = new System.Windows.Forms.ComboBox();
            this.comboBoxMonths = new System.Windows.Forms.ComboBox();
            this.comboBoxYears = new System.Windows.Forms.ComboBox();
            this.groupBoxDate = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.buttonTemizle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxRefresh = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxAxtar = new System.Windows.Forms.TextBox();
            this.groupBoxCancelSale = new System.Windows.Forms.GroupBox();
            this.checkBoxSatisLegvEdilsin = new System.Windows.Forms.CheckBox();
            this.textBoxTarix = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxSatIici = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxUmumiDeyer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMiqdar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxMehsul = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSaleId = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonTetbiqEt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleList)).BeginInit();
            this.groupBoxDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefresh)).BeginInit();
            this.groupBoxCancelSale.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewSaleList
            // 
            this.dataGridViewSaleList.AllowUserToAddRows = false;
            this.dataGridViewSaleList.AllowUserToDeleteRows = false;
            this.dataGridViewSaleList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSaleList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
            this.dataGridViewSaleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSaleList.Location = new System.Drawing.Point(266, 43);
            this.dataGridViewSaleList.Name = "dataGridViewSaleList";
            this.dataGridViewSaleList.ReadOnly = true;
            this.dataGridViewSaleList.RowTemplate.Height = 25;
            this.dataGridViewSaleList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSaleList.Size = new System.Drawing.Size(634, 483);
            this.dataGridViewSaleList.TabIndex = 16;
            this.dataGridViewSaleList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSaleList_CellDoubleClick);
            // 
            // buttonAxtar
            // 
            this.buttonAxtar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAxtar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAxtar.Image = global::WindowsForm.Properties.Resources.searchFerqliBlack_16px;
            this.buttonAxtar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAxtar.Location = new System.Drawing.Point(167, 118);
            this.buttonAxtar.Name = "buttonAxtar";
            this.buttonAxtar.Size = new System.Drawing.Size(75, 25);
            this.buttonAxtar.TabIndex = 17;
            this.buttonAxtar.Text = "   Axtar";
            this.buttonAxtar.UseVisualStyleBackColor = true;
            this.buttonAxtar.Click += new System.EventHandler(this.buttonAxtar_Click);
            // 
            // comboBoxDays
            // 
            this.comboBoxDays.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxDays.FormattingEnabled = true;
            this.comboBoxDays.Location = new System.Drawing.Point(73, 21);
            this.comboBoxDays.Name = "comboBoxDays";
            this.comboBoxDays.Size = new System.Drawing.Size(169, 22);
            this.comboBoxDays.TabIndex = 22;
            // 
            // comboBoxMonths
            // 
            this.comboBoxMonths.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxMonths.FormattingEnabled = true;
            this.comboBoxMonths.Location = new System.Drawing.Point(73, 54);
            this.comboBoxMonths.Name = "comboBoxMonths";
            this.comboBoxMonths.Size = new System.Drawing.Size(169, 22);
            this.comboBoxMonths.TabIndex = 24;
            // 
            // comboBoxYears
            // 
            this.comboBoxYears.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxYears.FormattingEnabled = true;
            this.comboBoxYears.Location = new System.Drawing.Point(73, 87);
            this.comboBoxYears.Name = "comboBoxYears";
            this.comboBoxYears.Size = new System.Drawing.Size(169, 22);
            this.comboBoxYears.TabIndex = 26;
            // 
            // groupBoxDate
            // 
            this.groupBoxDate.Controls.Add(this.label5);
            this.groupBoxDate.Controls.Add(this.labelTotal);
            this.groupBoxDate.Controls.Add(this.buttonTemizle);
            this.groupBoxDate.Controls.Add(this.label3);
            this.groupBoxDate.Controls.Add(this.label2);
            this.groupBoxDate.Controls.Add(this.label1);
            this.groupBoxDate.Controls.Add(this.comboBoxDays);
            this.groupBoxDate.Controls.Add(this.comboBoxYears);
            this.groupBoxDate.Controls.Add(this.buttonAxtar);
            this.groupBoxDate.Controls.Add(this.comboBoxMonths);
            this.groupBoxDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxDate.Location = new System.Drawing.Point(12, 38);
            this.groupBoxDate.Name = "groupBoxDate";
            this.groupBoxDate.Size = new System.Drawing.Size(248, 215);
            this.groupBoxDate.TabIndex = 27;
            this.groupBoxDate.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(114, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 37;
            this.label5.Text = "Alver :";
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTotal.ForeColor = System.Drawing.Color.Red;
            this.labelTotal.Location = new System.Drawing.Point(166, 163);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(22, 21);
            this.labelTotal.TabIndex = 34;
            this.labelTotal.Text = "#";
            // 
            // buttonTemizle
            // 
            this.buttonTemizle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTemizle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonTemizle.Image = ((System.Drawing.Image)(resources.GetObject("buttonTemizle.Image")));
            this.buttonTemizle.Location = new System.Drawing.Point(73, 118);
            this.buttonTemizle.Name = "buttonTemizle";
            this.buttonTemizle.Size = new System.Drawing.Size(88, 25);
            this.buttonTemizle.TabIndex = 30;
            this.buttonTemizle.Text = "Təmizlə";
            this.buttonTemizle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonTemizle.UseVisualStyleBackColor = true;
            this.buttonTemizle.Click += new System.EventHandler(this.buttonTemizle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(47, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 14);
            this.label3.TabIndex = 29;
            this.label3.Text = "İl :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(38, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 14);
            this.label2.TabIndex = 28;
            this.label2.Text = "Ay :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(29, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 14);
            this.label1.TabIndex = 27;
            this.label1.Text = "Gün :";
            // 
            // pictureBoxRefresh
            // 
            this.pictureBoxRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxRefresh.ErrorImage = global::WindowsForm.Properties.Resources.exitWhite;
            this.pictureBoxRefresh.Image = global::WindowsForm.Properties.Resources.refreshBlack_16px_;
            this.pictureBoxRefresh.InitialImage = global::WindowsForm.Properties.Resources.exitWhite;
            this.pictureBoxRefresh.Location = new System.Drawing.Point(875, 15);
            this.pictureBoxRefresh.Name = "pictureBoxRefresh";
            this.pictureBoxRefresh.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxRefresh.TabIndex = 31;
            this.pictureBoxRefresh.TabStop = false;
            this.pictureBoxRefresh.Click += new System.EventHandler(this.pictureBoxRefresh_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Image = global::WindowsForm.Properties.Resources.searchFerqliBlack_16px;
            this.label12.Location = new System.Drawing.Point(702, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 17);
            this.label12.TabIndex = 33;
            this.label12.Text = "     ";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // textBoxAxtar
            // 
            this.textBoxAxtar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAxtar.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxAxtar.Location = new System.Drawing.Point(731, 15);
            this.textBoxAxtar.Name = "textBoxAxtar";
            this.textBoxAxtar.Size = new System.Drawing.Size(138, 22);
            this.textBoxAxtar.TabIndex = 32;
            this.textBoxAxtar.TextChanged += new System.EventHandler(this.textBoxAxtar_TextChanged);
            // 
            // groupBoxCancelSale
            // 
            this.groupBoxCancelSale.Controls.Add(this.checkBoxSatisLegvEdilsin);
            this.groupBoxCancelSale.Controls.Add(this.textBoxTarix);
            this.groupBoxCancelSale.Controls.Add(this.label10);
            this.groupBoxCancelSale.Controls.Add(this.textBoxSatIici);
            this.groupBoxCancelSale.Controls.Add(this.label9);
            this.groupBoxCancelSale.Controls.Add(this.textBoxUmumiDeyer);
            this.groupBoxCancelSale.Controls.Add(this.label8);
            this.groupBoxCancelSale.Controls.Add(this.textBoxMiqdar);
            this.groupBoxCancelSale.Controls.Add(this.label7);
            this.groupBoxCancelSale.Controls.Add(this.textBoxMehsul);
            this.groupBoxCancelSale.Controls.Add(this.label6);
            this.groupBoxCancelSale.Controls.Add(this.textBoxSaleId);
            this.groupBoxCancelSale.Controls.Add(this.label13);
            this.groupBoxCancelSale.Controls.Add(this.buttonTetbiqEt);
            this.groupBoxCancelSale.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxCancelSale.Location = new System.Drawing.Point(12, 264);
            this.groupBoxCancelSale.Name = "groupBoxCancelSale";
            this.groupBoxCancelSale.Size = new System.Drawing.Size(248, 264);
            this.groupBoxCancelSale.TabIndex = 35;
            this.groupBoxCancelSale.TabStop = false;
            // 
            // checkBoxSatisLegvEdilsin
            // 
            this.checkBoxSatisLegvEdilsin.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxSatisLegvEdilsin.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxSatisLegvEdilsin.Location = new System.Drawing.Point(6, 186);
            this.checkBoxSatisLegvEdilsin.Name = "checkBoxSatisLegvEdilsin";
            this.checkBoxSatisLegvEdilsin.Size = new System.Drawing.Size(134, 21);
            this.checkBoxSatisLegvEdilsin.TabIndex = 36;
            this.checkBoxSatisLegvEdilsin.Text = "Satış ləğv edilsin";
            this.checkBoxSatisLegvEdilsin.UseVisualStyleBackColor = true;
            // 
            // textBoxTarix
            // 
            this.textBoxTarix.Enabled = false;
            this.textBoxTarix.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxTarix.Location = new System.Drawing.Point(105, 158);
            this.textBoxTarix.Name = "textBoxTarix";
            this.textBoxTarix.Size = new System.Drawing.Size(137, 22);
            this.textBoxTarix.TabIndex = 39;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(62, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 14);
            this.label10.TabIndex = 38;
            this.label10.Text = "Tarix :";
            // 
            // textBoxSatIici
            // 
            this.textBoxSatIici.Enabled = false;
            this.textBoxSatIici.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSatIici.Location = new System.Drawing.Point(105, 130);
            this.textBoxSatIici.Name = "textBoxSatIici";
            this.textBoxSatIici.Size = new System.Drawing.Size(137, 22);
            this.textBoxSatIici.TabIndex = 37;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(54, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 14);
            this.label9.TabIndex = 36;
            this.label9.Text = "Satıcı :";
            // 
            // textBoxUmumiDeyer
            // 
            this.textBoxUmumiDeyer.Enabled = false;
            this.textBoxUmumiDeyer.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxUmumiDeyer.Location = new System.Drawing.Point(105, 102);
            this.textBoxUmumiDeyer.Name = "textBoxUmumiDeyer";
            this.textBoxUmumiDeyer.Size = new System.Drawing.Size(137, 22);
            this.textBoxUmumiDeyer.TabIndex = 37;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(9, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 14);
            this.label8.TabIndex = 36;
            this.label8.Text = "Ümumi Dəyəri :";
            // 
            // textBoxMiqdar
            // 
            this.textBoxMiqdar.Enabled = false;
            this.textBoxMiqdar.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMiqdar.Location = new System.Drawing.Point(105, 74);
            this.textBoxMiqdar.Name = "textBoxMiqdar";
            this.textBoxMiqdar.Size = new System.Drawing.Size(137, 22);
            this.textBoxMiqdar.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(46, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 14);
            this.label7.TabIndex = 34;
            this.label7.Text = "Miqdar :";
            // 
            // textBoxMehsul
            // 
            this.textBoxMehsul.Enabled = false;
            this.textBoxMehsul.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMehsul.Location = new System.Drawing.Point(105, 46);
            this.textBoxMehsul.Name = "textBoxMehsul";
            this.textBoxMehsul.Size = new System.Drawing.Size(137, 22);
            this.textBoxMehsul.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(48, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 14);
            this.label6.TabIndex = 32;
            this.label6.Text = "Məhsul :";
            // 
            // textBoxSaleId
            // 
            this.textBoxSaleId.Enabled = false;
            this.textBoxSaleId.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSaleId.Location = new System.Drawing.Point(105, 18);
            this.textBoxSaleId.Name = "textBoxSaleId";
            this.textBoxSaleId.Size = new System.Drawing.Size(137, 22);
            this.textBoxSaleId.TabIndex = 31;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Helvetica", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(51, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 14);
            this.label13.TabIndex = 27;
            this.label13.Text = "SaleId :";
            // 
            // buttonTetbiqEt
            // 
            this.buttonTetbiqEt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTetbiqEt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonTetbiqEt.Image = global::WindowsForm.Properties.Resources.ok_16px;
            this.buttonTetbiqEt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTetbiqEt.Location = new System.Drawing.Point(140, 213);
            this.buttonTetbiqEt.Name = "buttonTetbiqEt";
            this.buttonTetbiqEt.Size = new System.Drawing.Size(102, 25);
            this.buttonTetbiqEt.TabIndex = 17;
            this.buttonTetbiqEt.Text = "   Tətbiq et";
            this.buttonTetbiqEt.UseVisualStyleBackColor = true;
            this.buttonTetbiqEt.Click += new System.EventHandler(this.buttonTetbiqEt_Click);
            // 
            // FormSalesListForUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(912, 540);
            this.Controls.Add(this.groupBoxCancelSale);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxAxtar);
            this.Controls.Add(this.pictureBoxRefresh);
            this.Controls.Add(this.groupBoxDate);
            this.Controls.Add(this.dataGridViewSaleList);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FormSalesListForUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Satışları sıralamaq səhifəsi";
            this.Load += new System.EventHandler(this.FormSalesList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleList)).EndInit();
            this.groupBoxDate.ResumeLayout(false);
            this.groupBoxDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefresh)).EndInit();
            this.groupBoxCancelSale.ResumeLayout(false);
            this.groupBoxCancelSale.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSaleList;
        private System.Windows.Forms.Button buttonAxtar;
        private System.Windows.Forms.ComboBox comboBoxDays;
        private System.Windows.Forms.ComboBox comboBoxMonths;
        private System.Windows.Forms.ComboBox comboBoxYears;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxRefresh;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxAxtar;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonTemizle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxSaleId;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonTetbiqEt;
        private System.Windows.Forms.TextBox textBoxUmumiDeyer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMiqdar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxMehsul;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxSatisLegvEdilsin;
        private System.Windows.Forms.TextBox textBoxTarix;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxSatIici;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBoxDate;
        private System.Windows.Forms.GroupBox groupBoxCancelSale;
    }
}