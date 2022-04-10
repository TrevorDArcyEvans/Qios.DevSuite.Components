// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMenuItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeMenuItemDesigner), typeof (IDesigner))]
  public class QCompositeMenuItem : QCompositeItem, IQCompositeItemControl
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bIsDropDownHot;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeMenuItemIcon m_oIcon;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPart m_oCurrentContentPart;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeText m_oTitle;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeItemControl m_oControl;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeText m_oShortcut;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeImage m_oDropDownButton;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeSeparator m_oDropDownSplit;
    private QCompositeMenuItemContentType m_eContentType;

    protected QCompositeMenuItem(object sourceObject, QObjectClonerConstructOptions options)
      : base(QCompositeItemCreationOptions.None)
    {
    }

    public QCompositeMenuItem()
    {
      this.Items.SuspendChangeNotification();
      this.m_oIcon = new QCompositeMenuItemIcon(QCompositeItemCreationOptions.None);
      this.m_oIcon.ItemName = nameof (Icon);
      this.m_oIcon.Configuration = this.Configuration.IconConfiguration;
      this.Items.Add((IQPart) this.m_oIcon, false);
      this.m_eContentType = this.DefaultContentType;
      this.SecureContentPart(this.m_eContentType, false);
      this.m_oShortcut = new QCompositeText(QCompositeItemCreationOptions.None);
      this.m_oShortcut.Configuration = this.Configuration.ShortcutConfiguration;
      this.m_oShortcut.ItemName = nameof (Shortcut);
      this.Items.Add((IQPart) this.m_oShortcut, false);
      this.m_oDropDownSplit = new QCompositeSeparator(QCompositeItemCreationOptions.None);
      this.m_oDropDownSplit.ItemName = "DropDownSplit";
      this.m_oDropDownSplit.Configuration = this.Configuration.DropDownSplitConfiguration;
      this.m_oDropDownSplit.AutoSize = true;
      this.Items.Add((IQPart) this.m_oDropDownSplit, false);
      this.m_oDropDownButton = new QCompositeImage(QCompositeItemCreationOptions.None);
      this.m_oDropDownButton.Configuration = (QCompositeImageConfiguration) this.Configuration.DropDownButtonConfiguration;
      this.m_oDropDownButton.ItemName = "DropDownButton";
      this.Items.Add((IQPart) this.m_oDropDownButton, false);
      this.ConfigureSharedItemsProperties();
      this.Items.ResumeChangeNotification(false);
    }

    private void ConfigureSharedItemsProperties()
    {
      for (int index = 0; index < this.Parts.Count; ++index)
      {
        if (this.Parts[index] is QCompositeItemBase part)
          part.ColorHost = (IQItemColorHost) this;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QCompositeItemTemplate Template
    {
      get => (QCompositeItemTemplate) null;
      set
      {
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected virtual QCompositeMenuItemContentType DefaultContentType => QCompositeMenuItemContentType.Title;

    [Category("QBehavior")]
    [DefaultValue(QCompositeMenuItemContentType.Title)]
    [Description("Gets or sets the used content type. This is Title by default.")]
    public virtual QCompositeMenuItemContentType ContentType
    {
      get => this.m_eContentType;
      set
      {
        if (this.m_eContentType == value)
          return;
        this.SecureContentPart(value, true);
      }
    }

    protected virtual void SecureTitlePart()
    {
      if (this.m_oTitle != null)
        return;
      this.m_oTitle = new QCompositeText(QCompositeItemCreationOptions.None);
      this.m_oTitle.Configuration = this.Configuration.TitleConfiguration;
      this.m_oTitle.ColorHost = (IQItemColorHost) this;
      this.m_oTitle.ItemName = "Content";
    }

    protected virtual void SecureControlPart()
    {
      if (this.m_oControl != null)
        return;
      this.m_oControl = new QCompositeItemControl(QCompositeItemCreationOptions.None);
      this.m_oControl.Configuration = this.Configuration.ControlConfiguration;
      this.m_oControl.ColorHost = (IQItemColorHost) this;
      this.m_oControl.ItemName = "Content";
      this.m_oControl.Control = this.CreateControl();
    }

    protected virtual void SecureContentPart(
      QCompositeMenuItemContentType contentType,
      bool sortParts)
    {
      this.Parts.SuspendChangeNotification();
      this.m_eContentType = contentType;
      if (contentType == QCompositeMenuItemContentType.Title && !this.Parts.Contains((IQPart) this.m_oTitle))
      {
        if (this.m_oControl != null)
          this.Parts.Remove((IQPart) this.m_oControl);
        this.SecureTitlePart();
        this.Parts.Add((IQPart) this.m_oTitle, false);
        this.m_oCurrentContentPart = (IQPart) this.m_oTitle;
      }
      else if (contentType == QCompositeMenuItemContentType.Control && !this.Parts.Contains((IQPart) this.m_oControl))
      {
        if (this.m_oTitle != null)
          this.Parts.Remove((IQPart) this.m_oTitle);
        this.SecureControlPart();
        this.Parts.Add((IQPart) this.m_oControl, false);
        this.m_oCurrentContentPart = (IQPart) this.m_oControl;
      }
      if (sortParts)
        this.Parts.SortParts(true);
      this.Parts.ResumeChangeNotification(true);
    }

    public override object Clone()
    {
      QCompositeMenuItem qcompositeMenuItem = base.Clone() as QCompositeMenuItem;
      qcompositeMenuItem.m_oIcon = qcompositeMenuItem.Parts["Icon"] as QCompositeMenuItemIcon;
      if (qcompositeMenuItem.m_oIcon != null)
        qcompositeMenuItem.m_oIcon.Configuration = qcompositeMenuItem.Configuration.IconConfiguration;
      qcompositeMenuItem.m_oCurrentContentPart = qcompositeMenuItem.Parts["Content"];
      if (qcompositeMenuItem.ContentType == QCompositeMenuItemContentType.Title)
      {
        qcompositeMenuItem.m_oTitle = qcompositeMenuItem.m_oCurrentContentPart as QCompositeText;
        if (qcompositeMenuItem.m_oTitle != null)
          qcompositeMenuItem.m_oTitle.Configuration = qcompositeMenuItem.Configuration.TitleConfiguration;
      }
      else if (qcompositeMenuItem.ContentType == QCompositeMenuItemContentType.Control)
      {
        qcompositeMenuItem.m_oControl = qcompositeMenuItem.m_oCurrentContentPart as QCompositeItemControl;
        if (qcompositeMenuItem.m_oControl != null)
          qcompositeMenuItem.m_oControl.Configuration = qcompositeMenuItem.Configuration.ControlConfiguration;
      }
      qcompositeMenuItem.m_oShortcut = qcompositeMenuItem.Parts["Shortcut"] as QCompositeText;
      if (qcompositeMenuItem.m_oShortcut != null)
        qcompositeMenuItem.m_oShortcut.Configuration = qcompositeMenuItem.Configuration.ShortcutConfiguration;
      qcompositeMenuItem.m_oDropDownSplit = qcompositeMenuItem.Parts["DropDownSplit"] as QCompositeSeparator;
      if (qcompositeMenuItem.m_oDropDownSplit != null)
        qcompositeMenuItem.m_oDropDownSplit.Configuration = qcompositeMenuItem.Configuration.DropDownSplitConfiguration;
      qcompositeMenuItem.m_oDropDownButton = qcompositeMenuItem.Parts["DropDownButton"] as QCompositeImage;
      if (qcompositeMenuItem.m_oDropDownButton != null)
        qcompositeMenuItem.m_oDropDownButton.Configuration = (QCompositeImageConfiguration) qcompositeMenuItem.Configuration.DropDownButtonConfiguration;
      qcompositeMenuItem.ConfigureSharedItemsProperties();
      return (object) qcompositeMenuItem;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QCompositeSeparator DropDownSplitPart => this.m_oDropDownSplit;

    internal void PutDropDownSplitPart(QCompositeSeparator value) => this.m_oDropDownSplit = value;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QCompositeImage DropDownButtonPart => this.m_oDropDownButton;

    internal void PutDropDownButtonPart(QCompositeImage value) => this.m_oDropDownButton = value;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QCompositeIcon IconPart => (QCompositeIcon) this.m_oIcon;

    internal void PutIconPart(QCompositeMenuItemIcon value) => this.m_oIcon = value;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IQPart CurrentContentPart => this.m_oCurrentContentPart;

    internal void PutCurrentContentPart(IQPart value) => this.m_oCurrentContentPart = value;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QCompositeText TitlePart => this.m_oTitle;

    internal void PutTitlePart(QCompositeText value) => this.m_oTitle = value;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QCompositeText ShortcutPart => this.m_oShortcut;

    internal void PutShortcutPart(QCompositeText value) => this.m_oShortcut = value;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QCompositeItemControl ControlPart => this.m_oControl;

    internal void PutControlPart(QCompositeItemControl value) => this.m_oControl = value;

    [Description("Gets or sets the QColorScheme that is used")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [Description("Contains the Configuration.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeMenuItemConfiguration Configuration
    {
      get => base.Configuration as QCompositeMenuItemConfiguration;
      set => this.Configuration = (QCompositeItemConfiguration) value;
    }

    public override IQItemColorHost ColorHost
    {
      get => base.ColorHost;
      set
      {
        IQItemColorHost colorHost = base.ColorHost;
        base.ColorHost = value;
        if (!this.HasItems)
          return;
        for (int index = 0; index < this.Items.Count; ++index)
        {
          if (this.Items[index] is QCompositeItemBase qcompositeItemBase && qcompositeItemBase.ColorHost == colorHost)
            qcompositeItemBase.ColorHost = value;
        }
      }
    }

    protected virtual Control CreateControl() => (Control) null;

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      QPartShapePainter painter1 = (QPartShapePainter) new QCompositeMenuItemBackgroundPainter();
      painter1.DrawOnBounds = QPartBoundsType.Bounds;
      painter1.Options = QPainterOptions.FillBackground;
      painter1.Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.ReturnDrawnShape | QShapePainterOptions.StayWithinBounds);
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Background, (IQPartObjectPainter) painter1);
      QPartShapePainter painter2 = (QPartShapePainter) new QCompositeMenuItemForegroundPainter();
      painter2.DrawOnBounds = QPartBoundsType.Bounds;
      painter2.Options = QPainterOptions.FillForeground;
      painter2.Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds);
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Foreground, (IQPartObjectPainter) painter2);
      return currentPainters;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeMenuItemConfiguration();

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part != this || layoutStage != QPartLayoutStage.CalculatingSize)
        return;
      this.m_oDropDownButton.SetImage(this.Configuration.DropDownButtonConfiguration.UsedMask, false);
      this.SetChildPartsVisibility(layoutStage);
    }

    protected virtual void SetChildPartsVisibility(QPartLayoutStage layoutStage)
    {
      bool flag = this.Composite != null && this.Composite.Configuration.GetIconBackgroundVisible((IQPart) this.Composite) && this.LayoutEngine is QPartTableRowLayoutEngine;
      if (this.m_oIcon != null)
        this.m_oIcon.SetVisible(flag || this.m_oIcon.UsedIcon != null, false, false);
      if (this.m_oTitle != null)
        this.m_oTitle.SetVisible(!QMisc.IsEmpty((object) this.m_oTitle.Title), false, false);
      if (this.m_oShortcut != null)
        this.m_oShortcut.SetVisible(!QMisc.IsEmpty((object) this.m_oShortcut.Title), false, false);
      if (this.m_oControl != null)
        this.m_oControl.SetVisible(this.m_oControl.Control != null, false, false);
      if (this.m_oDropDownButton != null)
        this.m_oDropDownButton.SetVisible(this.CanExpand, false, false);
      if (this.m_oDropDownSplit == null)
        return;
      this.m_oDropDownSplit.SetVisible(this.CanExpand && this.Configuration.DropDownSeparated, false, false);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QPartCollection Items => base.Items;

    protected internal override QPartCollection DesignablePartsCollection => (QPartCollection) null;

    private void UpdateShortcut()
    {
      if (this.Shortcut == Shortcut.None)
        this.m_oShortcut.Title = (string) null;
      else
        this.m_oShortcut.Title = this.ShortcutString;
    }

    [Category("QAppearance")]
    [Description("Indicates whether the QCompositeMenuItem is checked")]
    [DefaultValue(false)]
    public override bool Checked
    {
      get => this.m_oIcon.Checked;
      set
      {
        this.m_oIcon.Checked = value;
        this.HandleChange(true);
      }
    }

    protected override QItemStates GetState(
      QItemStates checkForStates,
      bool includeParentStates)
    {
      QItemStates state = base.GetState(checkForStates, includeParentStates);
      if (this.Configuration.CheckBehaviour == QCompositeMenuItemCheckBehaviour.CheckItem)
        state = QItemStatesHelper.AdjustState(state, QItemStates.Checked, this.Checked);
      return state;
    }

    public override Shortcut Shortcut
    {
      get => base.Shortcut;
      set
      {
        base.Shortcut = value;
        this.UpdateShortcut();
      }
    }

    public bool ShouldSerializeIcon() => this.m_oIcon.ShouldSerializeIcon();

    public void ResetIcon() => this.m_oIcon.ResetIcon();

    [Category("QAppearance")]
    [Description("Gets or sets the Icon of the QCompositeMenuItem")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [QXmlSave(QXmlSaveType.NeverSave)]
    public Icon Icon
    {
      get => this.m_oIcon.Icon;
      set => this.m_oIcon.Icon = value;
    }

    [Browsable(false)]
    public Icon UsedIcon => this.m_oIcon.UsedIcon;

    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    [Category("QAppearance")]
    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    public string IconResourceName
    {
      get => this.m_oIcon.IconResourceName;
      set => this.m_oIcon.IconResourceName = value;
    }

    [Browsable(false)]
    [Description("Gets whether the Icon is loaded from a resource")]
    public bool IconLoadedFromResource => this.m_oIcon.IconLoadedFromResource;

    public bool ShouldSerializeDisabledIcon() => this.m_oIcon.ShouldSerializeDisabledIcon();

    public void ResetDisabledIcon() => this.m_oIcon.ResetDisabledIcon();

    [Category("QAppearance")]
    [Description("Gets or sets the Icon of the QCompositeMenuItem")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [QXmlSave(QXmlSaveType.NeverSave)]
    public Icon DisabledIcon
    {
      get => this.m_oIcon.DisabledIcon;
      set => this.m_oIcon.DisabledIcon = value;
    }

    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string DisabledIconResourceName
    {
      get => this.m_oIcon.DisabledIconResourceName;
      set => this.m_oIcon.DisabledIconResourceName = value;
    }

    [Browsable(false)]
    [Description("Gets whether the Icon is loaded from a resource")]
    public bool DisabledIconLoadedFromResource => this.m_oIcon.DisabledIconLoadedFromResource;

    public bool ShouldSerializeCheckedIcon() => this.m_oIcon.ShouldSerializeCheckedIcon();

    public void ResetCheckedIcon() => this.m_oIcon.ResetCheckedIcon();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the Icon of the QCompositeMenuItem")]
    [Category("QAppearance")]
    [QXmlSave(QXmlSaveType.NeverSave)]
    public Icon CheckedIcon
    {
      get => this.m_oIcon.CheckedIcon;
      set => this.m_oIcon.CheckedIcon = value;
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    public string CheckedIconResourceName
    {
      get => this.m_oIcon.CheckedIconResourceName;
      set => this.m_oIcon.CheckedIconResourceName = value;
    }

    [Description("Gets whether the Icon is loaded from a resource")]
    [Browsable(false)]
    public bool CheckedIconLoadedFromResource => this.m_oIcon.CheckedIconLoadedFromResource;

    [Localizable(true)]
    [Description("Gets or sets the title of the QCompositeItem")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [QDesignerMainText(true)]
    public string Title
    {
      get => this.m_oTitle == null ? (string) null : this.m_oTitle.Title;
      set
      {
        bool flag = this.HotkeyText == char.ToUpper(QHotkeyHelper.FindHotkeyChar(this.Title)).ToString();
        if (value != null && this.m_oTitle == null)
          this.SecureTitlePart();
        if (this.m_oTitle == null)
          return;
        this.m_oTitle.Title = value;
        if (this.HotkeyText != null && this.HotkeyText.Length != 0 && !flag)
          return;
        char hotkeyChar = QHotkeyHelper.FindHotkeyChar(value);
        if (hotkeyChar <= char.MinValue)
          return;
        this.HotkeyText = char.ToUpper(hotkeyChar).ToString();
      }
    }

    [Browsable(false)]
    public string DisplayedTitle => this.m_oTitle == null ? (string) null : this.m_oTitle.DisplayedTitle;

    [Category("QBehavior")]
    [DefaultValue(typeof (Size), "0,0")]
    [Description("Gets or sets the prefered size of the control")]
    public Size ControlSize
    {
      get => this.m_oControl == null ? Size.Empty : this.m_oControl.ControlSize;
      set
      {
        if (this.m_oControl == null && value != Size.Empty)
          this.SecureControlPart();
        this.m_oControl.ControlSize = value;
      }
    }

    [Description("Gets or sets a possible Control that is displayed on this item.")]
    [Category("QBehavior")]
    [DefaultValue(null)]
    public virtual Control Control
    {
      get => this.m_oControl == null ? (Control) null : this.m_oControl.Control;
      set
      {
        if (value != null && this.m_oControl == null)
          this.SecureContentPart(QCompositeMenuItemContentType.Control, true);
        if (this.m_oControl == null)
          return;
        this.m_oControl.Control = value;
      }
    }

    [Browsable(false)]
    internal bool IsDropDownAreaHot => this.m_bIsDropDownHot;

    [Browsable(false)]
    internal Rectangle DropDownArea
    {
      get
      {
        Rectangle bounds = this.CalculatedProperties.GetBounds(QPartBoundsType.Bounds);
        if (this.Configuration.Direction == QPartDirection.Horizontal)
          return Rectangle.FromLTRB(this.m_oDropDownSplit.CalculatedProperties.Bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
        return this.Configuration.Direction == QPartDirection.Vertical ? Rectangle.FromLTRB(bounds.Left, this.m_oDropDownSplit.CalculatedProperties.Bounds.Top, bounds.Right, bounds.Bottom) : Rectangle.Empty;
      }
    }

    [Browsable(false)]
    internal Rectangle ScrollCorrectedDropDownArea
    {
      get
      {
        Rectangle scrollCorrectedBounds = this.CalculatedProperties.CachedScrollCorrectedBounds;
        if (this.Configuration.Direction == QPartDirection.Horizontal)
          return Rectangle.FromLTRB(this.m_oDropDownSplit.CalculatedProperties.CachedScrollCorrectedBounds.Left, scrollCorrectedBounds.Top, scrollCorrectedBounds.Right, scrollCorrectedBounds.Bottom);
        return this.Configuration.Direction == QPartDirection.Vertical ? Rectangle.FromLTRB(scrollCorrectedBounds.Left, this.m_oDropDownSplit.CalculatedProperties.CachedScrollCorrectedBounds.Top, scrollCorrectedBounds.Right, scrollCorrectedBounds.Bottom) : Rectangle.Empty;
      }
    }

    protected internal override bool HandleMouseDown(MouseEventArgs e) => !this.Configuration.DropDownSeparated || this.ScrollCorrectedDropDownArea.Contains(e.X, e.Y) || QItemStatesHelper.IsExpanded(this.ItemState);

    protected internal override bool HandleMouseMove(MouseEventArgs e)
    {
      bool flag = this.ScrollCorrectedDropDownArea.Contains(e.X, e.Y);
      if (flag == this.m_bIsDropDownHot)
        return true;
      this.m_bIsDropDownHot = flag;
      this.HandleChange(false);
      return true;
    }

    protected internal override bool HandleMouseUp(MouseEventArgs e) => !this.IsExpanded || this.Configuration.DropDownSeparated && !this.ScrollCorrectedDropDownArea.Contains(e.X, e.Y);

    internal QColorSet GetCheckedColorSet() => this.ColorHost.GetItemColorSet((object) this, QItemStates.Checked, (object) null);
  }
}
