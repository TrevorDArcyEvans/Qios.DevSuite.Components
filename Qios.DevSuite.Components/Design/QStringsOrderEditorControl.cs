// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QStringsOrderEditorControl
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QStringsOrderEditorControl : UserControl
  {
    private Panel panel1;
    private ListBox liItems;
    private Button btUp;
    private Button btDown;
    private Container components;

    public QStringsOrderEditorControl() => this.InitializeComponent();

    public string ItemsString
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < this.liItems.Items.Count; ++index)
        {
          string str1 = this.liItems.Items[index].ToString();
          if (str1 != null && str1.Length > 0)
          {
            stringBuilder.Append(str1);
            if (index < this.liItems.Items.Count - 1)
            {
              string str2 = this.liItems.Items[index + 1].ToString();
              if (str2 != null && str2.Length > 0)
                stringBuilder.Append(", ");
            }
          }
        }
        return stringBuilder.ToString();
      }
      set
      {
        this.liItems.Items.Clear();
        if (value == null || value.Length <= 0)
          return;
        string str1 = value;
        char[] chArray = new char[1]{ ',' };
        foreach (string str2 in str1.Split(chArray))
          this.liItems.Items.Add((object) str2.Trim());
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.liItems = new ListBox();
      this.panel1 = new Panel();
      this.btDown = new Button();
      this.btUp = new Button();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.liItems.BorderStyle = BorderStyle.FixedSingle;
      this.liItems.Dock = DockStyle.Fill;
      this.liItems.IntegralHeight = false;
      this.liItems.Location = new Point(2, 2);
      this.liItems.Name = "liItems";
      this.liItems.SelectionMode = SelectionMode.MultiExtended;
      this.liItems.Size = new Size(140, 168);
      this.liItems.TabIndex = 0;
      this.panel1.Controls.Add((Control) this.btDown);
      this.panel1.Controls.Add((Control) this.btUp);
      this.panel1.Dock = DockStyle.Bottom;
      this.panel1.Location = new Point(2, 170);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(140, 28);
      this.panel1.TabIndex = 1;
      this.btDown.FlatStyle = FlatStyle.Flat;
      this.btDown.Location = new Point(53, 2);
      this.btDown.Name = "btDown";
      this.btDown.Size = new Size(48, 23);
      this.btDown.TabIndex = 1;
      this.btDown.Text = "Down";
      this.btDown.Click += new EventHandler(this.btDown_Click);
      this.btUp.FlatStyle = FlatStyle.Flat;
      this.btUp.Location = new Point(1, 2);
      this.btUp.Name = "btUp";
      this.btUp.Size = new Size(48, 23);
      this.btUp.TabIndex = 0;
      this.btUp.Text = "Up";
      this.btUp.Click += new EventHandler(this.btUp_Click);
      this.Controls.Add((Control) this.liItems);
      this.Controls.Add((Control) this.panel1);
      this.DockPadding.All = 2;
      this.Name = nameof (QStringsOrderEditorControl);
      this.Size = new Size(144, 200);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void SwapItem(int firstIndex, int secondIndex)
    {
      object obj = this.liItems.Items[firstIndex];
      this.liItems.Items[firstIndex] = this.liItems.Items[secondIndex];
      this.liItems.Items[secondIndex] = obj;
    }

    private void MoveUp()
    {
      if (this.liItems.SelectedIndices.Count == 0 || this.liItems.SelectedIndices[0] <= 0)
        return;
      int[] destination = new int[this.liItems.SelectedIndices.Count];
      this.liItems.SelectedIndices.CopyTo((Array) destination, 0);
      this.liItems.ClearSelected();
      for (int index = 0; index < destination.Length; ++index)
      {
        int firstIndex = destination[index];
        this.SwapItem(firstIndex, firstIndex - 1);
        this.liItems.SetSelected(firstIndex - 1, true);
      }
    }

    private void MoveDown()
    {
      if (this.liItems.SelectedIndices.Count == 0 || this.liItems.SelectedIndices[this.liItems.SelectedIndices.Count - 1] >= this.liItems.Items.Count - 1)
        return;
      int[] destination = new int[this.liItems.SelectedIndices.Count];
      this.liItems.SelectedIndices.CopyTo((Array) destination, 0);
      this.liItems.ClearSelected();
      for (int index = destination.Length - 1; index >= 0; --index)
      {
        int firstIndex = destination[index];
        this.SwapItem(firstIndex, firstIndex + 1);
        this.liItems.SetSelected(firstIndex + 1, true);
      }
    }

    private void btUp_Click(object sender, EventArgs e) => this.MoveUp();

    private void btDown_Click(object sender, EventArgs e) => this.MoveDown();
  }
}
