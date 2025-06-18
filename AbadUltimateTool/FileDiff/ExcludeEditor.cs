using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace AbadUltimateTool;


// uses a bindinglist copy so unsaved edits don’t leak into the live set, aka, it confused the fuck out of me that the list was edited but changes were not being pushed
public partial class ExcludeEditor : Form
{
    // local, editable list backing the ListBox
    private readonly BindingList<string> _model;

    public ExcludeEditor()
    {
        InitializeComponent();

        // clone current persisted list
        _model = new BindingList<string>(ExcludeFolderStore.Names.ToList());
        lstNames.DataSource = _model;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        string name = txtNew.Text.Trim();
        if (name.Length == 0) return;

        if (!_model.Contains(name, StringComparer.OrdinalIgnoreCase))
            _model.Add(name);

        txtNew.Clear();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        foreach (var sel in lstNames.SelectedItems.Cast<string>().ToList())
            _model.Remove(sel);
    }

    //  copy local list into the shared store + disk
    private void btnSave_Click(object sender, EventArgs e)
    {
        ExcludeFolderStore.ReplaceAndSave(_model);
        MessageBox.Show(this, "Exclusion list saved.",
                        "File Diff", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        var decision = (DialogResult)Invoke(new Func<DialogResult>(() =>
                            MessageBox.Show(this,
                                $"Continue without saving?\n" +
                                "You will lose progress.\n",
                                "Don't be sleepy...\n",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2)));

        if (decision == DialogResult.No)   // go back
        { }


        if (decision == DialogResult.Yes)   // close the window without saving
        {
            Close();

        }

    }

}
