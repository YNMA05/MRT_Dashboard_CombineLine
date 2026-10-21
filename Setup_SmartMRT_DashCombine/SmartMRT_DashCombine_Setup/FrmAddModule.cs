using SmartMRT_DashCombine_Setup.Controls;
using SmartMRT_DashCombine_Setup.Security;
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

namespace SmartMRT_DashCombine_Setup
{
    public partial class FrmAddModule : Form
    {
        private Panel pnlModuleEditor;
        private TextBox txtModuleName;
        private TextBox txtModuleCode;
        private GlassButton btnSaveModule;
        private GlassButton btnCancelModule;
        private GlassButton btnDeleteModule;
        private GlassButton btnEditModule;
        private int selectedModuleId = -1;
        private int editingModuleId = -1;
        public FrmAddModule()
        {
            InitializeComponent();
           // this.Load += FrmAddModule_Load;
        }

        private void FrmAddModule_Load(object sender, EventArgs e)
        {
            try
            {
                BUilDUI();
                LoadModuleGrid();
                dgvModule.SelectionChanged += dgvModule_SelectionChanged;

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to View Module:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void LoadModuleGrid()
        {
            try
            {
                string connectionString =
                    ConnectionMain.GetDashboardConnection();

                string sql = @"
    SELECT 
        MODULEID,
        MODULENAME,
        MODULECODE,
        STATUS_ACTIVE,
        CASE 
            WHEN STATUS_ACTIVE = 1 THEN 'Active'
            ELSE 'Inactive'
        END AS STATUS,
        DATEMODIFIED
    FROM DC_DASHMODULE
    ORDER BY MODULEID";

                using (SqlConnection connection =
                       new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter =
                        new SqlDataAdapter(sql, connection);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvModule.AutoGenerateColumns = true;
                    dgvModule.DataSource = dataTable;
                }

                // ==============================
                // COLUMN SETTINGS
                // ==============================
                dgvModule.Columns["MODULEID"].Visible = false;
                dgvModule.Columns["STATUS_ACTIVE"].Visible = false;

                dgvModule.Columns["MODULENAME"].HeaderText =
                    "MODULE NAME";

                dgvModule.Columns["MODULECODE"].HeaderText =
                    "MODULE CODE";

                dgvModule.Columns["STATUS"].HeaderText =
                    "STATUS";

                dgvModule.Columns["DATEMODIFIED"].HeaderText =
                    "LAST MODIFIED";

                dgvModule.Columns["MODULENAME"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;

                dgvModule.Columns["MODULECODE"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.None;

                dgvModule.Columns["MODULECODE"].Width = 180;

                dgvModule.Columns["STATUS"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.None;

                dgvModule.Columns["STATUS"].Width = 120;

                dgvModule.Columns["DATEMODIFIED"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.None;

                dgvModule.Columns["DATEMODIFIED"].Width = 180;

                // ==============================
                // FORCE ROW HEIGHT
                // ==============================

                dgvModule.AutoSizeRowsMode =
                    DataGridViewAutoSizeRowsMode.None;

                dgvModule.AllowUserToResizeRows = false;

                dgvModule.RowTemplate.Height = 45;

                foreach (DataGridViewRow row in dgvModule.Rows)
                {
                    row.Height = 45;
                }

                // ==============================
                // STYLE
                // ==============================

                StyleModuleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load module grid:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BUilDUI()
        {
            // =========================
            // HEADER
            // =========================

            Panel pnlHeader = new Panel
            {
                Name = "pnlHeader",
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.Transparent
            };

            // =========================
            // MODULE LIST
            // =========================

            Panel pnlModuleList = new Panel
            {
                Name = "pnlModuleList",
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 5, 20, 20),
                BackColor = Color.Transparent
            };

            // Add list first
            Controls.Add(pnlModuleList);

            // Add header second
            Controls.Add(pnlHeader);

            // =========================
            // TITLE
            // =========================

            Label lblTitle = new Label
            {
                Text = "Module Management",
                Font = new Font(
                    "Segoe UI",
                    20F,
                    FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(25, 12)
            };

            pnlHeader.Controls.Add(lblTitle);

            // =========================
            // SUBTITLE
            // =========================

            Label lblSubtitle = new Label
            {
                Text = "Manage SmartMRT dashboard modules",
                Font = new Font(
                    "Segoe UI",
                    9.5F),
                ForeColor =
                    Color.FromArgb(150, 180, 210),
                AutoSize = true,
                Location = new Point(27, 47)
            };

            pnlHeader.Controls.Add(lblSubtitle);

            // =========================
            // ADD BUTTON
            // =========================

            GlassButton btnAddModule = new GlassButton
            {
                Name = "btnAddModule",
                Text = "+ Add Module",
                Size = new Size(130, 40),

                BorderColor =
                    Color.FromArgb(0, 212, 255),

                GlassColor =
                    Color.FromArgb(20, 50, 70),

                TextColor = Color.White,

                Font =
                    new Font(
                        "Segoe UI",
                        10F,
                        FontStyle.Bold),

                Cursor = Cursors.Hand,

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right
            };

            btnAddModule.Location = new Point(
                pnlHeader.ClientSize.Width -
                btnAddModule.Width -
                25,
                20);

            pnlHeader.Controls.Add(btnAddModule);

            // =========================
            // DATAGRID
            // =========================

            //pnlHeader.Controls.Add(btnAddModule);

            dgvModule.Parent = pnlModuleList;
            dgvModule.Dock = DockStyle.Fill;
            dgvModule.Visible = true;
            dgvModule.BringToFront();

            pnlModuleList.BringToFront();

            pnlModuleEditor = new Panel
            {
                Name = "pnlModuleEditor",
                Dock = DockStyle.Bottom,
                Height = 180,
                BackColor = Color.FromArgb(16, 36, 71),
                Padding = new Padding(25),
                Visible = false
            };

            Controls.Add(pnlModuleEditor);

            Label lblEditorTitle = new Label
            {
                Text = "Add Module",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(25, 15)
            };

            pnlModuleEditor.Controls.Add(lblEditorTitle);

            // Module Name
            Label lblModuleName = new Label
            {
                Text = "Module Name",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(168, 179, 199),
                AutoSize = true,
                Location = new Point(25, 55)
            };

            pnlModuleEditor.Controls.Add(lblModuleName);

            txtModuleName = new TextBox
            {
                Width = 300,
                Location = new Point(25, 78),
                Font = new Font("Segoe UI", 10F)
            };

            pnlModuleEditor.Controls.Add(txtModuleName);

            // Module Code
            Label lblModuleCode = new Label
            {
                Text = "Module Code",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(168, 179, 199),
                AutoSize = true,
                Location = new Point(350, 55)
            };

            pnlModuleEditor.Controls.Add(lblModuleCode);

            txtModuleCode = new TextBox
            {
                Width = 300,
                Location = new Point(350, 78),
                Font = new Font("Segoe UI", 10F)
            };

            pnlModuleEditor.Controls.Add(txtModuleCode);

            // Save
            btnSaveModule = new GlassButton
            {
                Text = "Save Module",
                Size = new Size(120, 38),
                Location = new Point(680, 75),
                BorderColor = Color.FromArgb(0, 212, 255),
                GlassColor = Color.FromArgb(20, 60, 80),
                TextColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            pnlModuleEditor.Controls.Add(btnSaveModule);

            // Cancel
            btnCancelModule = new GlassButton
            {
                Text = "Cancel",
                Size = new Size(90, 38),
                Location = new Point(810, 75),
                BorderColor = Color.FromArgb(80, 100, 120),
                GlassColor = Color.FromArgb(30, 40, 55),
                TextColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };

            pnlModuleEditor.Controls.Add(btnCancelModule);

            // -----------------------------------------
            // BUTTON EVENTS
            // -----------------------------------------

            btnAddModule.Click += (s, e) =>
            {
                pnlModuleEditor.Visible = true;

                txtModuleName.Clear();
                txtModuleCode.Clear();

                txtModuleName.Focus();
                //AddNewModule();
            };

            btnCancelModule.Click += (s, e) =>
            {
                pnlModuleEditor.Visible = false;
            };

            btnSaveModule.Click += BtnSaveModule_Click;

            // Keep grid above/below the correct areas
            //pnlHeader.BringToFront();
            //dgvModule.Parent = pnlModuleList;
            //dgvModule.Dock = DockStyle.Fill;
            //dgvModule.Visible = true;
            //dgvModule.BringToFront();
            pnlModuleEditor.BringToFront();

            btnEditModule = new GlassButton
            {
                Name = "btnEditModule",
                Text = "Edit",
                Size = new Size(90, 40),
                BorderColor = Color.FromArgb(0, 212, 255),
                GlassColor = Color.FromArgb(20, 50, 70),
                TextColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            btnDeleteModule = new GlassButton
            {
                Name = "btnDeleteModule",
                Text = "Deactivate",
                Size = new Size(110, 40),
                BorderColor = Color.FromArgb(255, 92, 92),
                GlassColor = Color.FromArgb(60, 30, 35),
                TextColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnAddModule.Location = new Point(
    pnlHeader.ClientSize.Width - btnAddModule.Width - 25,
    20);

            btnDeleteModule.Location = new Point(
                btnAddModule.Left - btnDeleteModule.Width - 10,
                20);

            btnEditModule.Location = new Point(
                btnDeleteModule.Left - btnEditModule.Width - 10,
                20);
            btnEditModule.Click += BtnEditModule_Click;
            btnDeleteModule.Click += BtnDeleteModule_Click;

            pnlHeader.Controls.Add(btnEditModule);
            pnlHeader.Controls.Add(btnDeleteModule);
            pnlHeader.Controls.Add(btnAddModule);
        }

        private void dgvModule_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgvModule.CurrentRow == null)
                return;

            if (dgvModule.CurrentRow.Cells["MODULEID"].Value == null)
                return;

            selectedModuleId =
                Convert.ToInt32(
                    dgvModule.CurrentRow.Cells["MODULEID"].Value);

            bool isActive =
                Convert.ToInt32(
                    dgvModule.CurrentRow.Cells["STATUS_ACTIVE"].Value) == 1;

            btnDeleteModule.Text =
                isActive
                    ? "Deactivate"
                    : "Reactivate";
        }

        private void BtnSaveModule_Click(object sender, EventArgs e)
        {
            string moduleName = txtModuleName.Text.Trim();
            string moduleCode = txtModuleCode.Text.Trim();

            if (string.IsNullOrEmpty(moduleName) ||
                string.IsNullOrEmpty(moduleCode))
            {
                MessageBox.Show(
                    "Please fill in both Module Name and Module Code.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                string connectionString =
                    ConnectionMain.GetDashboardConnection();

                using (SqlConnection connection =
                       new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (editingModuleId == -1)
                    {
                        // =========================
                        // ADD NEW MODULE
                        // =========================

                        string insertQuery = @"
                    INSERT INTO DC_DASHMODULE
                    (
                        MODULENAME,
                        MODULECODE,
                        STATUS_ACTIVE,
                        DATEMODIFIED
                    )
                    VALUES
                    (
                        @ModuleName,
                        @ModuleCode,
                        1,
                        GETDATE()
                    )";

                        using (SqlCommand command =
                               new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue(
                                "@ModuleName", moduleName);

                            command.Parameters.AddWithValue(
                                "@ModuleCode", moduleCode);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show(
                            "Module added successfully.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ActivityLogger.Log(connectionString, "CREATE_MODULE", moduleCode, $"Module Added Successfully :{moduleCode}");
                    }
                    else
                    {
                        // =========================
                        // EDIT EXISTING MODULE
                        // =========================

                        string updateQuery = @"
                    UPDATE DC_DASHMODULE
                    SET
                        MODULENAME = @ModuleName,
                        MODULECODE = @ModuleCode,
                        DATEMODIFIED = GETDATE()
                    WHERE MODULEID = @ModuleID";

                        using (SqlCommand command =
                               new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue(
                                "@ModuleID", editingModuleId);

                            command.Parameters.AddWithValue(
                                "@ModuleName", moduleName);

                            command.Parameters.AddWithValue(
                                "@ModuleCode", moduleCode);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show(
                            "Module updated successfully.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                            ActivityLogger.Log(connectionString, "EDIT_MODULE", moduleCode, $"Module Update Succesfully :{moduleCode}");
                       // ActivityLogger.Log(connectionString,"CREATE_USER", txtUsername.Text.Trim(), $"Created user: {txtUsername.Text.Trim()}");
                    }
                }

                // Reset editor mode
                editingModuleId = -1;

                pnlModuleEditor.Visible = false;

                // Refresh grid
                LoadModuleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to save module:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnEditModule_Click(object sender, EventArgs e)
        {
            if (dgvModule.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a module first.",
                    "Edit Module",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            editingModuleId = Convert.ToInt32(
                dgvModule.CurrentRow.Cells["MODULEID"].Value);

            txtModuleName.Text =
                dgvModule.CurrentRow.Cells["MODULENAME"].Value?.ToString() ?? "";

            txtModuleCode.Text =
                dgvModule.CurrentRow.Cells["MODULECODE"].Value?.ToString() ?? "";

            btnSaveModule.Text = "Save Changes";

            pnlModuleEditor.Visible = true;

            txtModuleName.Focus();
        }

        private void BtnDeleteModule_Click(object sender, EventArgs e)
        {
            if (selectedModuleId <= 0 ||
                dgvModule.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a module first.",
                    "Module Status",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string moduleName =
                dgvModule.CurrentRow.Cells["MODULENAME"]
                .Value?.ToString() ?? "";

            bool isCurrentlyActive =
                Convert.ToInt32(
                    dgvModule.CurrentRow.Cells["STATUS_ACTIVE"].Value) == 1;

            // =========================
            // DEACTIVATE
            // =========================

            if (isCurrentlyActive)
            {
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to deactivate '{moduleName}'?\n\n" +
                    "This module will no longer appear on the dashboard.",
                    "Deactivate Module",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }
            else
            {
                // =========================
                // REACTIVATE
                // =========================

                DialogResult result = MessageBox.Show(
                    $"Do you want to reactivate '{moduleName}'?",
                    "Reactivate Module",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }

            try
            {
                string connectionString =
                    ConnectionMain.GetDashboardConnection();

                using (SqlConnection connection =
                       new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"
                UPDATE DC_DASHMODULE
                SET
                    STATUS_ACTIVE = @StatusActive,
                    DATEMODIFIED = GETDATE()
                WHERE MODULEID = @ModuleID";

                    using (SqlCommand command =
                           new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue(
                            "@StatusActive",
                            isCurrentlyActive ? 0 : 1);

                        command.Parameters.AddWithValue(
                            "@ModuleID",
                            selectedModuleId);

                        command.ExecuteNonQuery();
                    }
                }

                // Log
                ActivityLogger.Log(
                    connectionString,
                    "MODULE",
                    isCurrentlyActive
                        ? "Deactivate Module"
                        : "Reactivate Module",
                    (isCurrentlyActive
                        ? "Module Deactivated Successfully : "
                        : "Module Reactivated Successfully : ")
                    + moduleName);

                selectedModuleId = -1;

                LoadModuleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to update module status:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void StyleModuleGrid()
        {
            dgvModule.ScrollBars = ScrollBars.Vertical;

            dgvModule.BackgroundColor =
                Color.FromArgb(8, 21, 47);

            dgvModule.BorderStyle =
                BorderStyle.None;

            dgvModule.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvModule.GridColor =
                Color.FromArgb(40, 75, 122);

            dgvModule.EnableHeadersVisualStyles = false;

            // ==============================
            // HEADER
            // ==============================

            dgvModule.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(16, 36, 71);

            dgvModule.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(168, 179, 199);

            dgvModule.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            dgvModule.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvModule.ColumnHeadersHeight = 42;

            // ==============================
            // CELLS
            // ==============================

            dgvModule.DefaultCellStyle.BackColor =
                Color.FromArgb(12, 29, 55);

            dgvModule.DefaultCellStyle.ForeColor =
                Color.White;

            dgvModule.DefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9.5F);

            dgvModule.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(20, 52, 82);

            dgvModule.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvModule.DefaultCellStyle.Padding =
                new Padding(12, 0, 12, 0);

            dgvModule.RowTemplate.Height = 45;
            
            dgvModule.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            dgvModule.AllowUserToResizeRows = false;

            // ==============================
            // BEHAVIOR
            // ==============================

            dgvModule.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvModule.MultiSelect = false;

            dgvModule.AllowUserToAddRows = false;

            dgvModule.AllowUserToDeleteRows = false;

            dgvModule.AllowUserToResizeRows = false;

            dgvModule.ReadOnly = true;

            dgvModule.RowHeadersVisible = false;

            dgvModule.AutoGenerateColumns = true;

            dgvModule.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            dgvModule.CellFormatting -= dgvModule_CellFormatting;
            dgvModule.CellFormatting += dgvModule_CellFormatting;
        }

        private void dgvModule_CellFormatting(
     object sender,
     DataGridViewCellFormattingEventArgs e)
        {
            if (dgvModule.Columns[e.ColumnIndex].Name == "STATUS")
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString();

                    if (status.Contains("Active"))
                    {
                        e.CellStyle.ForeColor =
                            Color.FromArgb(56, 211, 159);
                    }
                    else
                    {
                        e.CellStyle.ForeColor =
                            Color.FromArgb(255, 180, 100);
                    }
                }
            }
        }

    }
}
