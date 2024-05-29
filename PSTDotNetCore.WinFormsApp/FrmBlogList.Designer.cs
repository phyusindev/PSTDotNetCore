namespace PSTDotNetCore.WinFormsApp
{
    partial class FrmBlogList
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
            dgvBlogs = new DataGridView();
            ColId = new DataGridViewTextBoxColumn();
            ColEdit = new DataGridViewButtonColumn();
            ColDelete = new DataGridViewButtonColumn();
            ColTitle = new DataGridViewTextBoxColumn();
            ColAuthor = new DataGridViewTextBoxColumn();
            ColContent = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvBlogs).BeginInit();
            SuspendLayout();
            // 
            // dgvBlogs
            // 
            dgvBlogs.AllowUserToAddRows = false;
            dgvBlogs.AllowUserToDeleteRows = false;
            dgvBlogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBlogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBlogs.Columns.AddRange(new DataGridViewColumn[] { ColId, ColEdit, ColDelete, ColTitle, ColAuthor, ColContent });
            dgvBlogs.Dock = DockStyle.Fill;
            dgvBlogs.Location = new Point(0, 0);
            dgvBlogs.Name = "dgvBlogs";
            dgvBlogs.ReadOnly = true;
            dgvBlogs.RowTemplate.Height = 25;
            dgvBlogs.Size = new Size(484, 361);
            dgvBlogs.TabIndex = 0;
            dgvBlogs.CellContentClick += dgvBlogs_CellContentClick;
            // 
            // ColId
            // 
            ColId.DataPropertyName = "BlogId";
            ColId.HeaderText = "ID";
            ColId.Name = "ColId";
            ColId.ReadOnly = true;
            ColId.Visible = false;
            // 
            // ColEdit
            // 
            ColEdit.HeaderText = "Edit";
            ColEdit.Name = "ColEdit";
            ColEdit.ReadOnly = true;
            ColEdit.Text = "Edit";
            ColEdit.UseColumnTextForButtonValue = true;
            // 
            // ColDelete
            // 
            ColDelete.HeaderText = "Delete";
            ColDelete.Name = "ColDelete";
            ColDelete.ReadOnly = true;
            ColDelete.Text = "Delete";
            ColDelete.UseColumnTextForButtonValue = true;
            // 
            // ColTitle
            // 
            ColTitle.DataPropertyName = "BlogTitle";
            ColTitle.HeaderText = "Title";
            ColTitle.Name = "ColTitle";
            ColTitle.ReadOnly = true;
            // 
            // ColAuthor
            // 
            ColAuthor.DataPropertyName = "BlogAuthor";
            ColAuthor.HeaderText = "Author";
            ColAuthor.Name = "ColAuthor";
            ColAuthor.ReadOnly = true;
            // 
            // ColContent
            // 
            ColContent.DataPropertyName = "BlogContent";
            ColContent.HeaderText = "Content";
            ColContent.Name = "ColContent";
            ColContent.ReadOnly = true;
            // 
            // FrmBlogList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 361);
            Controls.Add(dgvBlogs);
            Name = "FrmBlogList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Blogs";
            Load += FrmBlogList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBlogs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvBlogs;
        private DataGridViewTextBoxColumn ColId;
        private DataGridViewButtonColumn ColDelete;
        private DataGridViewButtonColumn ColEdit;
        private DataGridViewTextBoxColumn ColTitle;
        private DataGridViewTextBoxColumn ColAuthor;
        private DataGridViewTextBoxColumn ColContent;
    }
}