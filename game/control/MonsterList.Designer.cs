namespace game.control
{
    partial class MonsterList
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
            this.fourStar4 = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.threeStar3 = new System.Windows.Forms.RadioButton();
            this.twoStar2 = new System.Windows.Forms.RadioButton();
            this.暗 = new System.Windows.Forms.RadioButton();
            this.火 = new System.Windows.Forms.RadioButton();
            this.恶魔 = new System.Windows.Forms.RadioButton();
            this.光 = new System.Windows.Forms.RadioButton();
            this.地 = new System.Windows.Forms.RadioButton();
            this.天使 = new System.Windows.Forms.RadioButton();
            this.水 = new System.Windows.Forms.RadioButton();
            this.天 = new System.Windows.Forms.RadioButton();
            this.鬼 = new System.Windows.Forms.RadioButton();
            this.木 = new System.Windows.Forms.RadioButton();
            this.starGroupBox = new System.Windows.Forms.GroupBox();
            this.propGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.attackMaxTBox = new System.Windows.Forms.TextBox();
            this.attackMinTBox = new System.Windows.Forms.TextBox();
            this.searchByConditionBtn = new System.Windows.Forms.Button();
            this.monsterInfoTBox = new System.Windows.Forms.TextBox();
            this.resetBtn = new System.Windows.Forms.Button();
            this.monsterNameTBox = new System.Windows.Forms.TextBox();
            this.searchByNameBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.starGroupBox.SuspendLayout();
            this.propGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // fourStar4
            // 
            this.fourStar4.AutoSize = true;
            this.fourStar4.Location = new System.Drawing.Point(36, 87);
            this.fourStar4.Name = "fourStar4";
            this.fourStar4.Size = new System.Drawing.Size(47, 16);
            this.fourStar4.TabIndex = 0;
            this.fourStar4.TabStop = true;
            this.fourStar4.Text = "四星";
            this.fourStar4.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(61, 187);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(316, 258);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // threeStar3
            // 
            this.threeStar3.AutoSize = true;
            this.threeStar3.Location = new System.Drawing.Point(36, 56);
            this.threeStar3.Name = "threeStar3";
            this.threeStar3.Size = new System.Drawing.Size(47, 16);
            this.threeStar3.TabIndex = 2;
            this.threeStar3.TabStop = true;
            this.threeStar3.Text = "三星";
            this.threeStar3.UseVisualStyleBackColor = true;
            // 
            // twoStar2
            // 
            this.twoStar2.AutoSize = true;
            this.twoStar2.Location = new System.Drawing.Point(36, 20);
            this.twoStar2.Name = "twoStar2";
            this.twoStar2.Size = new System.Drawing.Size(47, 16);
            this.twoStar2.TabIndex = 3;
            this.twoStar2.TabStop = true;
            this.twoStar2.Text = "二星";
            this.twoStar2.UseVisualStyleBackColor = true;
            // 
            // 暗
            // 
            this.暗.AutoSize = true;
            this.暗.Location = new System.Drawing.Point(27, 17);
            this.暗.Name = "暗";
            this.暗.Size = new System.Drawing.Size(59, 16);
            this.暗.TabIndex = 11;
            this.暗.TabStop = true;
            this.暗.Text = "暗属性";
            this.暗.UseVisualStyleBackColor = true;
            // 
            // 火
            // 
            this.火.AutoSize = true;
            this.火.Location = new System.Drawing.Point(27, 53);
            this.火.Name = "火";
            this.火.Size = new System.Drawing.Size(59, 16);
            this.火.TabIndex = 10;
            this.火.TabStop = true;
            this.火.Text = "火属性";
            this.火.UseVisualStyleBackColor = true;
            // 
            // 恶魔
            // 
            this.恶魔.AutoSize = true;
            this.恶魔.Location = new System.Drawing.Point(27, 84);
            this.恶魔.Name = "恶魔";
            this.恶魔.Size = new System.Drawing.Size(71, 16);
            this.恶魔.TabIndex = 9;
            this.恶魔.TabStop = true;
            this.恶魔.Text = "恶魔属性";
            this.恶魔.UseVisualStyleBackColor = true;
            // 
            // 光
            // 
            this.光.AutoSize = true;
            this.光.Location = new System.Drawing.Point(122, 17);
            this.光.Name = "光";
            this.光.Size = new System.Drawing.Size(59, 16);
            this.光.TabIndex = 15;
            this.光.TabStop = true;
            this.光.Text = "光属性";
            this.光.UseVisualStyleBackColor = true;
            // 
            // 地
            // 
            this.地.AutoSize = true;
            this.地.Location = new System.Drawing.Point(122, 53);
            this.地.Name = "地";
            this.地.Size = new System.Drawing.Size(59, 16);
            this.地.TabIndex = 14;
            this.地.TabStop = true;
            this.地.Text = "地属性";
            this.地.UseVisualStyleBackColor = true;
            // 
            // 天使
            // 
            this.天使.AutoSize = true;
            this.天使.Location = new System.Drawing.Point(122, 84);
            this.天使.Name = "天使";
            this.天使.Size = new System.Drawing.Size(71, 16);
            this.天使.TabIndex = 13;
            this.天使.TabStop = true;
            this.天使.Text = "天使属性";
            this.天使.UseVisualStyleBackColor = true;
            // 
            // 水
            // 
            this.水.AutoSize = true;
            this.水.Location = new System.Drawing.Point(223, 17);
            this.水.Name = "水";
            this.水.Size = new System.Drawing.Size(59, 16);
            this.水.TabIndex = 19;
            this.水.TabStop = true;
            this.水.Text = "水属性";
            this.水.UseVisualStyleBackColor = true;
            // 
            // 天
            // 
            this.天.AutoSize = true;
            this.天.Location = new System.Drawing.Point(223, 53);
            this.天.Name = "天";
            this.天.Size = new System.Drawing.Size(59, 16);
            this.天.TabIndex = 18;
            this.天.TabStop = true;
            this.天.Text = "天属性";
            this.天.UseVisualStyleBackColor = true;
            // 
            // 鬼
            // 
            this.鬼.AutoSize = true;
            this.鬼.Location = new System.Drawing.Point(223, 84);
            this.鬼.Name = "鬼";
            this.鬼.Size = new System.Drawing.Size(59, 16);
            this.鬼.TabIndex = 17;
            this.鬼.TabStop = true;
            this.鬼.Text = "鬼属性";
            this.鬼.UseVisualStyleBackColor = true;
            // 
            // 木
            // 
            this.木.AutoSize = true;
            this.木.Location = new System.Drawing.Point(306, 53);
            this.木.Name = "木";
            this.木.Size = new System.Drawing.Size(59, 16);
            this.木.TabIndex = 21;
            this.木.TabStop = true;
            this.木.Text = "木属性";
            this.木.UseVisualStyleBackColor = true;
            // 
            // starGroupBox
            // 
            this.starGroupBox.Controls.Add(this.fourStar4);
            this.starGroupBox.Controls.Add(this.threeStar3);
            this.starGroupBox.Controls.Add(this.twoStar2);
            this.starGroupBox.Location = new System.Drawing.Point(61, 32);
            this.starGroupBox.Name = "starGroupBox";
            this.starGroupBox.Size = new System.Drawing.Size(109, 118);
            this.starGroupBox.TabIndex = 22;
            this.starGroupBox.TabStop = false;
            this.starGroupBox.Text = "星级";
            // 
            // propGroupBox
            // 
            this.propGroupBox.Controls.Add(this.天);
            this.propGroupBox.Controls.Add(this.恶魔);
            this.propGroupBox.Controls.Add(this.木);
            this.propGroupBox.Controls.Add(this.火);
            this.propGroupBox.Controls.Add(this.水);
            this.propGroupBox.Controls.Add(this.暗);
            this.propGroupBox.Controls.Add(this.天使);
            this.propGroupBox.Controls.Add(this.鬼);
            this.propGroupBox.Controls.Add(this.地);
            this.propGroupBox.Controls.Add(this.光);
            this.propGroupBox.Location = new System.Drawing.Point(196, 35);
            this.propGroupBox.Name = "propGroupBox";
            this.propGroupBox.Size = new System.Drawing.Size(391, 131);
            this.propGroupBox.TabIndex = 23;
            this.propGroupBox.TabStop = false;
            this.propGroupBox.Text = "属性";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.attackMaxTBox);
            this.groupBox3.Controls.Add(this.attackMinTBox);
            this.groupBox3.Location = new System.Drawing.Point(610, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(180, 131);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "攻击力";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "|";
            // 
            // attackMaxTBox
            // 
            this.attackMaxTBox.Location = new System.Drawing.Point(32, 94);
            this.attackMaxTBox.Name = "attackMaxTBox";
            this.attackMaxTBox.Size = new System.Drawing.Size(100, 21);
            this.attackMaxTBox.TabIndex = 1;
            // 
            // attackMinTBox
            // 
            this.attackMinTBox.Location = new System.Drawing.Point(32, 33);
            this.attackMinTBox.Name = "attackMinTBox";
            this.attackMinTBox.Size = new System.Drawing.Size(100, 21);
            this.attackMinTBox.TabIndex = 0;
            // 
            // searchByConditionBtn
            // 
            this.searchByConditionBtn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.searchByConditionBtn.Location = new System.Drawing.Point(396, 363);
            this.searchByConditionBtn.Name = "searchByConditionBtn";
            this.searchByConditionBtn.Size = new System.Drawing.Size(180, 38);
            this.searchByConditionBtn.TabIndex = 25;
            this.searchByConditionBtn.Text = "检索";
            this.searchByConditionBtn.UseVisualStyleBackColor = true;
            this.searchByConditionBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // monsterInfoTBox
            // 
            this.monsterInfoTBox.Location = new System.Drawing.Point(396, 187);
            this.monsterInfoTBox.Multiline = true;
            this.monsterInfoTBox.Name = "monsterInfoTBox";
            this.monsterInfoTBox.ReadOnly = true;
            this.monsterInfoTBox.Size = new System.Drawing.Size(180, 170);
            this.monsterInfoTBox.TabIndex = 26;
            // 
            // resetBtn
            // 
            this.resetBtn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.resetBtn.Location = new System.Drawing.Point(396, 407);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(180, 38);
            this.resetBtn.TabIndex = 27;
            this.resetBtn.Text = "重置";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // monsterNameTBox
            // 
            this.monsterNameTBox.Location = new System.Drawing.Point(610, 187);
            this.monsterNameTBox.Multiline = true;
            this.monsterNameTBox.Name = "monsterNameTBox";
            this.monsterNameTBox.Size = new System.Drawing.Size(84, 27);
            this.monsterNameTBox.TabIndex = 28;
            // 
            // searchByNameBtn
            // 
            this.searchByNameBtn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.searchByNameBtn.Location = new System.Drawing.Point(706, 187);
            this.searchByNameBtn.Name = "searchByNameBtn";
            this.searchByNameBtn.Size = new System.Drawing.Size(84, 27);
            this.searchByNameBtn.TabIndex = 29;
            this.searchByNameBtn.Text = "查询";
            this.searchByNameBtn.UseVisualStyleBackColor = true;
            this.searchByNameBtn.Click += new System.EventHandler(this.searchByNameBtn_Click);
            // 
            // MonsterList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 460);
            this.Controls.Add(this.searchByNameBtn);
            this.Controls.Add(this.monsterNameTBox);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.monsterInfoTBox);
            this.Controls.Add(this.searchByConditionBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.propGroupBox);
            this.Controls.Add(this.starGroupBox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MonsterList";
            this.Text = "MonsterList";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.starGroupBox.ResumeLayout(false);
            this.starGroupBox.PerformLayout();
            this.propGroupBox.ResumeLayout(false);
            this.propGroupBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton fourStar4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton threeStar3;
        private System.Windows.Forms.RadioButton twoStar2;
        private System.Windows.Forms.RadioButton 暗;
        private System.Windows.Forms.RadioButton 火;
        private System.Windows.Forms.RadioButton 恶魔;
        private System.Windows.Forms.RadioButton 光;
        private System.Windows.Forms.RadioButton 地;
        private System.Windows.Forms.RadioButton 天使;
        private System.Windows.Forms.RadioButton 水;
        private System.Windows.Forms.RadioButton 天;
        private System.Windows.Forms.RadioButton 鬼;
        private System.Windows.Forms.RadioButton 木;
        private System.Windows.Forms.GroupBox starGroupBox;
        private System.Windows.Forms.GroupBox propGroupBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox attackMaxTBox;
        private System.Windows.Forms.TextBox attackMinTBox;
        private System.Windows.Forms.Button searchByConditionBtn;
        private System.Windows.Forms.TextBox monsterInfoTBox;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.TextBox monsterNameTBox;
        private System.Windows.Forms.Button searchByNameBtn;
    }
}