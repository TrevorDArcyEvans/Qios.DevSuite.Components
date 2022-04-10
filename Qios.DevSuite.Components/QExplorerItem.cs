// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QExplorerItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  [TypeConverter(typeof (QExplorerItemConverter))]
  [Designer(typeof (QExplorerItemDesigner), typeof (IDesigner))]
  public class QExplorerItem : QMenuItem
  {
    private bool m_bControlStretched;
    private bool m_bPersonalized = true;
    private QColorScheme m_oColorScheme;
    private EventHandler m_oColorSchemeColorsChangedEventHandler;
    private QExplorerBarItemAlternateConfiguration m_oAlternateConfiguration;
    private Size m_oIconSize;
    private Image m_oGroupPanelBackgroundImage;
    private QImageAlign m_eGroupPanelBackgroundImageAlign = QImageAlign.BottomRight;
    private QExplorerItemType m_eItemType = QExplorerItemType.MenuItem;
    private QExplorerItemState m_eItemState = QExplorerItemState.Collapsed;
    private Rectangle m_oDepersonalizeItemBounds;
    private Rectangle m_oPanelBounds;
    private Rectangle m_oGroupBounds;
    private int m_iPositionOffset;
    private int m_iMotionOffset;
    private int m_iMotionHeight;
    private int m_iMotionStep;
    private Bitmap m_oExpandedBitmap;

    public QExplorerItem() => this.InternalConstruct();

    public QExplorerItem(IContainer container)
      : base(container)
    {
      this.InternalConstruct();
    }

    public QExplorerItem(bool separator)
      : base(separator)
    {
      this.InternalConstruct();
    }

    public QExplorerItem(string title)
      : base(title)
    {
      this.InternalConstruct();
    }

    public QExplorerItem(string title, string name)
      : base(title, name)
    {
      this.InternalConstruct();
    }

    public QExplorerItem(string title, string itemName, Icon icon, Shortcut shortcut)
      : base(title, itemName, icon, shortcut)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.OrientationForFlow = true;
      this.m_oAlternateConfiguration = this.CreateAlternateConfigurationInstance();
      this.m_oColorSchemeColorsChangedEventHandler = new EventHandler(this.ColorScheme_ColorsChanged);
      this.m_oColorScheme = this.CreateColorSchemeInstance();
      if (this.m_oColorScheme == null)
        return;
      this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
    }

    public bool ShouldSerializeColorScheme() => this.ColorScheme.ShouldSerialize();

    public void ResetColorScheme() => this.ColorScheme.Reset();

    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QColorScheme ColorScheme
    {
      get => this.m_oColorScheme;
      set
      {
        if (this.m_oColorScheme == value)
          return;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
        this.m_oColorScheme = value;
        if (this.m_oColorScheme == null)
          return;
        this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    public void ResetConfiguration()
    {
      if (this.m_oAlternateConfiguration == null)
        return;
      this.m_oAlternateConfiguration.SetToDefaultValues();
    }

    public bool ShouldSerializeConfiguration() => this.m_oAlternateConfiguration != null && !this.m_oAlternateConfiguration.IsSetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the configuration for this QExplorerItem.")]
    [Category("QAppearance")]
    public QExplorerBarItemAlternateConfiguration Configuration => this.m_oAlternateConfiguration;

    internal QExplorerBarItemAlternateConfiguration UsedConfiguration
    {
      get
      {
        QExplorerBar parentExplorerBar = this.GetParentExplorerBar();
        if (this.Configuration != null)
          return this.Configuration;
        if (this.ItemType == QExplorerItemType.GroupItem && parentExplorerBar != null)
          return parentExplorerBar.GroupItemConfiguration.AlternateConfiguration;
        return this.ItemType == QExplorerItemType.MenuItem && parentExplorerBar != null ? parentExplorerBar.ItemConfiguration.AlternateConfiguration : (QExplorerBarItemAlternateConfiguration) null;
      }
    }

    [Browsable(false)]
    public virtual bool Personalized => this.m_bPersonalized;

    internal void PutPersonalized(bool value)
    {
      this.m_bPersonalized = value;
      this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      if (value)
        return;
      this.GetParentExplorerBar()?.PutAutoScrollItem(this);
    }

    internal bool ShowDepersonalizeItem
    {
      get
      {
        QExplorerBar parentExplorerBar = this.GetParentExplorerBar();
        return parentExplorerBar != null && parentExplorerBar.GroupItemConfiguration.PersonalizedItemBehavior == QPersonalizedItemBehavior.DependsOnPersonalized && this.Personalized && this.MenuItems.HasPersonalizedItems(false) && this.Expanded && this.ItemType == QExplorerItemType.GroupItem;
      }
    }

    [Browsable(false)]
    public Rectangle DepersonalizeItemBounds => this.m_oDepersonalizeItemBounds;

    internal void PutDepersonalizeItemBounds(Rectangle value) => this.m_oDepersonalizeItemBounds = value;

    internal QExplorerItemState ItemState
    {
      get => this.m_eItemState;
      set
      {
        this.m_eItemState = value;
        if (this.m_eItemState == QExplorerItemState.Collapsed)
          this.PutPersonalized(true);
        if (this.InMotion)
        {
          this.m_iMotionOffset = 0;
          this.m_iMotionHeight = 0;
          this.m_iMotionStep = 0;
        }
        else
          this.CalculateControlBoundsProperties(true);
      }
    }

    [DefaultValue(QExplorerItemType.MenuItem)]
    [Description("Gets or sets the ItemType of the QExplorerItem.")]
    [Category("QBehavior")]
    internal QExplorerItemType ItemType
    {
      get => this.m_eItemType;
      set
      {
        this.m_eItemType = value;
        if (value == QExplorerItemType.MenuItem && this.Expanded)
          this.ItemState = QExplorerItemState.Collapsed;
        this.UpdateConfigurationBase();
        if (this.Configuration != null)
          this.Configuration.RedefineProperties(this.m_eItemType);
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    public override QCommandControlContainer Control
    {
      get => base.Control;
      set
      {
        if (!this.Expanded & value != null && value.Visible)
          value.Visible = false;
        base.Control = value;
      }
    }

    [Browsable(false)]
    public override bool IsEnabled
    {
      get
      {
        bool isEnabled = base.IsEnabled;
        if (this.ParentCommand is QExplorerItem && !((QMenuItem) this.ParentCommand).IsEnabled)
          isEnabled = false;
        return isEnabled;
      }
    }

    [Browsable(false)]
    public override bool IsVisible => this.IsVisibleForFocus && (this.VisibleWhenPersonalized || !(this.ParentCommand is QExplorerItem) || ((QExplorerItem) this.ParentCommand).ShowPersonalizedItems);

    [Browsable(false)]
    internal virtual bool IsVisibleForFocus
    {
      get
      {
        if (!this.Visible || !this.UserHasRightToExecute && this.UserRightBehavior == QCommandUserRightBehavior.HideWhenNoRight)
          return false;
        if (this.IsSeparator && this.ParentMenuItemCollection != null)
        {
          QMenuItem previousVisibleItem = this.PreviousVisibleItem;
          if (previousVisibleItem == null || previousVisibleItem.IsSeparator)
            return false;
        }
        return !(this.ParentCommand is QExplorerItem) || ((QMenuItem) this.ParentCommand).IsVisible;
      }
    }

    [Browsable(false)]
    internal bool ShowPersonalizedItems
    {
      get
      {
        if (!(this.ParentContainer is QExplorerBar parentContainer))
          return false;
        return parentContainer.GroupItemConfiguration.PersonalizedItemBehavior == QPersonalizedItemBehavior.DependsOnPersonalized ? !this.Personalized : parentContainer.GroupItemConfiguration.PersonalizedItemBehavior == QPersonalizedItemBehavior.AlwaysVisible;
      }
    }

    [Browsable(false)]
    public override bool IsAccessible => base.IsAccessible && this.IsEnabled && this.Visible;

    [Description("Gets or sets if the QCommandControlContainer must be stretched horizontally")]
    [DefaultValue(false)]
    [Category("QAppearance")]
    public bool ControlStretched
    {
      get => this.m_bControlStretched;
      set
      {
        this.m_bControlStretched = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Description("Gets or sets the alignment of the GroupPanelBackgroundImage for this QExplorerItem")]
    [DefaultValue(QImageAlign.BottomRight)]
    [Category("QAppearance")]
    public QImageAlign GroupPanelBackgroundImageAlign
    {
      get => this.m_eGroupPanelBackgroundImageAlign;
      set
      {
        this.m_eGroupPanelBackgroundImageAlign = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the background image of the group panel.")]
    public Image GroupPanelBackgroundImage
    {
      get => this.m_oGroupPanelBackgroundImage;
      set
      {
        this.m_oGroupPanelBackgroundImage = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Description("Contains the collection of ExplorerItems of this ExplorerItem")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QExplorerItemCollectionEditor), typeof (UITypeEditor))]
    [Category("QBehavior")]
    public override QMenuItemCollection MenuItems => (QMenuItemCollection) this.Commands;

    [Browsable(false)]
    public override bool MouseIsOverMenuItem
    {
      get
      {
        if (this.ParentContainer == null || !(this.ParentContainer is System.Windows.Forms.Control))
          return false;
        if (!(this.ParentContainer is QExplorerBar parentContainer) && this.ParentContainer != null)
          return base.MouseIsOverMenuItem;
        return parentContainer != null && parentContainer.Visible && this.Bounds.Contains(parentContainer.ContainerPointToClient(System.Windows.Forms.Control.MousePosition));
      }
    }

    [Browsable(false)]
    internal virtual bool MouseIsOverMenuItemHasChildItemsHotBounds => this.ParentContainer != null && this.ParentContainer is System.Windows.Forms.Control && this.ParentContainer is QExplorerBar parentContainer && parentContainer.Visible && this.HasChildItemsHotBounds.Contains(parentContainer.ContainerPointToClient(System.Windows.Forms.Control.MousePosition));

    protected override IQCommandContainer CreateChildCommandContainer() => this.ItemType == QExplorerItemType.GroupItem ? (IQCommandContainer) null : base.CreateChildCommandContainer();

    protected internal override void SetParent(IQCommandContainer container, QCommand command)
    {
      base.SetParent(container, command);
      this.ItemType = command != null || !(container is QExplorerBar) ? QExplorerItemType.MenuItem : QExplorerItemType.GroupItem;
      this.UpdateConfigurationBase();
    }

    internal override void SavePropertiesToXml(IXPathNavigable element, QMenuItemSaveType saveType)
    {
      base.SavePropertiesToXml(element, saveType);
      if (this.ItemType != QExplorerItemType.GroupItem)
        return;
      QXmlHelper.AddElement(element, "expanded", (object) this.Expanded);
      QXmlHelper.AddElement(element, "personalized", (object) this.Personalized);
    }

    public override void LoadFromXml(IXPathNavigable itemElement, QMenuItemLoadType loadType)
    {
      base.LoadFromXml(itemElement, loadType);
      if (this.ItemType != QExplorerItemType.GroupItem)
        return;
      this.PutPersonalized(QXmlHelper.GetChildElementBool(itemElement, "personalized"));
    }

    internal Color RetrieveFirstDefinedColor(string colorName, QColorScheme parentColorScheme)
    {
      if (this.ColorScheme == null)
        return (Color) parentColorScheme[colorName];
      QColor qcolor = this.ColorScheme[colorName];
      return qcolor.ContainsColorForCurrentTheme() || parentColorScheme == null ? (Color) qcolor : (Color) parentColorScheme[colorName];
    }

    internal QExplorerBar GetParentExplorerBar()
    {
      System.Windows.Forms.Control parentControl = this.ParentControl;
      for (QCommand parentCommand = this.ParentCommand; parentControl == null && parentCommand != null; parentCommand = parentCommand.ParentCommand)
        parentControl = parentCommand.ParentControl;
      return parentControl as QExplorerBar;
    }

    internal void UpdateConfigurationBase()
    {
      if (this.Configuration == null)
        return;
      QExplorerBar parentExplorerBar = this.GetParentExplorerBar();
      if (parentExplorerBar != null)
      {
        if (this.ItemType == QExplorerItemType.GroupItem)
          this.Configuration.Properties.BaseProperties = parentExplorerBar.GroupItemConfiguration.AlternateConfiguration.Properties;
        else if (this.ItemType == QExplorerItemType.MenuItem)
          this.Configuration.Properties.BaseProperties = parentExplorerBar.ItemConfiguration.AlternateConfiguration.Properties;
        else
          this.Configuration.Properties.BaseProperties = (QFastPropertyBag) null;
      }
      else
        this.Configuration.Properties.BaseProperties = (QFastPropertyBag) null;
    }

    protected virtual QExplorerBarItemAlternateConfiguration CreateAlternateConfigurationInstance() => new QExplorerBarItemAlternateConfiguration(this.ItemType);

    protected virtual QColorScheme CreateColorSchemeInstance() => new QColorScheme(false);

    protected override QCommandCollection CreateCommandCollection() => (QCommandCollection) new QMenuItemCollection(this.ParentContainer, (QCommand) this);

    internal override Size CalculateIconSizes(Size preferredSize)
    {
      this.m_oIconSize = base.CalculateIconSizes(preferredSize);
      return this.ParentContainer is QExplorerBar parentContainer && parentContainer.GroupItemConfiguration.IconOverlaps && this.ItemType == QExplorerItemType.GroupItem ? new Size(this.m_oIconSize.Width, 0) : this.m_oIconSize;
    }

    [Description("Determines if the QExplorerItem is expanded or collapsed")]
    [Category("QBehavior")]
    [DefaultValue(false)]
    public bool Expanded
    {
      get => this.m_eItemState == QExplorerItemState.Expanded || this.m_eItemState == QExplorerItemState.Expanding;
      set
      {
        this.ItemState = !value ? QExplorerItemState.Collapsed : QExplorerItemState.Expanded;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    internal override bool IsExpanded => this.Expanded;

    internal bool InMotion => this.m_eItemState == QExplorerItemState.Expanding || this.m_eItemState == QExplorerItemState.Collapsing;

    internal bool InMovement => this.PositionOffset != 0;

    internal int PositionOffset
    {
      get => this.m_iPositionOffset;
      set
      {
        if (!this.InMovement && value != 0 && this.Control != null && this.Control.Bitmap == null && this.ParentCommand == null)
          this.Control.CreateBitmap();
        this.PutOuterBounds(QMath.SetY(this.OuterBounds, this.OuterBounds.Y - (this.m_iPositionOffset - value)));
        this.PutGroupBounds(QMath.SetY(this.GroupBounds, this.GroupBounds.Y - (this.m_iPositionOffset - value)));
        this.PutPanelBounds(QMath.SetY(this.PanelBounds, this.PanelBounds.Y - (this.m_iPositionOffset - value)));
        this.PutDepersonalizeItemBounds(QMath.SetY(this.DepersonalizeItemBounds, this.DepersonalizeItemBounds.Y - (this.m_iPositionOffset - value)));
        this.m_iPositionOffset = value;
        this.CalculateControlBoundsProperties(false);
        for (int index = 0; index < this.MenuItems.Count; ++index)
        {
          if (this.MenuItems[index] is QExplorerItem)
            (this.MenuItems[index] as QExplorerItem).PositionOffset = value;
        }
      }
    }

    internal void ApplyPositionOffset()
    {
      this.PutOuterBounds(QMath.SetY(this.OuterBounds, this.OuterBounds.Y + this.m_iPositionOffset));
      this.PutGroupBounds(QMath.SetY(this.GroupBounds, this.GroupBounds.Y + this.m_iPositionOffset));
      this.PutPanelBounds(QMath.SetY(this.PanelBounds, this.PanelBounds.Y + this.m_iPositionOffset));
      this.PutDepersonalizeItemBounds(QMath.SetY(this.DepersonalizeItemBounds, this.DepersonalizeItemBounds.Y + this.m_iPositionOffset));
    }

    internal int MotionOffset
    {
      get => this.m_iMotionOffset;
      set => this.m_iMotionOffset = value;
    }

    internal int MotionHeight
    {
      get => this.m_iMotionHeight;
      set => this.m_iMotionHeight = value;
    }

    internal int MotionStep
    {
      get => this.m_iMotionStep;
      set => this.m_iMotionStep = value;
    }

    [Browsable(false)]
    public Rectangle PanelBounds => this.m_oPanelBounds;

    internal void PutPanelBounds(Rectangle value) => this.m_oPanelBounds = value;

    public Rectangle GetIconBackgroundBounds(QCommandConfiguration configuration) => new Rectangle(this.IconBounds.X - configuration.IconSpacing.Before, this.IconBounds.Y - configuration.IconSpacing.Before, this.IconBounds.Width + configuration.IconSpacing.Before + configuration.IconSpacing.After, this.IconBounds.Height + configuration.IconSpacing.Before + configuration.IconSpacing.After);

    [Browsable(false)]
    public Rectangle GroupBounds => this.m_oGroupBounds;

    internal void PutGroupBounds(Rectangle value) => this.m_oGroupBounds = value;

    public Rectangle IconBoundsForPaint
    {
      get
      {
        if (this.IconBoundsOverlap <= 0)
          return this.IconBounds;
        Rectangle iconBounds = this.IconBounds;
        return QMath.SetHeight(QMath.SetY(iconBounds, iconBounds.Y - this.IconBoundsOverlap), this.m_oIconSize.Height);
      }
    }

    internal int IconBoundsOverlap => Math.Max(this.m_oIconSize.Height - this.IconBounds.Height, 0);

    public Rectangle GetGroupBoundsForPaint(bool forceDrawChildren) => this.InMotion && !forceDrawChildren ? QMath.SetHeight(this.GroupBounds, this.Bounds.Height) : this.GroupBounds;

    internal void Offset(int x, int y) => this.Offset(x, y, false);

    internal void Offset(int x, int y, bool childrenOnly)
    {
      if (!childrenOnly)
      {
        this.PutOuterBounds(QMath.SetY(this.OuterBounds, this.OuterBounds.Y + y));
        this.PutPanelBounds(QMath.SetY(this.PanelBounds, this.PanelBounds.Y + y));
        this.PutGroupBounds(QMath.SetY(this.GroupBounds, this.GroupBounds.Y + y));
        this.PutHasChildItemsHotBounds(QMath.SetY(this.HasChildItemsHotBounds, this.HasChildItemsHotBounds.Y + y));
        this.PutOuterBounds(QMath.SetX(this.OuterBounds, this.OuterBounds.X + x));
        this.PutPanelBounds(QMath.SetX(this.PanelBounds, this.PanelBounds.X + x));
        this.PutGroupBounds(QMath.SetX(this.GroupBounds, this.GroupBounds.X + x));
        this.PutHasChildItemsHotBounds(QMath.SetX(this.HasChildItemsHotBounds, this.HasChildItemsHotBounds.X + x));
      }
      for (int index = 0; index < this.MenuItems.Count; ++index)
      {
        QMenuItem menuItem = this.MenuItems[index];
        if (menuItem is QExplorerItem qexplorerItem)
        {
          qexplorerItem.Offset(x, y);
        }
        else
        {
          menuItem.PutOuterBounds(QMath.SetY(menuItem.OuterBounds, menuItem.OuterBounds.Y + y));
          menuItem.PutOuterBounds(QMath.SetX(menuItem.OuterBounds, menuItem.OuterBounds.X + x));
        }
      }
    }

    internal GraphicsPath GetExpandedBitmapBoundsForPaint(
      QExplorerBarGroupItemConfiguration configuration,
      int width)
    {
      QDrawRoundedRectangleOptions roundedRectangleOptions = QDrawRoundedRectangleOptions.None;
      if (configuration.ItemRoundedCornerTopLeft)
        roundedRectangleOptions |= QDrawRoundedRectangleOptions.TopLeft;
      if (configuration.ItemRoundedCornerTopRight)
        roundedRectangleOptions |= QDrawRoundedRectangleOptions.TopRight;
      if (configuration.ItemRoundedCornerBottomLeft)
        roundedRectangleOptions |= QDrawRoundedRectangleOptions.BottomLeft;
      if (configuration.ItemRoundedCornerBottomRight)
        roundedRectangleOptions |= QDrawRoundedRectangleOptions.BottomRight;
      if (this.ItemState == QExplorerItemState.Expanding)
        return QRoundedRectanglePainter.Default.GetRoundedRectanglePath(new Rectangle(0, this.GroupBounds.Y + this.Bounds.Height / 2 + (configuration.ShadeVisible ? Math.Abs(configuration.ShadePosition.Y) : 0), width, this.GroupBounds.Height - (this.MotionHeight - this.MotionOffset) + 2 - this.Bounds.Height / 2 + (configuration.ShadeVisible ? 2 * Math.Abs(configuration.ShadePosition.Y) : 0)), configuration.ItemRoundedCornerSize, QAppearanceForegroundOptions.DrawAllBorders, roundedRectangleOptions);
      return this.ItemState == QExplorerItemState.Collapsing ? QRoundedRectanglePainter.Default.GetRoundedRectanglePath(new Rectangle(0, this.GroupBounds.Y + this.Bounds.Height / 2 + (configuration.ShadeVisible ? Math.Abs(configuration.ShadePosition.Y) : 0), width, this.GroupBounds.Height + (this.MotionHeight - this.MotionOffset) + 2 - this.Bounds.Height / 2 + (configuration.ShadeVisible ? 2 * Math.Abs(configuration.ShadePosition.Y) : 0)), configuration.ItemRoundedCornerSize, QAppearanceForegroundOptions.DrawAllBorders, roundedRectangleOptions) : (GraphicsPath) null;
    }

    internal float ExpandedBitmapOpacityForPaint => this.ItemState == QExplorerItemState.Expanding ? (float) this.MotionOffset / (float) this.MotionHeight : (float) (1.0 - (double) this.MotionOffset / (double) this.MotionHeight);

    internal Bitmap ExpandedBitmap
    {
      get => this.m_oExpandedBitmap;
      set => this.m_oExpandedBitmap = value;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this.m_oColorScheme == null || this.m_oColorScheme.IsDisposed)
        return;
      this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
      this.m_oColorScheme.Dispose();
      this.m_oColorScheme = (QColorScheme) null;
    }

    private void ColorScheme_ColorsChanged(object sender, EventArgs e) => this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
  }
}
