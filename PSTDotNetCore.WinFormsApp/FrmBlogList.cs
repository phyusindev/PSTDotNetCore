using Dapper;
using PSTDotNetCore.Shared;
using PSTDotNetCore.WinFormsApp.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSTDotNetCore.WinFormsApp
{
    public partial class FrmBlogList : Form
    {
        private readonly DapperService _dapperService;
        
        public FrmBlogList()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }

        private void BlogList()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>("Select * from tbl_blog");
            dgvBlogs.DataSource = lst;
        }

        private void dgvBlogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            #region if case

            var blogId = Convert.ToInt32(dgvBlogs.Rows[e.RowIndex].Cells["colId"].Value);

            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {
                FrmBlog frm = new FrmBlog(blogId);
                frm.ShowDialog();

                BlogList();
            }
            else if(e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                var dialogResult = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);               
                if (dialogResult != DialogResult.Yes) return;

                DeleteBlog(blogId);
                
            }
            #endregion

            //#region switch case

            //int index = e.ColumnIndex;
            //if (index == -1) return;

            //EnumFormControlType enumFormControlType = (EnumFormControlType)index;
            //switch (enumFormControlType)
            //{
            //    case EnumFormControlType.Edit:
            //        FrmBlog frm = new FrmBlog(blogId);
            //        frm.ShowDialog();

            //        BlogList();
            //        break;
            //    case EnumFormControlType.Delete:
            //        var dialogResult = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (dialogResult != DialogResult.Yes) return;

            //        DeleteBlog(blogId);
            //        break;
            //    case EnumFormControlType.None:
            //    default:
            //        MessageBox.Show("Invalid case");
            //        break;
            //}
            //#endregion 
        }

        private void DeleteBlog(int id)
        {
            string query = @"Delete from [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new { BlogId = id });
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            MessageBox.Show(message);
            BlogList();
        }
    }
}
