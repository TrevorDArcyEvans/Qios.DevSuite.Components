// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabPage
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  [Designer(typeof (QNoResizeScrollableControlDesigner), typeof (IDesigner))]
  [DefaultEvent("Activated")]
  [TypeConverter(typeof (QTabPageTypeConverter))]
  [Designer(typeof (QTabPageDocumentDesigner), typeof (IRootDesigner))]
  public class QTabPage : QContainerControl, IQTabButtonSource, IQPersistableObject
  {
    private bool m_bPersistObject = true;
    private Guid m_oPersistGuid = Guid.NewGuid();
    private bool m_bIsPersisted;
    private QTabButtonPaintParams m_oTabButtonPaintParams;
    private QTabControl m_oTabControl;
    private QTabButton m_oTabButton;
    private bool m_bButtonVisibleDesignTime = true;
    private QTabButtonDockStyle m_eButtonDockStyle;
    private bool m_bActivating;
    private QWeakDelegate m_oActivatedDelegate;
    private QWeakDelegate m_oDeactivatedDelegate;
    private QWeakDelegate m_oClosingDelegate;
    private QWeakDelegate m_oClosedDelegate;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event EventHandler VisibleChanged;

    public QTabPage()
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor, true);
      this.m_oTabButton = this.CreateTabButton();
      this.m_oTabButtonPaintParams = new QTabButtonPaintParams();
      base.Visible = false;
      this.MinimumClientSize = Size.Empty;
      this.ResumeLayout(false);
    }

    protected virtual QTabButton CreateTabButton() => new QTabButton((IQTabButtonSource) this);

    [Category("QEvents")]
    [Description("Gets raised when the QTabPage is activated.")]
    [QWeakEvent]
    public event EventHandler Activated
    {
      add => this.m_oActivatedDelegate = QWeakDelegate.Combine(this.m_oActivatedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oActivatedDelegate = QWeakDelegate.Remove(this.m_oActivatedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the QTabPage is deactivated.")]
    [QWeakEvent]
    public event EventHandler Deactivated
    {
      add => this.m_oDeactivatedDelegate = QWeakDelegate.Combine(this.m_oDeactivatedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oDeactivatedDelegate = QWeakDelegate.Remove(this.m_oDeactivatedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the QTabPage is about to close.")]
    public event CancelEventHandler Closing
    {
      add => this.m_oClosingDelegate = QWeakDelegate.Combine(this.m_oClosingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oClosingDelegate = QWeakDelegate.Remove(this.m_oClosingDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the QTabPage is closed.")]
    public event EventHandler Closed
    {
      add => this.m_oClosedDelegate = QWeakDelegate.Combine(this.m_oClosedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oClosedDelegate = QWeakDelegate.Remove(this.m_oClosedDelegate, (Delegate) value);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int TabIndex
    {
      get => base.TabIndex;
      set => base.TabIndex = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new bool Visible
    {
      get => base.Visible;
      set => base.Visible = value;
    }

    [Browsable(false)]
    public bool CanFocusTabButton => this.TabControl != null && this.TabControl.FocusTabButtons;

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QTabPageAppearance();

    [Description("Gets or sets the QAppearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QTabPageAppearance Appearance => (QTabPageAppearance) base.Appearance;

    protected override void SetVisibleCore(bool value)
    {
      if (!value && this.TabControl != null && this.TabControl.ActiveControl == this)
        this.TabControl.ActiveControl = (Control) null;
      base.SetVisibleCore(value);
      if (!this.Visible || !(this.Parent is IContainerControl parent) || parent.ActiveControl == this)
        return;
      this.ActivateTabPageCore();
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if (this.TabControl != null)
      {
        if (this.TabControl.TabPagesBounds.Width > 0 && this.TabControl.TabPagesBounds.Height > 0)
        {
          Rectangle tabPagesBounds = this.TabControl.TabPagesBounds;
          base.SetBoundsCore(tabPagesBounds.Left, tabPagesBounds.Top, tabPagesBounds.Width, tabPagesBounds.Height, specified);
        }
        else
          base.SetBoundsCore(x, y, width, height, specified);
      }
      else
        base.SetBoundsCore(x, y, width, height, specified);
    }

    [Category("QButtonAppearance")]
    [DefaultValue(true)]
    [Description("Gets or sets whether the TabButton is visible")]
    public bool ButtonVisible
    {
      get => this.DesignMode ? this.m_bButtonVisibleDesignTime : this.TabButton.Visible;
      set
      {
        if (this.DesignMode)
          this.m_bButtonVisibleDesignTime = value;
        else
          this.TabButton.Visible = value;
      }
    }

    [Category("QAppearance")]
    [DefaultValue(true)]
    [Description("Gets or sets whether this QTabPage can be closed. To actually see the CloseButton QTabStripConfiguration.CloseButtonVisible must be specified")]
    public bool CanClose
    {
      get => this.TabButton.CanClose;
      set
      {
        this.TabButton.CanClose = value;
        if (this.TabControl == null || !this.TabButton.IsActivated)
          return;
        this.TabControl.EnableCloseButtons(this.TabButton.CanClose);
      }
    }

    [DefaultValue(-1)]
    [Category("QButtonAppearance")]
    [Description("Gets or sets the order of the button. -1 means it is added as last.")]
    public int ButtonOrder
    {
      get => this.TabButton.ButtonOrder;
      set => this.TabButton.ButtonOrder = value;
    }

    [DefaultValue(typeof (Size), "0,0")]
    public override Size MinimumClientSize
    {
      get => base.MinimumClientSize;
      set => base.MinimumClientSize = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DockStyle Dock
    {
      get => base.Dock;
      set
      {
      }
    }

    [Localizable(true)]
    public override string Text
    {
      get => base.Text;
      set
      {
        base.Text = value;
        this.TabButton.Text = value;
      }
    }

    [Localizable(true)]
    public override Font Font
    {
      get => base.Font;
      set => base.Font = value;
    }

    [Browsable(false)]
    public QTabButton TabButton => this.m_oTabButton;

    public void ResetButtonConfiguration() => this.m_oTabButton.Configuration.SetToDefaultValues();

    public bool ShouldSerializeButtonConfiguration() => !this.m_oTabButton.Configuration.IsSetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the configuration of the QTabButton for this QTabPage. This configuration uses the values of a parenting QTabStrip. You can override these values by setting values for this configuration.")]
    [Category("QButtonAppearance")]
    public QTabButtonConfiguration ButtonConfiguration
    {
      get => this.m_oTabButton.Configuration;
      set => this.m_oTabButton.Configuration = value;
    }

    [Category("QButtonAppearance")]
    [DefaultValue(QTabButtonDockStyle.Top)]
    [Description("Gets or sets the DockStyle of a QTabButton")]
    public QTabButtonDockStyle ButtonDockStyle
    {
      get => this.m_eButtonDockStyle;
      set
      {
        if (this.m_eButtonDockStyle == value)
          return;
        QTabButtonDockStyle eButtonDockStyle = this.m_eButtonDockStyle;
        this.m_eButtonDockStyle = value;
        try
        {
          this.NotifyDockChanged(eButtonDockStyle, this.m_eButtonDockStyle);
        }
        catch
        {
          this.m_eButtonDockStyle = eButtonDockStyle;
          throw;
        }
      }
    }

    [Category("QButtonAppearance")]
    [Localizable(true)]
    [Description("Gets or sets the text on the ToolTip of the TabButton. This must contain Xml as used with QMarkupText")]
    [DefaultValue(null)]
    public string ButtonToolTipText
    {
      get => this.TabButton.ToolTipText;
      set => this.TabButton.ToolTipText = value;
    }

    public bool ShouldSerializeButtonBackgroundImage() => this.TabButton.ShouldSerializeBackgroundImage();

    public void ResetButtonBackgroundImage() => this.TabButton.ResetBackgroundImage();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the BackgroundImage of the QTabPage")]
    [Category("QButtonAppearance")]
    public Image ButtonBackgroundImage
    {
      get => this.TabButton.BackgroundImage;
      set => this.TabButton.BackgroundImage = value;
    }

    [DefaultValue(null)]
    [Description("Gets or sets a possible resource name to load the BackgroundImage from. This must be in the format 'NameSpace.IconName.extension, AssemblyName'")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("QButtonAppearance")]
    public string ButtonBackgroundImageResourceName
    {
      get => this.TabButton.BackgroundImageResourceName;
      set => this.TabButton.BackgroundImageResourceName = value;
    }

    public bool ShouldSerializeIcon() => this.TabButton.ShouldSerializeIcon();

    public void ResetIcon() => this.TabButton.ResetIcon();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the Icon of the QTabPage")]
    [Category("QButtonAppearance")]
    public Icon Icon
    {
      get => this.TabButton.Icon;
      set => this.TabButton.Icon = value;
    }

    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [Category("QButtonAppearance")]
    public string IconResourceName
    {
      get => this.TabButton.IconResourceName;
      set => this.TabButton.IconResourceName = value;
    }

    public bool ShouldSerializeDisabledIcon() => this.TabButton.ShouldSerializeDisabledIcon();

    public void ResetDisabledIcon() => this.TabButton.ResetDisabledIcon();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("QButtonAppearance")]
    [Description("Gets or sets the DisabledIcon of the QTabPage. When this is not set the default Icon is used for painting.")]
    public Icon DisabledIcon
    {
      get => this.TabButton.DisabledIcon;
      set => this.TabButton.DisabledIcon = value;
    }

    [DefaultValue(null)]
    [Category("QButtonAppearance")]
    [Description("Gets or sets a possible resource name to load the DisabledIcon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string DisabledIconResourceName
    {
      get => this.TabButton.DisabledIconResourceName;
      set => this.TabButton.DisabledIconResourceName = value;
    }

    public bool Close()
    {
      if (this.TabControl == null)
        return true;
      CancelEventArgs e = new CancelEventArgs(false);
      this.RaiseClosing(e);
      if (e.Cancel)
        return false;
      if (this.TabControl.TabPageCloseBehavior == QTabPageCloseBehavior.Dispose)
      {
        this.TabControl.Controls.Remove((Control) this);
        this.Dispose();
      }
      else
        this.ButtonVisible = false;
      this.RaiseClosed(EventArgs.Empty);
      return true;
    }

    public virtual bool ShouldLayoutWhenInvisible() => this.ButtonConfiguration.Appearance.UseControlBackgroundForTabButton || this.ButtonConfiguration.AppearanceHot.UseControlBackgroundForTabButton || this.ButtonConfiguration.AppearanceActive.UseControlBackgroundForTabButton;

    protected override int ClientAreaMarginTop => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderTop ? 0 : this.Appearance.BorderWidth;

    protected override int ClientAreaMarginLeft => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderLeft ? 0 : this.Appearance.BorderWidth;

    protected override int ClientAreaMarginRight => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderRight ? 0 : this.Appearance.BorderWidth;

    protected override int ClientAreaMarginBottom => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderBottom ? 0 : this.Appearance.BorderWidth;

    protected override string BackColorPropertyName => "TabPageBackground1";

    protected override string BackColor2PropertyName => "TabPageBackground2";

    protected override string BorderColorPropertyName => "TabPageBorder";

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QTabControl TabControl
    {
      get => this.m_oTabControl;
      set
      {
        if (this.m_oTabControl == value)
          return;
        if (this.m_oTabControl != null)
          this.m_oTabControl.Controls.Remove((Control) this);
        value?.Controls.Add((Control) this);
      }
    }

    internal void NotifyDockChanged(QTabButtonDockStyle fromDock, QTabButtonDockStyle toDock)
    {
      if (this.m_oTabControl == null)
        return;
      this.m_oTabControl.HandleTabButtonDockChange(this, fromDock, toDock);
    }

    internal void SetButtonDockStyleWithoutChange(QTabButtonDockStyle dockStyle) => this.m_eButtonDockStyle = dockStyle;

    internal void PutTabControl(QTabControl value) => this.m_oTabControl = value;

    internal new void UpdateBounds() => base.UpdateBounds();

    public new void UpdateBounds(int x, int y, int width, int height) => base.UpdateBounds(x, y, width, height);

    public Color RetrieveFirstDefinedColor(string colorName) => this.ColorScheme[colorName].Current;

    protected override void Select(bool directed, bool forward)
    {
      if (this.m_bActivating)
        return;
      this.m_bActivating = true;
      if (!this.Visible)
        this.Visible = true;
      if (this.CanFocusTabButton && this.ActiveControl != null)
        this.ActiveControl = (Control) null;
      if (this.GetStyle(ControlStyles.Selectable) && !this.DesignMode)
        base.Select(!this.CanFocusTabButton, forward);
      if (!this.TabButton.IsActivated && this.TabButton.TabStrip != null)
        this.TabButton.TabStrip.SetActiveButton(this.TabButton, true, false, true);
      this.m_bActivating = false;
      this.OnActivated(EventArgs.Empty);
    }

    protected override void OnColorsChanged(EventArgs e) => base.OnColorsChanged(e);

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      if (this.TabControl == null || !this.TabControl.Enabled)
        return;
      this.TabButton.Enabled = this.Enabled;
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      if (!this.CanFocusTabButton)
        return;
      this.TabButton.PutFocused(true);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.TabButton.PutFocused(false);
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (this.VisibleChanged == null)
        return;
      this.VisibleChanged((object) this, e);
    }

    internal void RaiseClosing(CancelEventArgs e) => this.OnClosing(e);

    internal void RaiseClosed(EventArgs e) => this.OnClosed(e);

    protected virtual void OnActivated(EventArgs e) => this.m_oActivatedDelegate = QWeakDelegate.InvokeDelegate(this.m_oActivatedDelegate, (object) this, (object) e);

    protected virtual void OnDeactivated(EventArgs e) => this.m_oDeactivatedDelegate = QWeakDelegate.InvokeDelegate(this.m_oDeactivatedDelegate, (object) this, (object) e);

    protected virtual void OnClosing(CancelEventArgs e) => this.m_oClosingDelegate = QWeakDelegate.InvokeDelegate(this.m_oClosingDelegate, (object) this, (object) e);

    protected virtual void OnClosed(EventArgs e) => this.m_oClosedDelegate = QWeakDelegate.InvokeDelegate(this.m_oClosedDelegate, (object) this, (object) e);

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this.m_oTabButton == null)
        return;
      this.m_oTabButton.Dispose();
      this.m_oTabButton = (QTabButton) null;
    }

    Rectangle IQTabButtonSource.GetBoundsForBackgroundFill() => this.TabControl != null ? this.TabControl.ContentShapeBounds : this.Bounds;

    void IQTabButtonSource.DeactivateSource() => this.DeactivateTabPageCore();

    void IQTabButtonSource.ActivateSource() => this.ActivateTabPageCore();

    void IQTabButtonSource.HandleScrollStep() => this.HandleScrollStep();

    protected virtual void HandleScrollStep()
    {
    }

    protected virtual void DeactivateTabPageCore()
    {
      this.Visible = false;
      this.OnDeactivated(EventArgs.Empty);
    }

    protected virtual void ActivateTabPageCore() => this.Select();

    protected virtual void UpdateTabButtonPaintParams(QTabButtonPaintParams paintParams)
    {
      paintParams.ButtonBackground1 = this.RetrieveFirstDefinedColor("TabButtonBackground1");
      paintParams.ButtonBackground2 = this.RetrieveFirstDefinedColor("TabButtonBackground2");
      paintParams.ButtonBorder = this.RetrieveFirstDefinedColor("TabButtonBorder");
      paintParams.ButtonText = this.RetrieveFirstDefinedColor("TabButtonText");
      paintParams.ButtonTextDisabled = this.RetrieveFirstDefinedColor("TabButtonTextDisabled");
      paintParams.ButtonShade = this.RetrieveFirstDefinedColor("TabButtonShade");
      paintParams.ButtonActiveBackground1 = this.RetrieveFirstDefinedColor("TabButtonActiveBackground1");
      paintParams.ButtonActiveBackground2 = this.RetrieveFirstDefinedColor("TabButtonActiveBackground2");
      paintParams.ButtonActiveBorder = this.RetrieveFirstDefinedColor("TabButtonActiveBorder");
      paintParams.ButtonActiveText = this.RetrieveFirstDefinedColor("TabButtonActiveText");
      paintParams.ButtonHotBackground1 = this.RetrieveFirstDefinedColor("TabButtonHotBackground1");
      paintParams.ButtonHotBackground2 = this.RetrieveFirstDefinedColor("TabButtonHotBackground2");
      paintParams.ButtonHotBorder = this.RetrieveFirstDefinedColor("TabButtonHotBorder");
      paintParams.ButtonHotText = this.RetrieveFirstDefinedColor("TabButtonHotText");
      paintParams.IconReplace = Color.FromArgb((int) byte.MaxValue, 0, 0);
      paintParams.IconReplaceColorWith = this.RetrieveFirstDefinedColor("TabButtonActiveText");
    }

    QTabButtonPaintParams IQTabButtonSource.RetrieveTabButtonPaintParams()
    {
      this.UpdateTabButtonPaintParams(this.m_oTabButtonPaintParams);
      return this.m_oTabButtonPaintParams;
    }

    [Category("QPersistence")]
    [Description("Gets or sets the PersistGuid. With this Guid the control is identified in the persistence files.")]
    public virtual Guid PersistGuid
    {
      get => this.m_oPersistGuid;
      set => this.m_oPersistGuid = value;
    }

    [Category("QPersistence")]
    [DefaultValue(true)]
    [Description("Gets or sets whether this object must be persisted.")]
    public bool PersistObject
    {
      get => this.m_bPersistObject;
      set => this.m_bPersistObject = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsPersisted
    {
      get => this.m_bIsPersisted;
      set => this.m_bIsPersisted = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool RequiresUnload => false;

    [Description("Gets or sets whether a new instance of this PersistableObject must be created when it is loaded from file. If this is false the persistableObject must match an existing persistableObject in the QPersistenceManager.PersistableObjects collection.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("QPersistence")]
    [DefaultValue(false)]
    [Browsable(false)]
    public bool CreateNew
    {
      get => false;
      set
      {
      }
    }

    string IQPersistableObject.Name
    {
      get => this.Name;
      set => this.Name = value;
    }

    public bool MustBePersistedAfter(IQPersistableObject persistableObject) => persistableObject != null && persistableObject == this.TabControl;

    public IXPathNavigable SavePersistableObject(
      QPersistenceManager manager,
      IXPathNavigable parentElement)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      if (this.IsPersisted)
        return (IXPathNavigable) null;
      IXPathNavigable persistableObjectElement = manager.CreatePersistableObjectElement((IQPersistableObject) this, parentElement);
      QXmlHelper.AddElement(persistableObjectElement, "buttonOrder", (object) this.ButtonOrder);
      QXmlHelper.AddElement(persistableObjectElement, "buttonDockStyle", (object) this.ButtonDockStyle);
      this.IsPersisted = true;
      return persistableObjectElement;
    }

    public bool LoadPersistableObject(
      QPersistenceManager manager,
      IXPathNavigable persistableObjectElement,
      object parentState)
    {
      QTabControl qtabControl = (QTabControl) null;
      this.ButtonDockStyle = (QTabButtonDockStyle) QXmlHelper.GetChildElementEnum(persistableObjectElement, "buttonDockStyle", typeof (QTabButtonDockStyle));
      this.ButtonOrder = QXmlHelper.GetChildElementInt(persistableObjectElement, "buttonOrder");
      var persistableObjectElement1 = parentState as IXPathNavigable;
      if (persistableObjectElement1 != null)
        qtabControl = manager.GetPersistableObject(persistableObjectElement1) as QTabControl;
      if (persistableObjectElement1 != null && (this.TabControl == null || this.TabControl != qtabControl))
        this.TabControl = qtabControl;
      return true;
    }

    public void UnloadPersistableObject()
    {
    }
  }
}
