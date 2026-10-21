using System;
using System.Drawing;
using System.Windows.Forms;

public class FrmModuleEditor : Form
{
    public string ModuleName => txtModuleName.Text.Trim();
    public string ModuleCode => txtModuleCode.Text.Trim();

    private TextBox txtModuleName;
    private TextBox txtModuleCode;
    private Button btnOk;
    private Button btnCancel;

    public FrmModuleEditor()
    {
        Text = "Add Module";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(480, 220);
        MaximizeBox = false;
        MinimizeBox = false;

        txtModuleName = new TextBox { Location = new Point(20, 40), Width = 420 };
        txtModuleCode = new TextBox { Location = new Point(20, 95), Width = 420 };

        Controls.Add(new Label { Text = "Module Name", Location = new Point(20, 20) });
        Controls.Add(txtModuleName);
        Controls.Add(new Label { Text = "Module Code", Location = new Point(20, 75) });
        Controls.Add(txtModuleCode);

        btnOk = new Button { Text = "Save", Location = new Point(260, 140), DialogResult = DialogResult.OK };
        btnCancel = new Button { Text = "Cancel", Location = new Point(360, 140), DialogResult = DialogResult.Cancel };

        Controls.Add(btnOk);
        Controls.Add(btnCancel);

        AcceptButton = btnOk;
        CancelButton = btnCancel;
    }
}