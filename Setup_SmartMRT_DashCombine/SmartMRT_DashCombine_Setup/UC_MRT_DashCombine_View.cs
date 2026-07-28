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
    public partial class UC_MRT_DashCombine_View : UserControl
    {
        private DashboardConfig config;
        private int selectedModuleID = -1;

        // public EventHandler ModuleCard_Clicked { get; private set; }

        public UC_MRT_DashCombine_View()
        {
            InitializeComponent();
            flowModuleList.AutoScroll = true;
            flowModuleList.WrapContents = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void UC_MRT_DashCombine_View_Load(object sender, EventArgs e)
        {
            config = DashboardConfigManager.Load();

            if (config == null)
            {
                MessageBox.Show(
                    "DashboardConfig.json was not found.");

                return;
            }
            LoadModules();
            //LoadLinesForModule(card.ModuleID);
        }


        private void LoadModules()
        {
            try
            {
                flowModuleList.Controls.Clear();

                string password =
                    PasswordEncryption.Decrypt(
                        config.DashboardDatabase.Password);

                string connectionString =
                    ConnectionMain.GetSqlConnectionString(
                        config.DashboardDatabase.Server,
                        config.DashboardDatabase.Instance,
                        config.DashboardDatabase.Username,
                        password,
                        config.DashboardDatabase.Database);

                string sql = @"
            SELECT
                MODULEID,
                MODULENAME,
                MODULECODE
            FROM DC_DASHMODULE
            WHERE STATUS_ACTIVE = 1
            ORDER BY MODULEID";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            int count = 0;

                            while (reader.Read())
                            {
                                UC_Line_ModuleCard card =
                                    new UC_Line_ModuleCard();

                                card.ModuleID =
                                    Convert.ToInt32(
                                        reader["MODULEID"]);

                                card.ModuleName =
                                    reader["MODULENAME"].ToString();

                                card.ModuleCode =
                                    reader["MODULECODE"].ToString();

                                card.ModuleClicked +=
                                    ModuleCard_Clicked;

                                flowModuleList.Controls.Add(card);

                                count++;
                            }

                            MessageBox.Show(
                                $"Modules loaded: {count}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "LoadModules failed:\n\n" +
                    ex.ToString());
            }
        }

        private void ModuleCard_Clicked(
    object sender,
    EventArgs e)
        {
            if (sender is UC_Line_ModuleCard card)
            {
                selectedModuleID =
                    card.ModuleID;

                lblSelectedModule.Text =
                    $"Selected Module: {card.ModuleName}";

                LoadLinesForModule(
                    card.ModuleID);
            }
        }


        private void LoadLinesForModule(
        int moduleID)
        {
            flowLineList.Controls.Clear();

            string password =
                PasswordEncryption.Decrypt(
                    config.DashboardDatabase.Password);

            string connectionString =
                ConnectionMain.GetSqlConnectionString(
                    config.DashboardDatabase.Server,
                    config.DashboardDatabase.Instance,
                    config.DashboardDatabase.Username,
                    password,
                    config.DashboardDatabase.Database);

            string sql = @"
        SELECT
            L.LINEID,
            L.LINE_NUM,
            L.IP_ADDRESS,

            CASE
                WHEN LM.LINEID IS NOT NULL
                THEN 1
                ELSE 0
            END AS IS_SELECTED

        FROM DC_LINEDETAIL L

        LEFT JOIN DC_LINE_MODULE LM
            ON L.LINEID = LM.LINEID
            AND LM.MODULEID = @ModuleID

        WHERE L.STATUS_ACTIVE = 1

        ORDER BY L.LINE_NUM";

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@ModuleID",
                        moduleID);

                    using (SqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UC_Line_ViewCard card =
                                new UC_Line_ViewCard();

                            card.LineID =
                                Convert.ToInt32(
                                    reader["LINEID"]);

                            card.LineNumber =
                                Convert.ToInt32(
                                    reader["LINE_NUM"]);

                            card.IPAddress =
                                reader["IP_ADDRESS"]
                                .ToString();

                            card.IsSelected =
                                Convert.ToBoolean(
                                    reader["IS_SELECTED"]);

                            flowLineList.Controls.Add(
                                card);
                        }
                    }
                }
            }
        }

        private void SaveModuleLines(
    int moduleID)
        {
            string password =
              PasswordEncryption.Decrypt(
                  config.DashboardDatabase.Password);

            string connectionString =
                ConnectionMain.GetSqlConnectionString(
                    config.DashboardDatabase.Server,
                    config.DashboardDatabase.Instance,
                    config.DashboardDatabase.Username,
                    password,
                    config.DashboardDatabase.Database);


            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction =
                    conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Remove existing assignments
                        string deleteSql = @"
                    DELETE FROM DC_LINE_MODULE
                    WHERE MODULEID = @ModuleID";

                        using (SqlCommand cmd =
                            new SqlCommand(
                                deleteSql,
                                conn,
                                transaction))
                        {
                            cmd.Parameters.AddWithValue(
                                "@ModuleID",
                                moduleID);

                            cmd.ExecuteNonQuery();
                        }

                        // 2. Insert selected lines
                        foreach (Control control
                            in flowModuleList.Controls)
                        {
                            if (control is UC_Line_ViewCard card)
                            {
                                if (!card.IsSelected)
                                    continue;

                                string insertSql = @"
                            INSERT INTO DC_LINE_MODULE
                            (
                                LINEID,
                                MODULEID
                            )
                            VALUES
                            (
                                @LineID,
                                @ModuleID
                            )";

                                using (SqlCommand cmd =
                                    new SqlCommand(
                                        insertSql,
                                        conn,
                                        transaction))
                                {
                                    cmd.Parameters.AddWithValue(
                                        "@LineID",
                                        card.LineID);

                                    cmd.Parameters.AddWithValue(
                                        "@ModuleID",
                                        moduleID);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();

                        MessageBox.Show(
                            "Module configuration saved.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        MessageBox.Show(
                            "Save failed: "
                            + ex.Message);
                    }
                }
            }
        }

        private void btnSave_Click(
     object sender,
     EventArgs e)
        {
            if (selectedModuleID == -1)
            {
                MessageBox.Show(
                    "Please select a dashboard module.");

                return;
            }

            SaveModuleLines(
                selectedModuleID);
        }

        private void cbLineNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLineNum.SelectedValue == null)
                return;

            if (!int.TryParse(
                cbLineNum.SelectedValue.ToString(),
                out int lineID))
            {
                return;
            }

            // LoadModuleSettings(lineID);

        }

    }
}
