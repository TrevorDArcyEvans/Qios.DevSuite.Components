// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QShapeDesignerForm
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  public class QShapeDesignerForm : Form
  {
    private QShapeCollection m_oBaseShapes;
    private bool m_bSettingItems;
    private bool m_bSuspendPopulation;
    private Panel pnShapePainter;
    private Panel pnProperties;
    private Splitter spProperties;
    private Panel pnTop;
    private Button btOK;
    private Button btCancel;
    private Panel pnBaseShapes;
    private Label lbBaseShapes;
    private ComboBox cbBaseShapes;
    private Panel pnDialogButtons;
    private QShapePainterControl qspPainter;
    private PropertyGrid pgProperties;
    private Panel pnShapeType;
    private Label lbShapeType;
    private ComboBox cbShapeType;
    private Container components;

    public QShapeDesignerForm()
    {
      this.InitializeComponent();
      this.pgProperties.CommandsVisibleIfAvailable = false;
      this.m_oBaseShapes = new QShapeCollection();
      this.m_oBaseShapes.CollectionChanged += new EventHandler(this.BaseShapes_CollectionChanged);
    }

    public QShapeCollection BaseShapes => this.m_oBaseShapes;

    public QShape CurrentShape
    {
      get => this.qspPainter.Shape;
      set
      {
        if (this.qspPainter.Shape != null)
          this.qspPainter.Shape.ShapeChanged -= new EventHandler(this.CurrentShape_ShapeChanged);
        this.qspPainter.Shape = value;
        if (this.qspPainter.Shape != null)
        {
          this.qspPainter.Shape.ShapeChanged += new EventHandler(this.CurrentShape_ShapeChanged);
          this.m_bSettingItems = true;
          try
          {
            QShape qshape = this.qspPainter.Shape;
            while (qshape.BaseShape != null)
              qshape = qshape.BaseShape;
            this.cbShapeType.SelectedItem = (object) qshape.ShapeType.ToString();
            this.cbBaseShapes.SelectedItem = (object) qshape;
          }
          finally
          {
            this.m_bSettingItems = false;
          }
        }
        this.ShowActiveShapeObject();
      }
    }

    public QShape SelectedShape => this.cbBaseShapes.SelectedItem as QShape;

    public bool SuspendPopulation
    {
      get => this.m_bSuspendPopulation;
      set => this.m_bSuspendPopulation = value;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ResourceManager resourceManager = new ResourceManager(typeof (QShapeDesignerForm));
      this.qspPainter = new QShapePainterControl();
      this.pnShapePainter = new Panel();
      this.pnProperties = new Panel();
      this.pgProperties = new PropertyGrid();
      this.pnDialogButtons = new Panel();
      this.btCancel = new Button();
      this.btOK = new Button();
      this.spProperties = new Splitter();
      this.pnTop = new Panel();
      this.pnBaseShapes = new Panel();
      this.cbBaseShapes = new ComboBox();
      this.lbBaseShapes = new Label();
      this.pnShapeType = new Panel();
      this.cbShapeType = new ComboBox();
      this.lbShapeType = new Label();
      this.pnShapePainter.SuspendLayout();
      this.pnProperties.SuspendLayout();
      this.pnDialogButtons.SuspendLayout();
      this.pnTop.SuspendLayout();
      this.pnBaseShapes.SuspendLayout();
      this.pnShapeType.SuspendLayout();
      this.SuspendLayout();
      this.qspPainter.Dock = DockStyle.Fill;
      this.qspPainter.Location = new Point(0, 0);
      this.qspPainter.Name = "qspPainter";
      this.qspPainter.Shape = (QShape) null;
      this.qspPainter.ShowItemNumbers = false;
      this.qspPainter.Size = new Size(481, 327);
      this.qspPainter.TabIndex = 1;
      this.qspPainter.Zoom = 1f;
      this.qspPainter.ZoomStep = 1f;
      this.qspPainter.SelectedItemsChanged += new EventHandler(this.qspPainter_SelectedItemsChanged);
      this.pnShapePainter.BorderStyle = BorderStyle.FixedSingle;
      this.pnShapePainter.Controls.Add((Control) this.qspPainter);
      this.pnShapePainter.Dock = DockStyle.Fill;
      this.pnShapePainter.Location = new Point(5, 32);
      this.pnShapePainter.Name = "pnShapePainter";
      this.pnShapePainter.Size = new Size(483, 329);
      this.pnShapePainter.TabIndex = 2;
      this.pnProperties.Controls.Add((Control) this.pgProperties);
      this.pnProperties.Controls.Add((Control) this.pnDialogButtons);
      this.pnProperties.Dock = DockStyle.Right;
      this.pnProperties.Location = new Point(491, 5);
      this.pnProperties.Name = "pnProperties";
      this.pnProperties.Size = new Size(208, 356);
      this.pnProperties.TabIndex = 4;
      this.pgProperties.CommandsVisibleIfAvailable = true;
      this.pgProperties.Dock = DockStyle.Fill;
      this.pgProperties.LargeButtons = false;
      this.pgProperties.LineColor = SystemColors.ScrollBar;
      this.pgProperties.Location = new Point(0, 0);
      this.pgProperties.Name = "pgProperties";
      this.pgProperties.Size = new Size(208, 324);
      this.pgProperties.TabIndex = 0;
      this.pgProperties.Text = "PropertyGrid";
      this.pgProperties.ViewBackColor = SystemColors.Window;
      this.pgProperties.ViewForeColor = SystemColors.WindowText;
      this.pgProperties.PropertyValueChanged += new PropertyValueChangedEventHandler(this.pgProperties_PropertyValueChanged);
      this.pnDialogButtons.Controls.Add((Control) this.btCancel);
      this.pnDialogButtons.Controls.Add((Control) this.btOK);
      this.pnDialogButtons.Dock = DockStyle.Bottom;
      this.pnDialogButtons.Location = new Point(0, 324);
      this.pnDialogButtons.Name = "pnDialogButtons";
      this.pnDialogButtons.Size = new Size(208, 32);
      this.pnDialogButtons.TabIndex = 4;
      this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btCancel.DialogResult = DialogResult.Cancel;
      this.btCancel.Location = new Point(132, 5);
      this.btCancel.Name = "btCancel";
      this.btCancel.TabIndex = 1;
      this.btCancel.Text = "Cancel";
      this.btOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btOK.DialogResult = DialogResult.OK;
      this.btOK.Location = new Point(54, 5);
      this.btOK.Name = "btOK";
      this.btOK.TabIndex = 0;
      this.btOK.Text = "OK";
      this.spProperties.Dock = DockStyle.Right;
      this.spProperties.Location = new Point(488, 5);
      this.spProperties.Name = "spProperties";
      this.spProperties.Size = new Size(3, 356);
      this.spProperties.TabIndex = 6;
      this.spProperties.TabStop = false;
      this.pnTop.Controls.Add((Control) this.pnBaseShapes);
      this.pnTop.Controls.Add((Control) this.pnShapeType);
      this.pnTop.Dock = DockStyle.Top;
      this.pnTop.Location = new Point(5, 5);
      this.pnTop.Name = "pnTop";
      this.pnTop.Size = new Size(483, 27);
      this.pnTop.TabIndex = 7;
      this.pnBaseShapes.Controls.Add((Control) this.cbBaseShapes);
      this.pnBaseShapes.Controls.Add((Control) this.lbBaseShapes);
      this.pnBaseShapes.Dock = DockStyle.Left;
      this.pnBaseShapes.Location = new Point(200, 0);
      this.pnBaseShapes.Name = "pnBaseShapes";
      this.pnBaseShapes.Size = new Size(272, 27);
      this.pnBaseShapes.TabIndex = 0;
      this.cbBaseShapes.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbBaseShapes.Location = new Point(88, 3);
      this.cbBaseShapes.Name = "cbBaseShapes";
      this.cbBaseShapes.Size = new Size(176, 21);
      this.cbBaseShapes.TabIndex = 1;
      this.cbBaseShapes.SelectedIndexChanged += new EventHandler(this.cbBaseShapes_SelectedIndexChanged);
      this.lbBaseShapes.Location = new Point(16, 6);
      this.lbBaseShapes.Name = "lbBaseShapes";
      this.lbBaseShapes.Size = new Size(68, 16);
      this.lbBaseShapes.TabIndex = 0;
      this.lbBaseShapes.Text = "Base shape:";
      this.pnShapeType.Controls.Add((Control) this.cbShapeType);
      this.pnShapeType.Controls.Add((Control) this.lbShapeType);
      this.pnShapeType.Dock = DockStyle.Left;
      this.pnShapeType.Location = new Point(0, 0);
      this.pnShapeType.Name = "pnShapeType";
      this.pnShapeType.Size = new Size(200, 27);
      this.pnShapeType.TabIndex = 1;
      this.cbShapeType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbShapeType.Location = new Point(64, 3);
      this.cbShapeType.Name = "cbShapeType";
      this.cbShapeType.Size = new Size(121, 21);
      this.cbShapeType.TabIndex = 1;
      this.cbShapeType.SelectedIndexChanged += new EventHandler(this.cbShapeType_SelectedIndexChanged);
      this.lbShapeType.Location = new Point(4, 6);
      this.lbShapeType.Name = "lbShapeType";
      this.lbShapeType.Size = new Size(60, 16);
      this.lbShapeType.TabIndex = 0;
      this.lbShapeType.Text = "Base type:";
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(704, 366);
      this.Controls.Add((Control) this.pnShapePainter);
      this.Controls.Add((Control) this.pnTop);
      this.Controls.Add((Control) this.spProperties);
      this.Controls.Add((Control) this.pnProperties);
      this.DockPadding.All = 5;
      this.Icon = (Icon) resourceManager.GetObject("$this.Icon");
      this.Name = nameof (QShapeDesignerForm);
      this.Text = "QShape Designer";
      this.pnShapePainter.ResumeLayout(false);
      this.pnProperties.ResumeLayout(false);
      this.pnDialogButtons.ResumeLayout(false);
      this.pnTop.ResumeLayout(false);
      this.pnBaseShapes.ResumeLayout(false);
      this.pnShapeType.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    internal void PopulateBaseShapes()
    {
      if (this.cbShapeType.Items.Count == 0)
        this.PopulateShapeTypes();
      QShapeType qshapeType = QShapeType.TabButton;
      try
      {
        qshapeType = (QShapeType) Enum.Parse(typeof (QShapeType), Convert.ToString(this.cbShapeType.SelectedItem, (IFormatProvider) CultureInfo.InvariantCulture));
      }
      catch (ArgumentNullException ex)
      {
      }
      catch (ArgumentException ex)
      {
      }
      QShape selectedItem = this.cbBaseShapes.SelectedItem as QShape;
      this.cbBaseShapes.Items.Clear();
      for (int index = 0; index < this.BaseShapes.Count; ++index)
      {
        if (this.BaseShapes[index].ShapeType == qshapeType)
          this.cbBaseShapes.Items.Add((object) this.BaseShapes[index]);
      }
      if (selectedItem != null && this.cbBaseShapes.Items.Contains((object) selectedItem))
      {
        this.cbBaseShapes.SelectedItem = (object) selectedItem;
      }
      else
      {
        if (this.cbBaseShapes.Items.Count <= 0)
          return;
        this.cbBaseShapes.SelectedIndex = 0;
      }
    }

    internal void PopulateShapeTypes()
    {
      this.cbShapeType.Items.Clear();
      this.cbShapeType.Items.AddRange((object[]) Enum.GetNames(typeof (QShapeType)));
      this.cbShapeType.SelectedIndex = 0;
    }

    private void ShowActiveShapeObject()
    {
      if (this.qspPainter.ActiveItem == null)
      {
        if (this.pgProperties.SelectedObject == this.qspPainter.Shape)
          return;
        this.pgProperties.SelectedObject = (object) this.qspPainter.Shape;
      }
      else
      {
        if (this.pgProperties.SelectedObject == this.qspPainter.ActiveItem && this.pgProperties.SelectedObjects.Length == this.qspPainter.SelectedItems.Length)
          return;
        this.pgProperties.SelectedObjects = (object[]) this.qspPainter.SelectedItems;
      }
    }

    private void BaseShapes_CollectionChanged(object sender, EventArgs e)
    {
      if (this.SuspendPopulation)
        return;
      this.PopulateBaseShapes();
    }

    private void cbShapeType_SelectedIndexChanged(object sender, EventArgs e) => this.PopulateBaseShapes();

    private void cbBaseShapes_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.m_bSettingItems)
        return;
      QShape selectedShape = this.SelectedShape;
      if (selectedShape == null || this.CurrentShape == null)
        return;
      if (this.CurrentShape.IsChanged)
      {
        if (MessageBox.Show(QResources.GetGeneral("QShapeEditorForm_BaseShapeChanged"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
          return;
        selectedShape.CopyTo(this.CurrentShape);
      }
      else
        selectedShape.CopyTo(this.CurrentShape);
    }

    private void CurrentShape_ShapeChanged(object sender, EventArgs e) => this.pgProperties.Refresh();

    private void pgProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
      if (!(this.pgProperties.SelectedObject is QShape))
        return;
      this.qspPainter.HandleLayoutPropertyChanged();
      this.qspPainter.UpdateLayout();
      this.qspPainter.Invalidate();
    }

    private void qspPainter_SelectedItemsChanged(object sender, EventArgs e) => this.ShowActiveShapeObject();
  }
}
