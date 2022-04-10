// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBox
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QInputBox), "Resources.ControlImages.QInputBox.bmp")]
  [ToolboxItem(true)]
  public class QInputBox : TextBox, IQWeakEventPublisher, IQControlListenerClient
  {
    private IntPtr m_oBackgroundBrush = IntPtr.Zero;
    private bool m_bPaintingMultilineBackground;
    private QOffscreenBitmapSet m_oOffscreenClientSet;
    private long m_iAccelerationTimerID = 1025;
    private uint m_iAccelerationTimerInterval = 500;
    private long m_iAccelerationTickStart = -1;
    private int m_iAccelerationIndex = -1;
    private QInputBoxAccelerationCollection m_oAccelerations;
    private bool m_bConfigurationChangedDirty;
    private bool m_bConfigureNonClientAreaDirty;
    private int m_iWheelData;
    private bool m_bNumericTextUpdatedUser;
    private int m_iNumericTextUpdating;
    private Decimal m_iNumericValue;
    private Decimal m_iIncrement = 1M;
    private Decimal m_iMaximumValue = 100M;
    private Decimal m_iMinimumValue;
    private string m_sFormatString = "#,##0.00";
    private QInputBoxFormatStringType m_eFormatStringType;
    private QInputBox.QStringSource m_oStringSource;
    private uint m_iAutoCompleteFlagsSet;
    private int m_iSuspendAutoSelectItem;
    private CurrencyManager m_oDataManager;
    private BindingMemberInfo m_oDisplayMember;
    private BindingMemberInfo m_oValueMember;
    private object m_oDataSource;
    private bool m_bSorted;
    private object m_oSelectedItem;
    private bool m_bPaintTransparentBackground;
    private QColorScheme m_oChildCompositeColorScheme;
    private QInputBoxCompositeWindow m_oDropDownWindow;
    private QInputBoxCompositeWindow m_oCustomDropDownWindow;
    private QInputBoxObjectCollection m_oObjectCollection;
    private Rectangle m_oVScrollRect = Rectangle.Empty;
    private Rectangle m_oHScrollRect = Rectangle.Empty;
    private QBalloon m_oBalloon;
    private string m_sToolTipText;
    private QToolTipConfiguration m_oToolTipConfiguration;
    private QInputBoxPaintParams m_oPaintParams;
    private QInputBoxPainter m_oPainter;
    private string m_sCueText;
    private bool m_bHot;
    private QButtonArea m_oDropDownButton = (QButtonArea) new QInputBoxButtonArea(MouseButtons.Left);
    private QButtonArea m_oDownButton = (QButtonArea) new QInputBoxButtonArea(MouseButtons.Left);
    private QButtonArea m_oUpButton = (QButtonArea) new QInputBoxButtonArea(MouseButtons.Left);
    private bool m_bTrackingNonClientAreaMouse;
    private bool m_bWeakEventHandlers = true;
    private QColorScheme m_oColorScheme;
    private EventHandler m_oToolTipConfigurationChangedEventHandler;
    private EventHandler m_oColorSchemeColorsChangedEventHandler;
    private QInputBoxAppearance m_oAppearance;
    private QInputBoxAppearance m_oAppearanceHot;
    private QInputBoxAppearance m_oAppearanceFocused;
    private EventHandler m_oAppearanceChangedEventHandler;
    private QFloatingInputBoxWindowCompositeConfiguration m_oChildCompositeConfiguration;
    private QInputBoxConfiguration m_oConfiguration;
    private QCompositeInputBoxItemConfiguration m_oItemConfiguration;
    private QFloatingInputBoxWindowConfiguration m_oChildWindowConfiguration;
    private EventHandler m_oConfigurationChangedEventHandler;
    private EventHandler m_oInputBoxTextPaddingChangedEventHandler;
    private bool m_bAutoSize = true;
    private int m_iCalculatedSize = -1;
    private QWeakDelegate m_oButtonMouseUpDelegate;
    private QWeakDelegate m_oButtonMouseDownDelegate;
    private QWeakDelegate m_oButtonClickDelegate;
    private QWeakDelegate m_oColorsChangedDelegate;
    private QWeakDelegate m_oPaintNonClientAreaDelegate;
    private QWeakDelegate m_oNonClientAreaMouseDownDelegate;
    private QWeakDelegate m_oNonClientAreaDoubleClickDelegate;
    private QWeakDelegate m_oNonClientAreaMouseUpDelegate;
    private QWeakDelegate m_oNonClientAreaMouseMoveDelegate;
    private QWeakDelegate m_oNonClientAreaMouseLeaveDelegate;
    private QWeakDelegate m_oWindowsXPThemeChangedDelegate;
    private QWeakDelegate m_oSelectedIndexChangedDelegate;
    private QWeakDelegate m_oSelectedItemChangedDelegate;
    private QWeakDelegate m_oSelectedValueChangedDelegate;
    private QWeakDelegate m_oExpandingDelegate;
    private QWeakDelegate m_oExpandedDelegate;
    private QWeakDelegate m_oCollapsingDelegate;
    private QWeakDelegate m_oCollapsedDelegate;
    private QWeakDelegate m_oDataSourceChangedDelegate;
    private QWeakDelegate m_oDisplayMemberChangedDelegate;
    private QWeakDelegate m_oValueMemberChangedDelegate;
    private QWeakDelegate m_oDropDownWindowRequested;

    public QInputBox()
    {
      base.AutoSize = false;
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.m_oAppearanceChangedEventHandler = new EventHandler(this.Appearance_AppearanceChanged);
      this.m_oAppearance = this.CreateAppearanceInstance();
      this.m_oAppearanceHot = this.CreateAppearanceHotInstance();
      this.m_oAppearanceFocused = this.CreateAppearanceFocusedInstance();
      this.m_oAppearance.AppearanceChanged += this.m_oAppearanceChangedEventHandler;
      this.m_oAppearanceHot.Properties.BaseProperties = this.m_oAppearance.Properties;
      this.m_oAppearanceFocused.Properties.BaseProperties = this.m_oAppearance.Properties;
      this.m_oAppearanceHot.AppearanceChanged += this.m_oAppearanceChangedEventHandler;
      this.m_oAppearanceFocused.AppearanceChanged += this.m_oAppearanceChangedEventHandler;
      this.m_oToolTipConfigurationChangedEventHandler = new EventHandler(this.ToolTip_ConfigurationChanged);
      this.m_oToolTipConfiguration = this.CreateToolTipConfigurationInstance();
      this.m_oToolTipConfiguration.ConfigurationChanged += this.m_oToolTipConfigurationChangedEventHandler;
      this.m_oPainter = this.CreatePainter();
      this.m_oPainter.Win32Window = (IWin32Window) this;
      this.m_oConfigurationChangedEventHandler = new EventHandler(this.Configuration_ConfigurationChanged);
      this.m_oInputBoxTextPaddingChangedEventHandler = new EventHandler(this.Configuration_InputBoxTextPaddingChanged);
      this.m_oConfiguration = this.CreateConfigurationInstance();
      this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
      this.m_oConfiguration.InputBoxTextPaddingChanged += this.m_oInputBoxTextPaddingChangedEventHandler;
      this.m_oItemConfiguration = this.CreateItemConfigurationInstance();
      this.m_oChildWindowConfiguration = this.CreateChildWindowConfigurationInstance();
      this.m_oChildCompositeConfiguration = this.CreateChildCompositeConfiguration();
      this.m_oChildCompositeColorScheme = this.CreateChildCompositeColorScheme();
      this.m_oColorSchemeColorsChangedEventHandler = new EventHandler(this.ColorScheme_ColorsChanged);
      this.m_oColorScheme = new QColorScheme();
      this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
      this.BackColorBase = (Color) this.m_oColorScheme[this.BackColorPropertyName];
      this.m_oDropDownButton.ButtonStateChanged += new QButtonAreaEventHandler(this.Button_ButtonStateChanged);
      this.m_oDownButton.ButtonStateChanged += new QButtonAreaEventHandler(this.Button_ButtonStateChanged);
      this.m_oUpButton.ButtonStateChanged += new QButtonAreaEventHandler(this.Button_ButtonStateChanged);
    }

    [QWeakEvent]
    [Description("Is raised when the data source changes")]
    [Category("QEvents")]
    public event QInputBoxEventHandler DataSourceChanged
    {
      add => this.m_oDataSourceChangedDelegate = QWeakDelegate.Combine(this.m_oDataSourceChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oDataSourceChangedDelegate = QWeakDelegate.Remove(this.m_oDataSourceChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Is raised when the value member changes")]
    [Category("QEvents")]
    public event QInputBoxEventHandler ValueMemberChanged
    {
      add => this.m_oValueMemberChangedDelegate = QWeakDelegate.Combine(this.m_oValueMemberChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oValueMemberChangedDelegate = QWeakDelegate.Remove(this.m_oValueMemberChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Is raised when the display member changes")]
    [Category("QEvents")]
    public event QInputBoxEventHandler DisplayMemberChanged
    {
      add => this.m_oDisplayMemberChangedDelegate = QWeakDelegate.Combine(this.m_oDisplayMemberChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oDisplayMemberChangedDelegate = QWeakDelegate.Remove(this.m_oDisplayMemberChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Is raised when the drop down window is requested.")]
    [Category("QEvents")]
    public event QInputBoxCompositeWindowEventHandler DropDownWindowRequested
    {
      add => this.m_oDropDownWindowRequested = QWeakDelegate.Combine(this.m_oDropDownWindowRequested, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oDropDownWindowRequested = QWeakDelegate.Remove(this.m_oDropDownWindowRequested, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the dropdown window is about to collapse")]
    public event QInputBoxCancelEventHandler Collapsing
    {
      add => this.m_oCollapsingDelegate = QWeakDelegate.Combine(this.m_oCollapsingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCollapsingDelegate = QWeakDelegate.Remove(this.m_oCollapsingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the dropdown window is collapsed")]
    public event QInputBoxEventHandler Collapsed
    {
      add => this.m_oCollapsedDelegate = QWeakDelegate.Combine(this.m_oCollapsedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCollapsedDelegate = QWeakDelegate.Remove(this.m_oCollapsedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the dropdown window is about to show")]
    public event QInputBoxCancelEventHandler Expanding
    {
      add => this.m_oExpandingDelegate = QWeakDelegate.Combine(this.m_oExpandingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oExpandingDelegate = QWeakDelegate.Remove(this.m_oExpandingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the dropdown window is showed")]
    public event QInputBoxEventHandler Expanded
    {
      add => this.m_oExpandedDelegate = QWeakDelegate.Combine(this.m_oExpandedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oExpandedDelegate = QWeakDelegate.Remove(this.m_oExpandedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the selected value has changed")]
    public event EventHandler SelectedValueChanged
    {
      add => this.m_oSelectedValueChangedDelegate = QWeakDelegate.Combine(this.m_oSelectedValueChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oSelectedValueChangedDelegate = QWeakDelegate.Remove(this.m_oSelectedValueChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the selected index has changed")]
    public event EventHandler SelectedIndexChanged
    {
      add => this.m_oSelectedIndexChangedDelegate = QWeakDelegate.Combine(this.m_oSelectedIndexChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oSelectedIndexChangedDelegate = QWeakDelegate.Remove(this.m_oSelectedIndexChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the selected item has changed")]
    public event EventHandler SelectedItemChanged
    {
      add => this.m_oSelectedItemChangedDelegate = QWeakDelegate.Combine(this.m_oSelectedItemChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oSelectedItemChangedDelegate = QWeakDelegate.Remove(this.m_oSelectedItemChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when a mouse button is released over a button of the QInputBox.")]
    public event QInputBoxButtonEventHandler ButtonMouseUp
    {
      add => this.m_oButtonMouseUpDelegate = QWeakDelegate.Combine(this.m_oButtonMouseUpDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oButtonMouseUpDelegate = QWeakDelegate.Remove(this.m_oButtonMouseUpDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when a mouse button is pressed over a button of the QInputBox.")]
    public event QInputBoxButtonEventHandler ButtonMouseDown
    {
      add => this.m_oButtonMouseDownDelegate = QWeakDelegate.Combine(this.m_oButtonMouseDownDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oButtonMouseDownDelegate = QWeakDelegate.Remove(this.m_oButtonMouseDownDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when a button of the QInputBox is clicked.")]
    public event QInputBoxButtonEventHandler ButtonClick
    {
      add => this.m_oButtonClickDelegate = QWeakDelegate.Combine(this.m_oButtonClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oButtonClickDelegate = QWeakDelegate.Remove(this.m_oButtonClickDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the Windows XP theme is changed")]
    public event EventHandler WindowsXPThemeChanged
    {
      add => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Combine(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Remove(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the colors or the QColorScheme changes")]
    public event EventHandler ColorsChanged
    {
      add => this.m_oColorsChangedDelegate = QWeakDelegate.Combine(this.m_oColorsChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oColorsChangedDelegate = QWeakDelegate.Remove(this.m_oColorsChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("This event gets raised when the NonClientArea should be drawn")]
    public event PaintEventHandler PaintNonClientArea
    {
      add => this.m_oPaintNonClientAreaDelegate = QWeakDelegate.Combine(this.m_oPaintNonClientAreaDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPaintNonClientAreaDelegate = QWeakDelegate.Remove(this.m_oPaintNonClientAreaDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("This event gets raised when the user clicks on the NonClientArea")]
    public event QNonClientAreaMouseEventHandler NonClientAreaMouseDown
    {
      add => this.m_oNonClientAreaMouseDownDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaMouseDownDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaMouseDownDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaMouseDownDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("This event gets raised when the user double clicks on the NonClientArea")]
    public event QNonClientAreaMouseEventHandler NonClientAreaDoubleClick
    {
      add => this.m_oNonClientAreaDoubleClickDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaDoubleClickDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaDoubleClickDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaDoubleClickDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the user releases the button on the NonClientArea.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QNonClientAreaMouseEventHandler NonClientAreaMouseUp
    {
      add => this.m_oNonClientAreaMouseUpDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaMouseUpDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaMouseUpDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaMouseUpDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("This event gets raised when the user moves over the NonClientArea.")]
    public event QNonClientAreaMouseEventHandler NonClientAreaMouseMove
    {
      add => this.m_oNonClientAreaMouseMoveDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaMouseMoveDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaMouseMoveDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaMouseMoveDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the user leaves the NonClientArea.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event EventHandler NonClientAreaMouseLeave
    {
      add => this.m_oNonClientAreaMouseLeaveDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaMouseLeaveDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaMouseLeaveDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaMouseLeaveDelegate, (Delegate) value);
    }

    [Browsable(false)]
    public virtual QInputBoxAccelerationCollection Accelerations
    {
      get
      {
        if (this.m_oAccelerations == null)
          this.m_oAccelerations = new QInputBoxAccelerationCollection();
        return this.m_oAccelerations;
      }
    }

    [Bindable(true)]
    [Description("Gets or sets the current numeric value")]
    [Category("QNumericUpDown")]
    [DefaultValue(typeof (Decimal), "0")]
    public virtual Decimal NumericValue
    {
      get
      {
        this.UpdateNumericTextUserUpdate();
        return this.m_iNumericValue;
      }
      set
      {
        this.m_iNumericValue = value;
        this.UpdateNumericValue();
      }
    }

    [Category("QNumericUpDown")]
    [Description("Gets or sets the numeric value to increase or decrease the value with when the user presses one of the two buttons")]
    [DefaultValue(typeof (Decimal), "1")]
    public virtual Decimal Increment
    {
      get => this.m_iIncrement;
      set => this.m_iIncrement = value;
    }

    [Description("Gets or sets the maximum numeric value")]
    [Category("QNumericUpDown")]
    [DefaultValue(typeof (Decimal), "100")]
    public virtual Decimal MaximumValue
    {
      get => this.m_iMaximumValue;
      set
      {
        this.m_iMaximumValue = value;
        this.UpdateNumericBounds();
      }
    }

    [DefaultValue(typeof (Decimal), "0")]
    [Description("Gets or sets the minimum numeric value")]
    [Category("QNumericUpDown")]
    public virtual Decimal MinimumValue
    {
      get => this.m_iMinimumValue;
      set
      {
        this.m_iMinimumValue = value;
        this.UpdateNumericBounds();
      }
    }

    [Description("Gets or sets the format string to use for formatting the numeric value")]
    [Category("QNumericUpDown")]
    [DefaultValue("#,##0.00")]
    public virtual string FormatString
    {
      get => this.m_sFormatString;
      set
      {
        this.m_sFormatString = value;
        this.FormatStringType = QInputBoxFormatStringType.Decimal;
        if (value != null)
        {
          int num = -1;
          char ch = ' ';
          StringBuilder stringBuilder = new StringBuilder(value.Length);
          for (int index = 0; index < value.Length; ++index)
          {
            if (value[index] == '\'' || value[index] == '"')
            {
              if (num < 0)
              {
                ch = value[index];
                num = index;
              }
              else if ((int) ch == (int) value[index])
              {
                num = -1;
                continue;
              }
            }
            if (num < 0 && value[index] == 'X' || value[index] == 'x')
            {
              this.FormatStringType = QInputBoxFormatStringType.Hexadecimal;
              break;
            }
          }
        }
        this.UpdateNumericValue();
      }
    }

    [Category("QNumericUpDown")]
    [Description("Gets or sets the type that the format string represents")]
    [DefaultValue(QInputBoxFormatStringType.Decimal)]
    internal virtual QInputBoxFormatStringType FormatStringType
    {
      get => this.m_eFormatStringType;
      set => this.m_eFormatStringType = value;
    }

    [Browsable(false)]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual QInputBoxCompositeWindow CustomDropDownWindow
    {
      get => this.m_oCustomDropDownWindow;
      set => this.m_oCustomDropDownWindow = value;
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor", typeof (UITypeEditor))]
    [Description("Gets or sets the member of the datasource to display in the QInputBox")]
    [DefaultValue("")]
    [Category("QDropDown")]
    public virtual string ValueMember
    {
      get => this.m_oValueMember.BindingMember;
      set
      {
        if (value != null && value != "")
          this.CheckDataBinding();
        BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(value);
        if (bindingMemberInfo.Equals((object) this.m_oValueMember))
          return;
        if (this.DisplayMember.Length == 0)
          this.SetDataConnection(this.DataSource, bindingMemberInfo, false);
        if (this.m_oDataManager != null && !"".Equals(value) && !this.BindingMemberInfoInDataManager(bindingMemberInfo))
          throw new ArgumentException(QResources.GetException("QInputBox_WrongValueMember"));
        this.m_oValueMember = bindingMemberInfo;
        this.OnValueMemberChanged(new QInputBoxEventArgs());
        this.OnSelectedValueChanged(EventArgs.Empty);
      }
    }

    [DefaultValue("")]
    [Description("Gets or sets the member of the datasource to display in the QInputBox")]
    [Category("QDropDown")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor", typeof (UITypeEditor))]
    public virtual string DisplayMember
    {
      get => this.m_oDisplayMember.BindingMember;
      set
      {
        if (value != null && value != "")
          this.CheckDataBinding();
        BindingMemberInfo oDisplayMember = this.m_oDisplayMember;
        try
        {
          this.SetDataConnection(this.m_oDataSource, new BindingMemberInfo(value), false);
        }
        catch
        {
          this.m_oDisplayMember = oDisplayMember;
        }
      }
    }

    [DefaultValue(null)]
    [TypeConverter("System.Windows.Forms.Design.DataSourceConverter")]
    [Category("QDropDown")]
    [Description("Gets or sets the datasource")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public virtual object DataSource
    {
      get => this.m_oDataSource;
      set
      {
        if (value != null)
          this.CheckDataBinding();
        if (value != null && !(value is IList) && !(value is IListSource))
          throw new ArgumentException(QResources.GetException("QInputBox_BadDataSourceForComplexBinding"));
        if (this.m_oDataSource == value)
          return;
        try
        {
          this.SetDataConnection(value, this.m_oDisplayMember, false);
        }
        catch
        {
          this.DisplayMember = "";
        }
        if (value != null)
          return;
        this.DisplayMember = "";
        this.ValueMember = "";
        this.m_oSelectedItem = (object) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool DroppedDown
    {
      get => this.m_oDropDownWindow != null && this.m_oDropDownWindow.Visible;
      set
      {
        if (value == this.DroppedDown)
          return;
        if (value)
        {
          this.ShowDropDownWindow();
        }
        else
        {
          if (!this.DroppedDown)
            return;
          this.m_oDropDownWindow.Close(QCompositeActivationType.Keyboard);
        }
      }
    }

    [Bindable(true)]
    [DefaultValue(null)]
    [Browsable(false)]
    [Description("Gets or sets the value of the SelectedItem in the Items collection")]
    public virtual object SelectedValue
    {
      get => this.SelectedIndex != -1 && this.m_oDataManager != null ? this.FilterItemOnProperty(this.m_oDataManager.List[this.SelectedIndex], this.m_oValueMember.BindingField) : (object) null;
      set
      {
        this.CheckDataBinding();
        if (this.m_oDataManager == null)
          return;
        string bindingField = this.m_oValueMember.BindingField;
        if (bindingField.Equals(string.Empty))
          throw new InvalidOperationException(QResources.GetException("QInputBox_MissingValueMember"));
        this.SelectedIndex = this.FindInDataManager(this.m_oDataManager.GetItemProperties().Find(bindingField, true), value, true);
      }
    }

    [Description("Gets or sets the index of the SelectedItem in the Items collection")]
    [Category("QDropDown")]
    [DefaultValue(-1)]
    public virtual int SelectedIndex
    {
      get => this.SelectedItem == null ? -1 : this.Items.IndexOf(this.SelectedItem);
      set
      {
        this.CheckDataBinding();
        if (value == this.SelectedIndex && (this.m_oSelectedItem == null || value != -1))
          return;
        if (value < 0 || value >= this.Items.Count)
          this.SelectedItem = (object) null;
        else
          this.SelectedItem = this.Items[value];
      }
    }

    [Description("Gets or sets the selected item. This item must be contained in the Item collection.")]
    [DefaultValue(null)]
    [Category("QDropDown")]
    [Browsable(false)]
    [Bindable(true)]
    public virtual object SelectedItem
    {
      get => this.m_oSelectedItem == null ? (object) null : this.m_oSelectedItem;
      set
      {
        this.CheckDataBinding();
        if (value != null && this.Items.IndexOf(value) < 0)
          value = (object) null;
        if (value == this.SelectedItem)
          return;
        this.m_oSelectedItem = value;
        if (value == null)
        {
          this.SetText((string) null);
        }
        else
        {
          this.SetText(this.GetItemText(value));
          if (this.Focused)
            this.SelectAll();
        }
        if (this.DroppedDown)
          this.AdjustStateBySystemReference(this.m_oDropDownWindow.Composite.Items, this.SelectedItem, QItemStates.Hot);
        if (this.m_oDataManager != null && this.m_oDataManager.Position != this.SelectedIndex && this.IsDropDown)
          this.m_oDataManager.Position = this.SelectedIndex;
        this.OnSelectedIndexChanged(EventArgs.Empty);
        this.OnSelectedItemChanged(EventArgs.Empty);
        this.OnSelectedValueChanged(EventArgs.Empty);
      }
    }

    private void AdjustStateBySystemReference(
      QPartCollection collection,
      object systemReference,
      QItemStates adjustment)
    {
      if (collection == null)
        return;
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index] is QCompositeItemBase qcompositeItemBase && qcompositeItemBase.SystemReference != null)
        {
          qcompositeItemBase.AdjustState(adjustment, qcompositeItemBase.SystemReference == systemReference, QCompositeActivationType.None);
          if (qcompositeItemBase.SystemReference == systemReference)
            qcompositeItemBase.ScrollIntoView();
        }
        this.AdjustStateBySystemReference(collection[index].Parts, systemReference, adjustment);
        if (collection[index] is QCompositeItem)
          this.AdjustStateBySystemReference(((QCompositeItem) collection[index]).ChildItems, systemReference, adjustment);
      }
    }

    [Browsable(false)]
    public bool IsHot => this.m_bHot;

    internal void CheckDataBinding()
    {
      if (!this.CanDataBind)
        throw new InvalidOperationException(QResources.GetException("QInputBox_DataBinding"));
    }

    internal void CheckDropDown()
    {
      if (!this.IsDropDown)
        throw new InvalidOperationException(QResources.GetException("QInputBox_NotDropDown"));
    }

    internal void CheckNoDataSource()
    {
      if (this.DataSource != null)
        throw new InvalidOperationException(QResources.GetException("QInputBox_DataSourceLocksItems"));
    }

    internal void SetHot(bool value, bool refresh)
    {
      if (this.m_bHot == value)
        return;
      this.m_bHot = value;
      this.SetBackColorToState();
      if (!refresh)
        return;
      this.RefreshNonClientArea();
    }

    [Description("Gets or sets whether a transparent background must be painted. When this is false the background color of the parent is painted on this Control. If this is true the parent is painted on this control. Keeping this false increases performance. Set this to false when the Control is situated on a Parent with a solid background or when the control has a rectangular filled out shape.")]
    [DefaultValue(false)]
    [Category("QBehavior")]
    public virtual bool PaintTransparentBackground
    {
      get => this.m_bPaintTransparentBackground;
      set
      {
        this.m_bPaintTransparentBackground = value;
        this.Invalidate();
      }
    }

    [Category("QDropDown")]
    [Localizable(true)]
    [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QInputBoxObjectCollection Items
    {
      get
      {
        if (this.m_oObjectCollection == null)
          this.m_oObjectCollection = new QInputBoxObjectCollection(this);
        return this.m_oObjectCollection;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new BorderStyle BorderStyle
    {
      get => base.BorderStyle;
      set => base.BorderStyle = value;
    }

    public override bool Multiline
    {
      get => base.Multiline;
      set
      {
        base.Multiline = value;
        this.AdjustHeight();
      }
    }

    public bool ShouldSerializeOuterBorderColor() => false;

    public void ResetOuterBorderColor() => this.ColorScheme[this.OuterBorderColorPropertyName].Reset();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Gets or sets the OuterBorderColor of this control.")]
    public virtual Color OuterBorderColor
    {
      get => this.ColorScheme[this.OuterBorderColorPropertyName].Current;
      set => this.ColorScheme[this.OuterBorderColorPropertyName].Current = value;
    }

    public new bool ShouldSerializeBackColor() => false;

    public override void ResetBackColor() => this.ColorScheme[this.BackColorPropertyName].Reset();

    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets the BackColor of the input area of this QInputBox.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public override Color BackColor
    {
      get => base.BackColor;
      set => this.ColorScheme[this.BackColorPropertyName].Current = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal Color BackColorBase
    {
      get => base.BackColor;
      set
      {
        if (base.BackColor == value)
          return;
        base.BackColor = value;
      }
    }

    internal bool BackColorContainsTransparency
    {
      get
      {
        if (this.ColorScheme == null)
          return false;
        return this.ColorScheme.InputBoxBackground.Current.A < byte.MaxValue || this.ColorScheme.InputBoxHotBackground.Current.A < byte.MaxValue || this.ColorScheme.InputBoxFocusedBackground.Current.A < byte.MaxValue || this.ColorScheme.InputBoxDisabledBackground.Current.A < byte.MaxValue;
      }
    }

    public new bool ShouldSerializeForeColor() => false;

    public override void ResetForeColor() => this.ColorScheme[this.ForeColorPropertyName].Reset();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Gets the ForeColor of the input area of this QInputBox.")]
    public override Color ForeColor
    {
      get => this.ColorScheme[this.ForeColorPropertyName].Current;
      set => this.ColorScheme[this.ForeColorPropertyName].Current = value;
    }

    public bool ShouldSerializeOuterBackColor() => false;

    public void ResetOuterBackColor() => this.ColorScheme[this.OuterBackColor1PropertyName].Reset();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the second backcolor of this Control. This Color is used when the Appearance is set to Gradient.")]
    [Category("Appearance")]
    public virtual Color OuterBackColor
    {
      get => this.ColorScheme[this.OuterBackColor1PropertyName].Current;
      set => this.ColorScheme[this.OuterBackColor1PropertyName].Current = value;
    }

    public bool ShouldSerializeOuterBackColor2() => false;

    public void ResetOuterBackColor2() => this.ColorScheme[this.OuterBackColor2PropertyName].Reset();

    [Description("Gets or sets the second backcolor of this Control. This Color is used when the Appearance is set to Gradient.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public virtual Color OuterBackColor2
    {
      get => this.ColorScheme[this.OuterBackColor2PropertyName].Current;
      set => this.ColorScheme[this.OuterBackColor2PropertyName].Current = value;
    }

    public bool ShouldSerializeColorScheme() => this.ColorScheme.ShouldSerialize();

    public void ResetColorScheme() => this.ColorScheme.Reset();

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [Category("QAppearance")]
    [Description("The ColorScheme that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.InputBox)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.InputBoxButton)]
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
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
        this.OnColorsChanged(EventArgs.Empty);
        if (this.m_oColorScheme != null && !this.IsDisposed)
        {
          this.RefreshNonClientArea();
          this.Refresh();
        }
        this.SetBalloonToConfiguration();
      }
    }

    public bool ShouldSerializeAppearance() => !this.Appearance.IsSetToDefaultValues();

    public void ResetAppearance() => this.m_oAppearance.SetToDefaultValues();

    [Description("Gets or sets the QAppearance for the QInputBox.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QInputBoxAppearance Appearance => this.m_oAppearance;

    public bool ShouldSerializeAppearanceHot() => !this.AppearanceHot.IsSetToDefaultValues();

    public void ResetAppearanceHot() => this.m_oAppearanceHot.SetToDefaultValues();

    [Description("Gets or sets the QAppearance for the QInputBox when the mouse is over the QInputBox.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QInputBoxAppearance AppearanceHot => this.m_oAppearanceHot;

    public bool ShouldSerializeAppearanceFocused() => !this.AppearanceFocused.IsSetToDefaultValues();

    public void ResetAppearanceFocused() => this.m_oAppearanceFocused.SetToDefaultValues();

    [Category("QAppearance")]
    [Description("Gets or sets the QAppearance for the QInputBox when it is focused.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QInputBoxAppearance AppearanceFocused => this.m_oAppearanceFocused;

    public bool ShouldSerializeItemConfiguration() => !this.ItemConfiguration.IsSetToDefaultValues();

    public void ResetItemConfiguration() => this.ItemConfiguration.SetToDefaultValues();

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets configuration for the items of this QInputBox.")]
    public virtual QCompositeItemConfiguration ItemConfiguration => (QCompositeItemConfiguration) this.m_oItemConfiguration;

    public bool ShouldSerializeChildWindowConfiguration() => !this.ChildWindowConfiguration.IsSetToDefaultValues();

    public void ResetChildWindowConfiguration() => this.ChildWindowConfiguration.SetToDefaultValues();

    [Description("Gets or sets configuration for the dropdown window of this QInputBox.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QFloatingInputBoxWindowConfiguration ChildWindowConfiguration => this.m_oChildWindowConfiguration;

    public bool ShouldSerializeChildCompositeConfiguration() => !this.ChildCompositeConfiguration.IsSetToDefaultValues();

    public void ResetChildCompositeConfiguration() => this.ChildCompositeConfiguration.SetToDefaultValues();

    [Category("QAppearance")]
    [Description("Gets the configuration for the composite of the child window.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QFloatingInputBoxWindowCompositeConfiguration ChildCompositeConfiguration => this.m_oChildCompositeConfiguration;

    public bool ShouldSerializeChildCompositeColorScheme() => this.ChildCompositeColorScheme.ShouldSerialize();

    public void ResetChildCompositeColorScheme() => this.ChildCompositeColorScheme.Reset();

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeItem)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used for child composites")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.Composite)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeSeparator)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeButton)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeGroup)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    public virtual QColorScheme ChildCompositeColorScheme => this.m_oChildCompositeColorScheme;

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    [Description("Gets or sets the QInputBoxConfiguration for this QInputBox.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QInputBoxConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set
      {
        if (this.m_oConfiguration == value)
          return;
        if (this.m_oConfiguration != null)
        {
          this.m_oConfiguration.ConfigurationChanged -= this.m_oConfigurationChangedEventHandler;
          this.m_oConfiguration.InputBoxTextPaddingChanged -= this.m_oInputBoxTextPaddingChangedEventHandler;
        }
        this.m_oConfiguration = value;
        if (this.m_oConfiguration == null)
          return;
        this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
        this.m_oConfiguration.InputBoxTextPaddingChanged += this.m_oInputBoxTextPaddingChangedEventHandler;
        this.Configuration_ConfigurationChanged((object) this.m_oConfiguration, EventArgs.Empty);
      }
    }

    [Description("Gets or sets the ToolTipText. This must contain Xml as used with QMarkupText The ToolTip, see ToolTipConfiguration, must be enabled for this to show.")]
    [Category("QAppearance")]
    [DefaultValue(null)]
    [Localizable(true)]
    public virtual string ToolTipText
    {
      get => this.m_sToolTipText;
      set
      {
        if (this.m_sToolTipText == value)
          return;
        QXmlHelper.ValidateXmlFragment(value, true);
        this.m_sToolTipText = value;
        if (this.m_oBalloon == null)
          this.SetBalloonToConfiguration();
        else
          this.m_oBalloon.SetMarkupText((Control) this, this.m_sToolTipText);
      }
    }

    public bool ShouldSerializeToolTipConfiguration() => !this.m_oToolTipConfiguration.IsSetToDefaultValues();

    public void ResetToolTipConfiguration() => this.m_oToolTipConfiguration.SetToDefaultValues();

    [Description("Gets or sets the QToolTipConfiguration for the QContainerControl.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QToolTipConfiguration ToolTipConfiguration
    {
      get => this.m_oToolTipConfiguration;
      set
      {
        if (this.m_oToolTipConfiguration == value)
          return;
        if (this.m_oToolTipConfiguration != null)
          this.m_oToolTipConfiguration.ConfigurationChanged -= this.m_oToolTipConfigurationChangedEventHandler;
        this.m_oToolTipConfiguration = value;
        if (this.m_oToolTipConfiguration != null)
          this.m_oToolTipConfiguration.ConfigurationChanged += this.m_oToolTipConfigurationChangedEventHandler;
        this.SetBalloonToConfiguration();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public new bool AutoSize
    {
      get => this.m_bAutoSize;
      set
      {
        if (this.m_bAutoSize == value)
          return;
        this.m_bAutoSize = value;
        this.AdjustHeight();
      }
    }

    public new int PreferredHeight
    {
      get
      {
        QMargin clientAreaMargin = this.GetClientAreaMargin();
        int num = 0;
        using (Graphics graphics = this.CreateGraphics())
          num = NativeHelper.CalculateFontHeight(graphics, this.Font);
        return this.Multiline ? num + clientAreaMargin.Vertical + this.Configuration.InputBoxTextPadding.Vertical : num + clientAreaMargin.Vertical;
      }
    }

    public override string Text
    {
      get => base.Text;
      set
      {
        if (this.IsNumeric)
        {
          switch (value)
          {
            case "":
              break;
            case null:
              break;
            default:
              this.NumericValue = this.ParseDecimalValue(value);
              break;
          }
        }
        else
          base.Text = value;
      }
    }

    [DefaultValue(null)]
    [Localizable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the text that is displayed as the textual cue.")]
    public string CueText
    {
      get => this.m_sCueText;
      set
      {
        this.m_sCueText = value;
        this.Invalidate();
      }
    }

    protected virtual QInputBoxPainter Painter
    {
      get => this.m_oPainter;
      set
      {
        this.m_oPainter = value;
        if (this.m_oPainter != null)
          this.m_oPainter.Win32Window = (IWin32Window) this;
        this.ConfigureNonClientArea();
      }
    }

    protected internal virtual string ForeColorPropertyName => "TextColor";

    protected internal virtual string TextCueColorPropertyName => "TextCueColor";

    protected internal virtual string BackColorPropertyName => "InputBoxBackground";

    protected internal virtual string DisabledBackColorPropertyName => "InputBoxDisabledBackground";

    protected internal virtual string HotBackColorPropertyName => "InputBoxHotBackground";

    protected internal virtual string FocusedBackColorPropertyName => "InputBoxFocusedBackground";

    protected internal virtual string OuterBackColor1PropertyName => "InputBoxOuterBackground1";

    protected internal virtual string OuterBackColor2PropertyName => "InputBoxOuterBackground2";

    protected internal virtual string OuterBorderColorPropertyName => "InputBoxOuterBorder";

    protected internal virtual string DisabledOuterBackColor1PropertyName => "InputBoxDisabledOuterBackground1";

    protected internal virtual string DisabledOuterBackColor2PropertyName => "InputBoxDisabledOuterBackground2";

    protected internal virtual string DisabledOuterBorderColorPropertyName => "InputBoxDisabledOuterBorder";

    protected internal virtual string HotOuterBackColor1PropertyName => "InputBoxHotOuterBackground1";

    protected internal virtual string HotOuterBackColor2PropertyName => "InputBoxHotOuterBackground2";

    protected internal virtual string HotOuterBorderColorPropertyName => "InputBoxHotOuterBorder";

    protected internal virtual string FocusedOuterBackColor1PropertyName => "InputBoxFocusedOuterBackground1";

    protected internal virtual string FocusedOuterBackColor2PropertyName => "InputBoxFocusedOuterBackground2";

    protected internal virtual string FocusedOuterBorderColorPropertyName => "InputBoxFocusedOuterBorder";

    protected internal virtual string ButtonBackColor1PropertyName => "InputBoxButtonBackground1";

    protected internal virtual string ButtonBackColor2PropertyName => "InputBoxButtonBackground2";

    protected internal virtual string ButtonBorderColorPropertyName => "InputBoxButtonBorder";

    protected internal virtual string DisabledButtonBackColor1PropertyName => "InputBoxDisabledButtonBackground1";

    protected internal virtual string DisabledButtonBackColor2PropertyName => "InputBoxDisabledButtonBackground2";

    protected internal virtual string DisabledButtonBorderColorPropertyName => "InputBoxDisabledButtonBorder";

    protected internal virtual string HotButtonBackColor1PropertyName => "InputBoxHotButtonBackground1";

    protected internal virtual string HotButtonBackColor2PropertyName => "InputBoxHotButtonBackground2";

    protected internal virtual string HotButtonBorderColorPropertyName => "InputBoxHotButtonBorder";

    protected internal virtual string PressedButtonBackColor1PropertyName => "InputBoxPressedButtonBackground1";

    protected internal virtual string PressedButtonBackColor2PropertyName => "InputBoxPressedButtonBackground2";

    protected internal virtual string PressedButtonBorderColorPropertyName => "InputBoxPressedButtonBorder";

    private bool IsNumeric => this.Configuration.InputStyle == QInputBoxStyle.UpDown;

    private bool CanDataBind => this.Configuration.InputStyle == QInputBoxStyle.DropDownList || this.Configuration.InputStyle == QInputBoxStyle.DropDown || this.Configuration.InputStyle == QInputBoxStyle.TextBox;

    private bool CanAutoComplete => this.Configuration.InputStyle == QInputBoxStyle.DropDownList || this.Configuration.InputStyle == QInputBoxStyle.DropDown || this.Configuration.InputStyle == QInputBoxStyle.TextBox;

    private bool IsDropDown => this.Configuration.InputStyle == QInputBoxStyle.DropDownList || this.Configuration.InputStyle == QInputBoxStyle.DropDown;

    [DefaultValue(false)]
    [Category("QDropDown")]
    [Description("Gets or sets whether the items of the QInputBox are sorted")]
    public virtual bool Sorted
    {
      get => this.m_bSorted;
      set
      {
        if (value == this.m_bSorted)
          return;
        this.m_bSorted = !value || this.DataSource == null ? value : throw new ArgumentException(QResources.GetException("QInputBox_SortWithDataSource"));
        this.Items.Sort();
        this.SelectedIndex = -1;
      }
    }

    private bool ShowCueText => (!this.Focused || this.Configuration.InputStyle == QInputBoxStyle.DropDownList) && (this.Text == null || this.Text == "") && this.CueText != null && this.CueText != "";

    private bool VScroll => (NativeMethods.GetWindowLong(this.Handle, -16) & 2097152) == 2097152;

    private bool HScroll => (NativeMethods.GetWindowLong(this.Handle, -16) & 1048576) == 1048576;

    internal QButtonState DropDownButtonState => !this.Enabled ? QButtonState.Inactive : this.m_oDropDownButton.State;

    internal QButtonState DownButtonState => !this.Enabled ? QButtonState.Inactive : this.m_oDownButton.State;

    internal QButtonState UpButtonState => !this.Enabled ? QButtonState.Inactive : this.m_oUpButton.State;

    internal QInputBoxButtonDrawType DropDownButtonDrawType => this.GetButtonDrawType(this.DropDownButtonState, this.InputBoxState);

    internal QInputBoxButtonDrawType DownButtonDrawType => this.GetButtonDrawType(this.DownButtonState, this.InputBoxState);

    internal QInputBoxButtonDrawType UpButtonDrawType => this.GetButtonDrawType(this.UpButtonState, this.InputBoxState);

    internal QInputBoxPaintParams PaintParams => this.m_oPaintParams;

    internal QInputBoxButtonDrawType GetButtonDrawType(
      QButtonState buttonState,
      QInputBoxStates inputBoxState)
    {
      if ((inputBoxState & QInputBoxStates.Inactive) == QInputBoxStates.Inactive)
        return QInputBoxButtonDrawType.DrawButtonDisabled;
      return buttonState == QButtonState.Normal ? this.GetButtonDrawType(inputBoxState) : this.GetButtonDrawType(buttonState);
    }

    internal QInputBoxButtonDrawType GetButtonDrawType(
      QButtonState buttonState)
    {
      switch (buttonState)
      {
        case QButtonState.Inactive:
          return QInputBoxButtonDrawType.DrawButtonDisabled;
        case QButtonState.Normal:
          return QInputBoxButtonDrawType.DrawButtonNormal;
        case QButtonState.Hot:
          return QInputBoxButtonDrawType.DrawButtonHot;
        case QButtonState.Pressed:
          return QInputBoxButtonDrawType.DrawButtonPressed;
        default:
          return QInputBoxButtonDrawType.DrawButtonNormal;
      }
    }

    internal QInputBoxButtonDrawType GetButtonDrawType(
      QInputBoxStates boxState)
    {
      if ((boxState & QInputBoxStates.Inactive) == QInputBoxStates.Inactive)
        return this.Configuration.InputBoxButtonDrawNormal;
      if ((boxState & QInputBoxStates.Focused) == QInputBoxStates.Focused)
        return this.Configuration.InputBoxButtonDrawFocused;
      return (boxState & QInputBoxStates.Hot) == QInputBoxStates.Hot ? this.Configuration.InputBoxButtonDrawHot : this.Configuration.InputBoxButtonDrawNormal;
    }

    internal QInputBoxStates InputBoxState
    {
      get
      {
        QInputBoxStates inputBoxState = QInputBoxStates.Normal;
        if (!this.Enabled)
          inputBoxState |= QInputBoxStates.Inactive;
        if (this.IsHot)
          inputBoxState |= QInputBoxStates.Hot;
        if (this.Focused)
          inputBoxState |= QInputBoxStates.Focused;
        return inputBoxState;
      }
    }

    public int FindString(string value) => this.FindString(value, -1);

    public int FindString(string value, int startIndex)
    {
      if (value == null || this.Items.Count == 0)
        return -1;
      if (startIndex < -1 || startIndex >= this.Items.Count - 1)
        throw new ArgumentOutOfRangeException(nameof (startIndex));
      return this.FindString(value, (IList) this.Items, startIndex, false);
    }

    public int FindStringExact(string value) => this.FindStringExact(value, -1);

    public int FindStringExact(string value, int startIndex)
    {
      if (value == null || this.Items.Count == 0)
        return -1;
      if (startIndex < -1 || startIndex >= this.Items.Count - 1)
        throw new ArgumentOutOfRangeException(nameof (startIndex));
      return this.FindString(value, (IList) this.Items, startIndex, true);
    }

    public string GetItemText(object item)
    {
      item = this.FilterItemOnProperty(item, this.m_oDisplayMember.BindingField);
      return item == null ? "" : Convert.ToString(item, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    public Point PointToControl(Point clientPoint)
    {
      QMargin clientAreaMargin = this.GetClientAreaMargin();
      clientPoint.X += clientAreaMargin.Left;
      clientPoint.Y += clientAreaMargin.Top;
      return clientPoint;
    }

    public virtual void RefreshNonClientArea() => NativeMethods.RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 1377);

    public virtual void PerformNonClientAreaLayout() => NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, 39U);

    protected object FilterItemOnProperty(object item, string field)
    {
      if (item != null && field != null)
      {
        if (field.Length > 0)
        {
          try
          {
            PropertyDescriptor propertyDescriptor = this.m_oDataManager == null ? TypeDescriptor.GetProperties(item).Find(field, true) : this.m_oDataManager.GetItemProperties().Find(field, true);
            if (propertyDescriptor != null)
              item = propertyDescriptor.GetValue(item);
          }
          catch
          {
          }
        }
      }
      return item;
    }

    protected virtual QInputBoxCompositeWindow CreateDropDownWindow()
    {
      QInputBoxCompositeWindow dropDownWindow;
      if (this.m_oCustomDropDownWindow != null)
      {
        dropDownWindow = this.m_oCustomDropDownWindow;
      }
      else
      {
        QInputBoxCompositeWindowEventArgs e = new QInputBoxCompositeWindowEventArgs();
        this.OnDropDownWindowRequested(e);
        if (e.Window != null)
        {
          dropDownWindow = e.Window;
        }
        else
        {
          dropDownWindow = new QInputBoxCompositeWindow();
          dropDownWindow.Configuration = (QCompositeWindowConfiguration) this.ChildWindowConfiguration;
          dropDownWindow.ColorScheme = this.ChildCompositeColorScheme;
          dropDownWindow.CompositeConfiguration = (QCompositeConfiguration) this.ChildCompositeConfiguration;
        }
      }
      return dropDownWindow;
    }

    protected virtual void ConfigureDropDownWindow()
    {
      if (this.m_oDropDownWindow == null)
        return;
      this.m_oDropDownWindow.Composite.Configuration.MinimumSize = new Size(this.Width, 10);
      this.m_oDropDownWindow.SuspendChangeNotification();
      this.m_oDropDownWindow.ClearInputBoxItems();
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items[index] != null)
        {
          QCompositeItemBase inputBoxItem = this.m_oDropDownWindow.CreateInputBoxItem(this.Items[index], this, index == this.SelectedIndex);
          if (inputBoxItem != null)
            this.m_oDropDownWindow.AddInputBoxItem(inputBoxItem);
        }
      }
      if (this.m_oChildWindowConfiguration.UseSizeAsRequestedSize)
        this.m_oDropDownWindow.Size = new Size(this.Width, 0);
      this.m_oDropDownWindow.SetOwnerWindow((IWin32Window) this, true);
      this.m_oDropDownWindow.ResumeChangeNotification(false);
    }

    protected virtual QInputBoxPainter CreatePainter() => new QInputBoxPainter();

    protected virtual QToolTipConfiguration CreateToolTipConfigurationInstance() => new QToolTipConfiguration();

    protected virtual QInputBoxAppearance CreateAppearanceInstance() => new QInputBoxAppearance();

    protected virtual QInputBoxAppearance CreateAppearanceHotInstance() => new QInputBoxAppearance();

    protected virtual QInputBoxAppearance CreateAppearanceFocusedInstance() => new QInputBoxAppearance();

    protected virtual QInputBoxConfiguration CreateConfigurationInstance() => new QInputBoxConfiguration();

    protected virtual QCompositeInputBoxItemConfiguration CreateItemConfigurationInstance() => new QCompositeInputBoxItemConfiguration();

    protected virtual QFloatingInputBoxWindowConfiguration CreateChildWindowConfigurationInstance() => new QFloatingInputBoxWindowConfiguration();

    protected virtual QFloatingInputBoxWindowCompositeConfiguration CreateChildCompositeConfiguration() => new QFloatingInputBoxWindowCompositeConfiguration();

    protected virtual QColorScheme CreateChildCompositeColorScheme() => new QColorScheme();

    private bool HandleKeyboardBehaviour(QInputBoxKeyboardBehaviour behaviour) => this.Configuration != null && !this.ReadOnly && (this.Configuration.KeyboardBehaviour & behaviour) == behaviour;

    private bool HandleKeyUp(Keys keys)
    {
      bool flag = false;
      if (this.HandleKeyboardBehaviour(QInputBoxKeyboardBehaviour.NavigationKeys) && (keys == Keys.Up || keys == Keys.Down) && this.IsNumeric)
      {
        this.StopIncrementAcceleration();
        flag = true;
      }
      return flag;
    }

    private bool HandleKeyDown(Keys keys)
    {
      bool flag = false;
      if (this.HandleKeyboardBehaviour(QInputBoxKeyboardBehaviour.NavigationKeys))
      {
        if (keys == Keys.Up && this.IsNumeric)
        {
          this.IncrementNumericValue(true, true);
          flag = true;
        }
        else if (keys == Keys.Down && this.IsNumeric)
        {
          this.IncrementNumericValue(false, true);
          flag = true;
        }
        else if (keys == Keys.Down && this.IsDropDown)
        {
          if (this.SelectedIndex < this.Items.Count - 1)
          {
            ++this.SelectedIndex;
            flag = true;
          }
        }
        else if (keys == Keys.Up && this.IsDropDown && this.SelectedIndex > 0)
        {
          --this.SelectedIndex;
          flag = true;
        }
      }
      if (this.HandleKeyboardBehaviour(QInputBoxKeyboardBehaviour.ExpandKeys) && (keys == Keys.Down || keys == Keys.Up) && Control.ModifierKeys == Keys.Alt && this.IsDropDown)
      {
        this.ShowDropDownWindow();
        flag = true;
      }
      if (this.HandleKeyboardBehaviour(QInputBoxKeyboardBehaviour.CollapseKeys) && (keys == Keys.Escape || keys == Keys.Return) && this.DroppedDown && this.IsDropDown)
      {
        this.m_oDropDownWindow.Close(QCompositeActivationType.Keyboard);
        flag = true;
      }
      if (this.HandleKeyboardBehaviour(QInputBoxKeyboardBehaviour.PagingKeys) && (this.DroppedDown && this.IsDropDown || this.Configuration.InputStyle == QInputBoxStyle.DropDownList))
      {
        switch (keys)
        {
          case Keys.Prior:
            if (this.Items.Count > 0)
            {
              this.SelectedIndex = 0;
              flag = true;
              break;
            }
            break;
          case Keys.End:
            if (this.Items.Count > 0)
            {
              this.SelectedIndex = this.Items.Count - 1;
              flag = true;
              break;
            }
            break;
          case Keys.Home:
            if (this.Items.Count > 0)
            {
              this.SelectedIndex = 0;
              flag = true;
              break;
            }
            break;
          default:
            if (keys == Keys.Next && this.Items.Count > 0)
            {
              this.SelectedIndex = this.Items.Count - 1;
              flag = true;
              break;
            }
            break;
        }
      }
      if (keys == Keys.Return && this.IsNumeric)
      {
        this.UpdateNumericTextUserUpdate();
        flag = true;
      }
      return flag;
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
      if (!this.IsNumeric)
        return;
      NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
      string decimalSeparator = numberFormat.NumberDecimalSeparator;
      string numberGroupSeparator = numberFormat.NumberGroupSeparator;
      string negativeSign = numberFormat.NegativeSign;
      string str = e.KeyChar.ToString();
      if (char.IsDigit(e.KeyChar) || str.Equals(decimalSeparator) || str.Equals(numberGroupSeparator) || str.Equals(negativeSign) || e.KeyChar == '\b' || this.FormatStringType == QInputBoxFormatStringType.Hexadecimal && (e.KeyChar >= 'a' && e.KeyChar <= 'f' || e.KeyChar >= 'A' && e.KeyChar <= 'F') || (Control.ModifierKeys & (Keys.Control | Keys.Alt)) != Keys.None)
        return;
      e.Handled = true;
      NativeMethods.MessageBeep(0);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.Configuration == null || this.Configuration.InputStyle != QInputBoxStyle.DropDownList || this.DroppedDown || this.ReadOnly)
        return;
      this.ShowDropDownWindow();
    }

    protected override void OnTextChanged(EventArgs e)
    {
      if (this.m_iNumericTextUpdating == 0 && this.IsNumeric)
        this.m_bNumericTextUpdatedUser = true;
      base.OnTextChanged(e);
      if (!this.IsDropDown && this.Configuration.InputStyle != QInputBoxStyle.TextBox || this.m_iSuspendAutoSelectItem != 0)
        return;
      int stringExact = this.FindStringExact(this.Text);
      if (stringExact >= 0)
        this.m_oSelectedItem = this.Items[stringExact];
      else
        this.m_oSelectedItem = (object) null;
    }

    protected override void OnMultilineChanged(EventArgs e)
    {
      base.OnMultilineChanged(e);
      this.ConfigureNonClientArea();
      if (!this.IsHandleCreated)
        return;
      this.UpdateFormattingRectangle();
    }

    protected virtual void OnValueMemberChanged(QInputBoxEventArgs e) => this.m_oValueMemberChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oValueMemberChangedDelegate, (object) this, (object) e);

    protected virtual void OnDisplayMemberChanged(QInputBoxEventArgs e) => this.m_oDisplayMemberChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oDisplayMemberChangedDelegate, (object) this, (object) e);

    protected virtual void OnDataSourceChanged(QInputBoxEventArgs e) => this.m_oDataSourceChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oDataSourceChangedDelegate, (object) this, (object) e);

    protected virtual void OnDropDownWindowRequested(QInputBoxCompositeWindowEventArgs e) => this.m_oDropDownWindowRequested = QWeakDelegate.InvokeDelegate(this.m_oDropDownWindowRequested, (object) this, (object) e);

    protected virtual void OnCollapsing(QInputBoxCancelEventArgs e) => this.m_oCollapsingDelegate = QWeakDelegate.InvokeDelegate(this.m_oCollapsingDelegate, (object) this, (object) e);

    protected virtual void OnCollapsed(QInputBoxEventArgs e) => this.m_oCollapsedDelegate = QWeakDelegate.InvokeDelegate(this.m_oCollapsedDelegate, (object) this, (object) e);

    protected virtual void OnExpanding(QInputBoxCancelEventArgs e) => this.m_oExpandingDelegate = QWeakDelegate.InvokeDelegate(this.m_oExpandingDelegate, (object) this, (object) e);

    protected virtual void OnExpanded(QInputBoxEventArgs e) => this.m_oExpandedDelegate = QWeakDelegate.InvokeDelegate(this.m_oExpandedDelegate, (object) this, (object) e);

    protected virtual void OnSelectedValueChanged(EventArgs e) => this.m_oSelectedValueChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oSelectedValueChangedDelegate, (object) this, (object) e);

    protected virtual void OnSelectedIndexChanged(EventArgs e) => this.m_oSelectedIndexChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oSelectedIndexChangedDelegate, (object) this, (object) e);

    protected virtual void OnSelectedItemChanged(EventArgs e) => this.m_oSelectedItemChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oSelectedItemChangedDelegate, (object) this, (object) e);

    protected virtual void OnButtonMouseUp(QInputBoxButtonEventArgs e) => this.m_oButtonMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oButtonMouseUpDelegate, (object) this, (object) e);

    protected virtual void OnButtonMouseDown(QInputBoxButtonEventArgs e)
    {
      if (e.ButtonType == QInputBoxButtonType.DropDownButton && !this.ReadOnly)
        this.ShowDropDownWindow();
      else if (e.ButtonType == QInputBoxButtonType.UpButton && !this.ReadOnly)
      {
        this.IncrementNumericValue(true, true);
        this.StartTimer(500U);
      }
      else if (e.ButtonType == QInputBoxButtonType.DownButton && !this.ReadOnly)
      {
        this.IncrementNumericValue(false, true);
        this.StartTimer(500U);
      }
      this.m_oButtonMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oButtonMouseDownDelegate, (object) this, (object) e);
    }

    protected virtual void OnButtonClick(QInputBoxButtonEventArgs e) => this.m_oButtonClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oButtonClickDelegate, (object) this, (object) e);

    protected virtual void OnWindowsXPThemeChanged(EventArgs e)
    {
      this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oWindowsXPThemeChangedDelegate, (object) this, (object) e);
      if (this.Painter == null)
        return;
      this.Painter.CloseWindowsXpTheme();
    }

    protected virtual void OnPaintNonClientArea(PaintEventArgs e)
    {
      if (this.m_oPainter != null && this.m_oPaintParams != null)
      {
        this.m_oPaintParams.PaintTransparentBackground = this.PaintTransparentBackground;
        this.m_oPainter.DrawInputBox(this, this.m_oPaintParams, e);
      }
      this.m_oPaintNonClientAreaDelegate = QWeakDelegate.InvokeDelegate(this.m_oPaintNonClientAreaDelegate, (object) this, (object) e);
    }

    protected virtual void OnColorsChanged(EventArgs e)
    {
      this.SetBackColorToState();
      this.m_oColorsChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oColorsChangedDelegate, (object) this, (object) e);
    }

    protected virtual void OnNonClientAreaMouseDown(QNonClientAreaMouseEventArgs e)
    {
      this.Focus();
      this.m_oDropDownButton.HandleMouseDown((MouseEventArgs) e);
      this.m_oDownButton.HandleMouseDown((MouseEventArgs) e);
      this.m_oUpButton.HandleMouseDown((MouseEventArgs) e);
      if (this.m_oDropDownButton.Visible && this.m_oDropDownButton.Bounds.Contains(e.X, e.Y))
        this.OnButtonMouseDown(new QInputBoxButtonEventArgs(QInputBoxButtonType.DropDownButton, e.Button, e.Clicks, e.X, e.Y));
      if (this.m_oDownButton.Visible && this.m_oDownButton.Bounds.Contains(e.X, e.Y))
        this.OnButtonMouseDown(new QInputBoxButtonEventArgs(QInputBoxButtonType.DownButton, e.Button, e.Clicks, e.X, e.Y));
      if (this.m_oUpButton.Visible && this.m_oUpButton.Bounds.Contains(e.X, e.Y))
        this.OnButtonMouseDown(new QInputBoxButtonEventArgs(QInputBoxButtonType.UpButton, e.Button, e.Clicks, e.X, e.Y));
      if (!this.m_oHScrollRect.Contains(e.X, e.Y) && !this.m_oVScrollRect.Contains(e.X, e.Y))
        e.RedirectToNowhere = true;
      this.m_oNonClientAreaMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaMouseDownDelegate, (object) this, (object) e);
    }

    protected virtual void OnNonClientAreaDoubleClick(QNonClientAreaMouseEventArgs e) => this.m_oNonClientAreaDoubleClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaDoubleClickDelegate, (object) this, (object) e);

    protected virtual void OnNonClientAreaMouseUp(QNonClientAreaMouseEventArgs e)
    {
      this.m_oDropDownButton.HandleMouseUp((MouseEventArgs) e);
      this.m_oDownButton.HandleMouseUp((MouseEventArgs) e);
      this.m_oUpButton.HandleMouseUp((MouseEventArgs) e);
      if (this.m_oDropDownButton.Visible && this.m_oDropDownButton.Bounds.Contains(e.X, e.Y))
        this.OnButtonMouseUp(new QInputBoxButtonEventArgs(QInputBoxButtonType.DropDownButton, e.Button, e.Clicks, e.X, e.Y));
      if (this.m_oDownButton.Visible && this.m_oDownButton.Bounds.Contains(e.X, e.Y))
        this.OnButtonMouseUp(new QInputBoxButtonEventArgs(QInputBoxButtonType.DownButton, e.Button, e.Clicks, e.X, e.Y));
      if (this.m_oUpButton.Visible && this.m_oUpButton.Bounds.Contains(e.X, e.Y))
        this.OnButtonMouseUp(new QInputBoxButtonEventArgs(QInputBoxButtonType.UpButton, e.Button, e.Clicks, e.X, e.Y));
      if (!this.m_oHScrollRect.Contains(e.X, e.Y) && !this.m_oVScrollRect.Contains(e.X, e.Y))
        e.RedirectToNowhere = true;
      this.m_oNonClientAreaMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaMouseUpDelegate, (object) this, (object) e);
    }

    protected virtual void OnNonClientAreaMouseLeave(QNonClientAreaMouseEventArgs e)
    {
      this.m_oDropDownButton.HandleMouseLeave((MouseEventArgs) e);
      this.m_oDownButton.HandleMouseLeave((MouseEventArgs) e);
      this.m_oUpButton.HandleMouseLeave((MouseEventArgs) e);
      if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
        this.SetHot(false, true);
      this.m_oNonClientAreaMouseLeaveDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaMouseLeaveDelegate, (object) this, (object) e);
    }

    protected virtual void OnNonClientAreaMouseMove(QNonClientAreaMouseEventArgs e)
    {
      this.m_oDropDownButton.HandleMouseMove((MouseEventArgs) e);
      this.m_oDownButton.HandleMouseMove((MouseEventArgs) e);
      this.m_oUpButton.HandleMouseMove((MouseEventArgs) e);
      if (!this.m_oHScrollRect.Contains(e.X, e.Y) && !this.m_oVScrollRect.Contains(e.X, e.Y))
        e.RedirectToNowhere = true;
      this.SetHot(true, true);
      this.m_oNonClientAreaMouseMoveDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaMouseMoveDelegate, (object) this, (object) e);
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      if (this.IsDropDown)
        this.SetDataConnection(this.m_oDataSource, this.m_oDisplayMember, true);
      base.OnBindingContextChanged(e);
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      this.SetBackColorToState();
      this.RefreshNonClientArea();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      this.SetHot(true, true);
      base.OnMouseMove(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      Point control = this.PointToControl(this.PointToClient(Control.MousePosition));
      if (!new Rectangle(Point.Empty, this.Size).Contains(control))
        this.SetHot(false, true);
      base.OnMouseLeave(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.SetBackColorToState();
      this.RefreshNonClientArea();
      if (this.Configuration == null || this.Configuration.InputStyle != QInputBoxStyle.DropDownList)
        return;
      NativeMethods.HideCaret(this.Handle);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      if (this.DroppedDown && this.IsDropDown && !QCompositeHelper.HasOrContainsFocus((IQCompositeContainer) this.m_oDropDownWindow))
        this.m_oDropDownWindow.Close(QCompositeActivationType.Keyboard);
      this.UpdateNumericTextUserUpdate();
      this.SetBackColorToState();
      this.RefreshNonClientArea();
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.ConfigureNonClientArea();
    }

    protected override void OnFontChanged(EventArgs e)
    {
      base.OnFontChanged(e);
      if (this.m_oBalloon != null)
        this.m_oBalloon.Font = this.Font;
      this.AdjustHeight();
      this.UpdateFormattingRectangle();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      this.AdjustHeight();
    }

    protected override void OnParentChanged(EventArgs e)
    {
      this.RemoveListener();
      base.OnParentChanged(e);
      if (!this.BackColorContainsTransparency)
        return;
      this.AddListener(this.Parent);
    }

    protected override void Dispose(bool disposing)
    {
      if (this.m_oBackgroundBrush != IntPtr.Zero)
      {
        NativeMethods.DeleteObject(this.m_oBackgroundBrush);
        this.m_oBackgroundBrush = IntPtr.Zero;
      }
      if (disposing)
      {
        if (this.m_oStringSource != null)
        {
          this.m_oStringSource.ReleaseAutoComplete();
          this.m_oStringSource = (QInputBox.QStringSource) null;
        }
        if (this.m_oBalloon != null)
        {
          this.m_oBalloon.Dispose();
          this.m_oBalloon = (QBalloon) null;
        }
        if (this.m_oColorScheme != null && !this.m_oColorScheme.IsDisposed)
        {
          this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
          this.m_oColorScheme.Dispose();
        }
        if (this.m_oOffscreenClientSet != null)
        {
          this.m_oOffscreenClientSet.Dispose();
          this.m_oOffscreenClientSet = (QOffscreenBitmapSet) null;
        }
      }
      base.Dispose(disposing);
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        if (base.BorderStyle == BorderStyle.FixedSingle)
          createParams.Style &= -8388609;
        if (base.BorderStyle == BorderStyle.Fixed3D)
          createParams.ExStyle &= -513;
        if (this.Configuration != null && this.Configuration.InputStyle == QInputBoxStyle.DropDownList)
          createParams.Style |= 2048;
        return createParams;
      }
    }

    bool IQControlListenerClient.HandleMessage(ref Message m)
    {
      if (m.Msg != 307 && m.Msg != 312 || !(m.LParam == this.Handle) || this.m_oOffscreenClientSet == null)
        return false;
      NativeMethods.SetBkMode(m.WParam, 1);
      if (this.Multiline)
      {
        if (!this.m_bPaintingMultilineBackground)
        {
          this.m_bPaintingMultilineBackground = true;
          IntPtr num = IntPtr.Zero;
          QOffscreenBitmapSet bitmapSet = (QOffscreenBitmapSet) null;
          try
          {
            num = NativeMethods.CreateCompatibleDC(m.WParam);
            bitmapSet = QOffscreenBitmapsManager.GetFreeBitmapSet();
            IntPtr hObject = bitmapSet.SecureOffscreenDesktopBitmap(this.ClientSize);
            NativeMethods.SelectObject(num, hObject);
            this.PaintClientBitmap(num);
            Message m1 = Message.Create(this.Handle, 792, num, IntPtr.Zero);
            this.DefWndProc(ref m1);
            NativeMethods.BitBlt(m.WParam, 0, 0, this.ClientSize.Width, this.ClientSize.Height, num, 0, 0, 13369376);
          }
          finally
          {
            if (num != IntPtr.Zero)
              NativeMethods.DeleteDC(num);
            if (bitmapSet != null)
              QOffscreenBitmapsManager.FreeBitmapSet(bitmapSet);
          }
          NativeMethods.ExcludeClipRect(m.WParam, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
          this.m_bPaintingMultilineBackground = false;
        }
      }
      else
      {
        if (this.m_oBackgroundBrush != IntPtr.Zero)
          NativeMethods.DeleteObject(this.m_oBackgroundBrush);
        this.m_oBackgroundBrush = NativeMethods.CreatePatternBrush(this.m_oOffscreenClientSet.OffscreenDesktopBitmap);
        NativeMethods.SelectObject(m.WParam, this.m_oBackgroundBrush);
        m.Result = this.m_oBackgroundBrush;
      }
      NativeMethods.SetTextColor(m.WParam, ColorTranslator.ToWin32(this.ForeColor));
      return true;
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 522)
      {
        if (this.IsNumeric)
        {
          this.m_iWheelData += (int) m.WParam >> 16;
          if (Math.Abs(this.m_iWheelData) >= 120)
          {
            this.IncrementNumericValue(this.m_iWheelData > 0, false);
            this.m_iWheelData = 0;
          }
          m.Result = new IntPtr(1);
        }
        else
          base.WndProc(ref m);
      }
      else if (m.Msg == 275)
      {
        if ((long) m.WParam.ToInt32() != this.m_iAccelerationTimerID)
          return;
        this.TimerElapsed();
      }
      else if (m.Msg == 794)
      {
        this.OnWindowsXPThemeChanged(EventArgs.Empty);
        base.WndProc(ref m);
      }
      else if ((m.Msg & 65520) == 160)
      {
        QNonClientAreaMouseEventArgs clientMouseEventArgs = this.GetNonClientMouseEventArgs(m);
        switch (clientMouseEventArgs.MouseAction)
        {
          case QMouseAction.Down:
            this.OnNonClientAreaMouseDown(clientMouseEventArgs);
            break;
          case QMouseAction.DoubleClick:
            this.OnNonClientAreaMouseDown(clientMouseEventArgs);
            this.OnNonClientAreaDoubleClick(clientMouseEventArgs);
            break;
          case QMouseAction.Up:
            QButtonState state1 = this.m_oDropDownButton.State;
            QButtonState state2 = this.m_oUpButton.State;
            QButtonState state3 = this.m_oDownButton.State;
            this.OnNonClientAreaMouseUp(clientMouseEventArgs);
            if (this.DroppedDown)
            {
              if (this.m_oDropDownButton.Visible && this.m_oDropDownButton.Bounds.Contains(clientMouseEventArgs.X, clientMouseEventArgs.Y) && state1 == QButtonState.Pressed)
                this.OnButtonClick(new QInputBoxButtonEventArgs(QInputBoxButtonType.DropDownButton, clientMouseEventArgs.Button, clientMouseEventArgs.Clicks, clientMouseEventArgs.X, clientMouseEventArgs.Y));
              if (this.m_oDownButton.Visible && this.m_oDownButton.Bounds.Contains(clientMouseEventArgs.X, clientMouseEventArgs.Y) && state3 == QButtonState.Pressed)
                this.OnButtonClick(new QInputBoxButtonEventArgs(QInputBoxButtonType.DownButton, clientMouseEventArgs.Button, clientMouseEventArgs.Clicks, clientMouseEventArgs.X, clientMouseEventArgs.Y));
              if (this.m_oUpButton.Visible && this.m_oUpButton.Bounds.Contains(clientMouseEventArgs.X, clientMouseEventArgs.Y) && state2 == QButtonState.Pressed)
              {
                this.OnButtonClick(new QInputBoxButtonEventArgs(QInputBoxButtonType.UpButton, clientMouseEventArgs.Button, clientMouseEventArgs.Clicks, clientMouseEventArgs.X, clientMouseEventArgs.Y));
                break;
              }
              break;
            }
            break;
          case QMouseAction.Move:
            this.TrackNonClientAreaMouse();
            this.OnNonClientAreaMouseMove(clientMouseEventArgs);
            break;
        }
        if (clientMouseEventArgs.RedirectToNowhere && !clientMouseEventArgs.CancelDefaultAction)
        {
          m.WParam = new IntPtr(0);
          base.WndProc(ref m);
        }
        else
        {
          if (clientMouseEventArgs.CancelDefaultAction)
            return;
          base.WndProc(ref m);
        }
      }
      else if (m.Msg == 132)
      {
        base.WndProc(ref m);
        if (m.Result != IntPtr.Zero)
          return;
        Point forMouseMessages = this.GetControlPointForMouseMessages(m.LParam);
        QMargin clientAreaMargin = this.GetClientAreaMargin();
        if (new Rectangle(clientAreaMargin.Left, clientAreaMargin.Top, this.ClientSize.Width, this.ClientSize.Height).Contains(forMouseMessages))
          return;
        m.Result = new IntPtr(18);
      }
      else if (m.Msg == 20)
      {
        if (this.Multiline && !this.BackColorContainsTransparency)
          base.WndProc(ref m);
        else
          m.Result = new IntPtr(1);
      }
      else if (m.Msg == 133)
      {
        base.WndProc(ref m);
        QMargin qmargin = new QMargin();
        QMargin clientAreaMargin = this.GetClientAreaMargin();
        Rectangle rectangle = new Rectangle(clientAreaMargin.Left + qmargin.Left, clientAreaMargin.Top + qmargin.Top, this.Width - (clientAreaMargin.Horizontal + qmargin.Horizontal), this.Height - (clientAreaMargin.Vertical + qmargin.Vertical));
        Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
        Rectangle clipRect = new Rectangle(qmargin.Left, qmargin.Top, this.Width - qmargin.Horizontal, this.Height - qmargin.Vertical);
        Region region = new Region(rect);
        if (this.HScroll && this.VScroll)
        {
          int x = rect.Right - (clientAreaMargin.Right + qmargin.Right + SystemInformation.VerticalScrollBarWidth);
          int y = rect.Bottom - (clientAreaMargin.Bottom + qmargin.Bottom + SystemInformation.HorizontalScrollBarHeight);
          region.Union(new Rectangle(x, y, SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight));
        }
        IntPtr num1 = IntPtr.Zero;
        IntPtr num2 = IntPtr.Zero;
        QOffscreenBitmapSet bitmapSet = (QOffscreenBitmapSet) null;
        try
        {
          num1 = NativeMethods.GetWindowDC(m.HWnd);
          num2 = NativeMethods.CreateCompatibleDC(num1);
          bitmapSet = QOffscreenBitmapsManager.GetFreeBitmapSet();
          IntPtr hObject = bitmapSet.SecureOffscreenDesktopBitmap(rect.Size);
          NativeMethods.SelectObject(num2, hObject);
          using (Graphics graphics = Graphics.FromHdc(num2))
          {
            graphics.Clip = region;
            this.OnPaintNonClientArea(new PaintEventArgs(graphics, clipRect));
          }
          int left1 = qmargin.Left;
          int top1 = qmargin.Top;
          NativeMethods.BitBlt(num1, left1, top1, rect.Width - qmargin.Horizontal, clientAreaMargin.Top, num2, left1, top1, 13369376);
          int left2 = qmargin.Left;
          int top2 = qmargin.Top;
          NativeMethods.BitBlt(num1, left2, top2, clientAreaMargin.Left, rect.Height - qmargin.Vertical, num2, left2, top2, 13369376);
          int left3 = qmargin.Left;
          int num3 = rect.Bottom - (clientAreaMargin.Bottom + qmargin.Bottom);
          NativeMethods.BitBlt(num1, left3, num3, rect.Width - qmargin.Horizontal, clientAreaMargin.Bottom, num2, left3, num3, 13369376);
          int num4 = rect.Right - (clientAreaMargin.Right + qmargin.Right);
          int top3 = qmargin.Top;
          NativeMethods.BitBlt(num1, num4, top3, clientAreaMargin.Right, rect.Height - qmargin.Vertical, num2, num4, top3, 13369376);
          if (this.HScroll && this.VScroll)
          {
            int num5 = rect.Right - (clientAreaMargin.Right + qmargin.Right + SystemInformation.VerticalScrollBarWidth);
            int num6 = rect.Bottom - (clientAreaMargin.Bottom + qmargin.Bottom + SystemInformation.HorizontalScrollBarHeight);
            NativeMethods.BitBlt(num1, num5, num6, SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight, num2, num5, num6, 13369376);
          }
          this.UpdateClientBitmap(num2);
        }
        finally
        {
          if (num2 != IntPtr.Zero)
            NativeMethods.DeleteDC(num2);
          if (num1 != IntPtr.Zero)
            NativeMethods.ReleaseDC(m.HWnd, num1);
          if (bitmapSet != null)
            QOffscreenBitmapsManager.FreeBitmapSet(bitmapSet);
        }
      }
      else if (m.Msg == 791)
      {
        if (((int) m.LParam & 2) == 2 && (((int) m.LParam & 1) != 1 || this.Visible))
        {
          Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
          Graphics graphics = Graphics.FromHdc(m.WParam);
          Region savedRegion = QControlPaint.AdjustClip(graphics, new Region(rectangle), CombineMode.Replace);
          this.OnPaintNonClientArea(new PaintEventArgs(graphics, rectangle));
          QControlPaint.RestoreClip(graphics, savedRegion);
          graphics.Dispose();
        }
        base.WndProc(ref m);
        if (!this.ShowCueText)
          return;
        QMargin clientAreaMargin = this.GetClientAreaMargin();
        this.PaintCueText(m.WParam, new Point(clientAreaMargin.Left, clientAreaMargin.Top), false);
      }
      else if (m.Msg == 131)
      {
        QMargin clientAreaMargin = this.GetClientAreaMargin();
        if (m.WParam != IntPtr.Zero)
        {
          NativeMethods.NCCALCSIZE_PARAMS valueType = (NativeMethods.NCCALCSIZE_PARAMS) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.NCCALCSIZE_PARAMS));
          valueType.rgrc0.left += clientAreaMargin.Left;
          valueType.rgrc0.top += clientAreaMargin.Top;
          valueType.rgrc0.bottom -= clientAreaMargin.Bottom;
          valueType.rgrc0.right -= clientAreaMargin.Right;
          valueType.rgrc1 = valueType.rgrc0;
          Marshal.StructureToPtr((object) valueType, m.LParam, false);
          base.WndProc(ref m);
          m.Result = IntPtr.Zero;
        }
        else
        {
          NativeMethods.RECT valueType = (NativeMethods.RECT) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.RECT));
          valueType.left += clientAreaMargin.Left;
          valueType.top += clientAreaMargin.Top;
          valueType.bottom -= clientAreaMargin.Bottom;
          valueType.right -= clientAreaMargin.Right;
          Marshal.StructureToPtr((object) valueType, m.LParam, false);
          base.WndProc(ref m);
          m.Result = IntPtr.Zero;
        }
      }
      else if (m.Msg == 674)
      {
        this.m_bTrackingNonClientAreaMouse = false;
        this.OnNonClientAreaMouseLeave((QNonClientAreaMouseEventArgs) null);
        base.WndProc(ref m);
      }
      else if (m.Msg == 15)
      {
        NativeMethods.PAINTSTRUCT lpPaint = new NativeMethods.PAINTSTRUCT();
        IntPtr num = NativeMethods.BeginPaint(m.HWnd, ref lpPaint);
        this.GetClientAreaMargin();
        if (!this.PaintClientBitmap(num))
        {
          Message m1 = Message.Create(this.Handle, 792, num, new IntPtr(8));
          this.DefWndProc(ref m1);
        }
        else
        {
          Message m2 = Message.Create(this.Handle, 792, num, IntPtr.Zero);
          this.DefWndProc(ref m2);
        }
        if (this.ShowCueText)
          this.PaintCueText(num, Point.Empty, true);
        NativeMethods.EndPaint(m.HWnd, ref lpPaint);
      }
      else if (m.Msg == 207)
      {
        if (m.WParam.ToInt32() == 0 && this.Configuration != null && this.Configuration.InputStyle == QInputBoxStyle.DropDownList)
          return;
        base.WndProc(ref m);
      }
      else if (m.Msg == 256 || m.Msg == 260)
      {
        if (this.HandleKeyDown((Keys) (int) m.WParam))
          m.Result = IntPtr.Zero;
        else
          base.WndProc(ref m);
      }
      else if (m.Msg == 257 || m.Msg == 261)
      {
        if (this.HandleKeyUp((Keys) (int) m.WParam))
          m.Result = IntPtr.Zero;
        else
          base.WndProc(ref m);
      }
      else if (m.Msg == 123)
      {
        if (this.Configuration != null && this.Configuration.InputStyle == QInputBoxStyle.DropDownList)
          return;
        base.WndProc(ref m);
      }
      else if (m.Msg == 513)
      {
        base.WndProc(ref m);
        if (this.Configuration == null || this.Configuration.InputStyle != QInputBoxStyle.DropDownList)
          return;
        this.SelectAll();
      }
      else if (m.Msg == 515)
      {
        if (this.Configuration != null && this.Configuration.InputStyle == QInputBoxStyle.DropDownList)
          return;
        base.WndProc(ref m);
      }
      else if (m.Msg == 32)
      {
        if (this.Configuration != null && this.Configuration.InputStyle == QInputBoxStyle.DropDownList)
          return;
        base.WndProc(ref m);
      }
      else if (m.Msg == 512)
      {
        if (this.Configuration != null && this.Configuration.InputStyle == QInputBoxStyle.DropDownList)
          this.OnMouseMove(new MouseEventArgs(Control.MouseButtons, 0, (int) (short) (int) m.LParam, (int) m.LParam >> 16, 0));
        else
          base.WndProc(ref m);
      }
      else
      {
        base.WndProc(ref m);
        if (m.Msg != 71)
          return;
        this.UpdateScrollBarInfo();
      }
    }

    private void UpdateClientBitmap(IntPtr hdc)
    {
      if (!this.BackColorContainsTransparency)
      {
        if (this.m_oOffscreenClientSet == null)
          return;
        this.m_oOffscreenClientSet.Dispose();
        this.m_oOffscreenClientSet = (QOffscreenBitmapSet) null;
      }
      else
      {
        if (this.m_oOffscreenClientSet == null)
          this.m_oOffscreenClientSet = new QOffscreenBitmapSet();
        QMargin clientAreaMargin = this.GetClientAreaMargin();
        IntPtr hObject = this.m_oOffscreenClientSet.SecureOffscreenDesktopBitmap(this.ClientSize);
        IntPtr compatibleDc = NativeMethods.CreateCompatibleDC(hdc);
        NativeMethods.SelectObject(compatibleDc, hObject);
        NativeMethods.BitBlt(compatibleDc, 0, 0, this.Width - clientAreaMargin.Horizontal, this.Height - clientAreaMargin.Vertical, hdc, clientAreaMargin.Left, clientAreaMargin.Top, 13369376);
        NativeMethods.DeleteDC(compatibleDc);
      }
    }

    private bool PaintClientBitmap(IntPtr hdc)
    {
      if (this.m_oOffscreenClientSet == null)
        return false;
      IntPtr compatibleDc = NativeMethods.CreateCompatibleDC(hdc);
      NativeMethods.SelectObject(compatibleDc, this.m_oOffscreenClientSet.OffscreenDesktopBitmap);
      NativeMethods.BitBlt(hdc, 0, 0, this.Width, this.Height, compatibleDc, 0, 0, 13369376);
      NativeMethods.DeleteDC(compatibleDc);
      return true;
    }

    protected override void CreateHandle()
    {
      base.CreateHandle();
      if (this.m_bConfigurationChangedDirty)
      {
        this.m_bConfigurationChangedDirty = false;
        this.Configuration_ConfigurationChanged((object) this.Configuration, EventArgs.Empty);
      }
      if (this.m_bConfigureNonClientAreaDirty)
      {
        this.m_bConfigureNonClientAreaDirty = false;
        this.ConfigureNonClientArea();
      }
      this.UpdateFormattingRectangle();
      this.UpdateScrollBarInfo();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      if (this.IsHandleCreated)
        this.UpdateFormattingRectangle();
      base.OnSizeChanged(e);
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height && this.m_iCalculatedSize != -1)
        height = this.m_iCalculatedSize;
      base.SetBoundsCore(x, y, width, height, specified);
    }

    private void PaintCueText(IntPtr dc, Point offset, bool setRegion)
    {
      Graphics graphics = Graphics.FromHdc(dc);
      Rectangle bounds = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
      if (this.Multiline)
      {
        bounds.Offset(this.Configuration.InputBoxTextPadding.Left, this.Configuration.InputBoxTextPadding.Top);
        bounds.Width -= this.Configuration.InputBoxTextPadding.Horizontal;
        bounds.Height -= this.Configuration.InputBoxTextPadding.Vertical;
      }
      if (!offset.IsEmpty)
        bounds.Offset(offset);
      IntPtr num = IntPtr.Zero;
      if (setRegion)
      {
        num = NativeMethods.CreateRectRgn(0, 0, this.ClientSize.Width, this.ClientSize.Height);
        NativeMethods.SelectClipRgn(dc, num);
      }
      NativeHelper.DrawText(this.m_sCueText, this.Font, bounds, (Color) this.ColorScheme[this.TextCueColorPropertyName], QDrawTextOptions.None, 0, this.TextAlign, this.Multiline, graphics);
      if (setRegion)
        NativeMethods.DeleteObject(num);
      graphics.Dispose();
    }

    private Decimal ConstrainNumericValue(Decimal value)
    {
      value = Math.Min(value, this.m_iMaximumValue);
      value = Math.Max(value, this.m_iMinimumValue);
      return value;
    }

    private void UpdateNumericValue()
    {
      if (!this.IsNumeric)
        return;
      ++this.m_iNumericTextUpdating;
      base.Text = this.FormatDecimalValue(this.m_iNumericValue);
      --this.m_iNumericTextUpdating;
    }

    private void UpdateNumericTextUserUpdate()
    {
      if (!this.IsNumeric || !this.m_bNumericTextUpdatedUser)
        return;
      this.m_bNumericTextUpdatedUser = false;
      try
      {
        this.NumericValue = this.ConstrainNumericValue(this.ParseDecimalValue(this.Text));
      }
      catch
      {
      }
    }

    private void StartTimer(uint interval)
    {
      if (!this.IsHandleCreated)
        return;
      this.m_iAccelerationTimerInterval = interval;
      NativeMethods.SetTimer(this.Handle, new IntPtr(this.m_iAccelerationTimerID), interval, (QTimerCallbackDelegate) null);
    }

    private void EndTimer()
    {
      if (!this.IsHandleCreated)
        return;
      NativeMethods.KillTimer(this.Handle, new IntPtr(this.m_iAccelerationTimerID));
      this.StopIncrementAcceleration();
    }

    private void TimerElapsed()
    {
      if ((this.m_oUpButton.State & QButtonState.Pressed) == QButtonState.Pressed)
        this.IncrementNumericValue(true, true);
      else if ((this.m_oDownButton.State & QButtonState.Pressed) == QButtonState.Pressed)
        this.IncrementNumericValue(false, true);
      this.m_iAccelerationTimerInterval = Math.Max(2U, this.m_iAccelerationTimerInterval * 7U / 10U);
      this.StartTimer(this.m_iAccelerationTimerInterval);
    }

    private void IncrementNumericValue(bool up, bool useAcceleration)
    {
      Decimal increment = this.Increment;
      if (this.m_oAccelerations != null && this.m_oAccelerations.Count > 0 && useAcceleration)
      {
        long ticks = DateTime.Now.Ticks;
        if (this.m_iAccelerationTickStart < 0L)
        {
          this.m_oAccelerations.Sort();
          this.m_iAccelerationTickStart = ticks;
          this.m_iAccelerationIndex = -1;
        }
        if (this.m_iAccelerationIndex + 1 < this.m_oAccelerations.Count && ticks - this.m_iAccelerationTickStart > this.m_oAccelerations[this.m_iAccelerationIndex + 1].Ticks)
          ++this.m_iAccelerationIndex;
        if (this.m_iAccelerationIndex >= 0)
          increment = this.m_oAccelerations[this.m_iAccelerationIndex].Increment;
      }
      this.NumericValue = this.ConstrainNumericValue(up ? this.NumericValue + increment : this.NumericValue - increment);
    }

    private void StopIncrementAcceleration()
    {
      this.m_iAccelerationTickStart = -1L;
      this.m_iAccelerationIndex = -1;
    }

    private string FormatDecimalValue(Decimal value) => this.FormatStringType == QInputBoxFormatStringType.Hexadecimal ? ((long) value).ToString(this.m_sFormatString, (IFormatProvider) CultureInfo.CurrentCulture) : value.ToString(this.m_sFormatString, (IFormatProvider) CultureInfo.CurrentCulture);

    private Decimal ParseDecimalValue(string value)
    {
      if (this.FormatStringType == QInputBoxFormatStringType.Hexadecimal)
        return Convert.ToDecimal(Convert.ToInt64(value, 16));
      return this.Text == null || this.Text == "" ? 0M : Decimal.Parse(this.RemoveLiteral(this.Text), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    private string RemoveLiteral(string value)
    {
      switch (value)
      {
        case "":
        case null:
          return value;
        default:
          NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
          string decimalSeparator = numberFormat.NumberDecimalSeparator;
          string numberGroupSeparator = numberFormat.NumberGroupSeparator;
          string negativeSign = numberFormat.NegativeSign;
          StringBuilder stringBuilder = new StringBuilder(value.Length);
          for (int index = 0; index < value.Length; ++index)
          {
            string s = value[index].ToString();
            if (char.IsDigit(s, 0) || s.Equals(decimalSeparator) || s.Equals(numberGroupSeparator) || s.Equals(negativeSign))
              stringBuilder.Append(s);
          }
          return stringBuilder.ToString();
      }
    }

    private void UpdateNumericBounds()
    {
      if (this.m_iNumericValue > this.m_iMaximumValue)
      {
        this.NumericValue = this.m_iMaximumValue;
      }
      else
      {
        if (!(this.m_iNumericValue < this.m_iMinimumValue))
          return;
        this.NumericValue = this.m_iMinimumValue;
      }
    }

    private int FindInDataManager(PropertyDescriptor property, object key, bool keepIndex)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (property != null && this.m_oDataManager.List is IBindingList && ((IBindingList) this.m_oDataManager.List).SupportsSearching)
        return ((IBindingList) this.m_oDataManager.List).Find(property, key);
      for (int index = 0; index < this.m_oDataManager.List.Count; ++index)
      {
        object obj = property.GetValue(this.m_oDataManager.List[index]);
        if (key.Equals(obj))
          return index;
      }
      return -1;
    }

    internal void UpdateText() => this.SetText(this.GetItemText(this.SelectedItem));

    internal void SetText(string text)
    {
      try
      {
        ++this.m_iSuspendAutoSelectItem;
        this.Text = text;
      }
      finally
      {
        --this.m_iSuspendAutoSelectItem;
      }
    }

    private int FindString(string value, IList items, int startIndex, bool exact)
    {
      if (value != null && items != null && startIndex >= -1 && startIndex < items.Count - 1)
      {
        int length = value.Length;
        int index = startIndex;
        do
        {
          ++index;
          if (!exact ? string.Compare(value, 0, this.GetItemText(items[index]), 0, length, true, CultureInfo.CurrentCulture) == 0 : string.Compare(value, this.GetItemText(items[index]), true, CultureInfo.CurrentCulture) == 0)
            return index;
          if (index == items.Count - 1)
            index = -1;
        }
        while (index != startIndex);
      }
      return -1;
    }

    private void SetDataConnection(
      object newDataSource,
      BindingMemberInfo newDisplayMember,
      bool force)
    {
      bool flag1 = this.DataSource != newDataSource;
      bool flag2 = !this.m_oDisplayMember.Equals((object) newDisplayMember);
      if (force || flag1 || flag2)
      {
        if (this.m_oDataSource is IComponent)
          ((IComponent) this.m_oDataSource).Disposed -= new EventHandler(this.DataSource_Disposed);
        this.m_oDataSource = newDataSource;
        this.m_oDisplayMember = newDisplayMember;
        if (this.m_oDataSource is IComponent)
          ((IComponent) this.m_oDataSource).Disposed += new EventHandler(this.DataSource_Disposed);
        CurrencyManager currencyManager = (CurrencyManager) null;
        if (newDataSource != null && this.BindingContext != null && newDataSource != Convert.DBNull)
          currencyManager = (CurrencyManager) this.BindingContext[newDataSource, newDisplayMember.BindingPath];
        if (this.m_oDataManager != currencyManager)
        {
          if (this.m_oDataManager != null)
          {
            this.m_oDataManager.ItemChanged -= new ItemChangedEventHandler(this.DataManager_ItemChanged);
            this.m_oDataManager.PositionChanged -= new EventHandler(this.DataManager_PositionChanged);
          }
          this.m_oDataManager = currencyManager;
          if (this.m_oDataManager != null)
          {
            this.m_oDataManager.ItemChanged += new ItemChangedEventHandler(this.DataManager_ItemChanged);
            this.m_oDataManager.PositionChanged += new EventHandler(this.DataManager_PositionChanged);
          }
        }
        if (this.m_oDataManager != null && (flag2 || flag1) && !"".Equals(this.m_oDisplayMember.BindingMember) && !this.BindingMemberInfoInDataManager(this.m_oDisplayMember))
          throw new ArgumentException(QResources.GetException("QInputBox_WrongDisplayMember"));
        if (this.m_oDataManager != null && (flag2 || flag1 || force))
          this.HandleDataManagerItemChanged(-1);
      }
      if (flag1)
        this.OnDataSourceChanged(new QInputBoxEventArgs());
      if (!flag2)
        return;
      this.OnDisplayMemberChanged(new QInputBoxEventArgs());
    }

    private bool BindingMemberInfoInDataManager(BindingMemberInfo info)
    {
      if (this.m_oDataManager == null)
        return false;
      PropertyDescriptorCollection itemProperties = this.m_oDataManager.GetItemProperties();
      for (int index = 0; index < itemProperties.Count; ++index)
      {
        if (!typeof (IList).IsAssignableFrom(itemProperties[index].PropertyType) && itemProperties[index].Name.Equals(info.BindingField))
          return true;
      }
      return false;
    }

    private void HandleDataManagerItemChanged(int index)
    {
      if (this.m_oDataManager == null)
        return;
      if (index == -1)
      {
        this.Items.ClearInternal(true);
        this.Items.AddRangeInternal(this.m_oDataManager.List);
        if (this.IsDropDown)
          this.SelectedIndex = this.m_oDataManager.Position;
        this.UpdateText();
      }
      else
        this.Items.SetItemInternal(index, this.m_oDataManager.List[index]);
    }

    private void DataSource_Disposed(object sender, EventArgs e) => this.SetDataConnection((object) null, new BindingMemberInfo(""), true);

    private void DataManager_ItemChanged(object sender, ItemChangedEventArgs e) => this.HandleDataManagerItemChanged(e.Index);

    private void DataManager_PositionChanged(object sender, EventArgs e)
    {
      if (this.m_oDataManager == null || !this.IsDropDown)
        return;
      this.SelectedIndex = this.m_oDataManager.Position;
    }

    private void SetBackColorToState()
    {
      if (!this.Enabled)
        this.BackColorBase = (Color) this.m_oColorScheme[this.DisabledBackColorPropertyName];
      else if (this.Focused)
        this.BackColorBase = (Color) this.m_oColorScheme[this.FocusedBackColorPropertyName];
      else if (this.IsHot)
        this.BackColorBase = (Color) this.m_oColorScheme[this.HotBackColorPropertyName];
      else
        this.BackColorBase = (Color) this.m_oColorScheme[this.BackColorPropertyName];
    }

    private void SecureDropDownWindow()
    {
      if (this.m_oDropDownWindow != null)
        return;
      this.m_oDropDownWindow = this.CreateDropDownWindow();
      this.m_oDropDownWindow.ItemActivated += new QCompositeEventHandler(this.m_oDropDownWindow_ItemActivated);
      this.m_oDropDownWindow.Closing += new CancelEventHandler(this.m_oDropDownWindow_Closing);
      this.m_oDropDownWindow.Closed += new EventHandler(this.m_oDropDownWindow_Closed);
    }

    private void ShowDropDownWindow()
    {
      this.CheckDropDown();
      QInputBoxCancelEventArgs e = new QInputBoxCancelEventArgs();
      this.OnExpanding(e);
      if (e.Cancel)
        return;
      this.SecureDropDownWindow();
      this.ConfigureDropDownWindow();
      QMargin clientAreaMargin = this.GetClientAreaMargin();
      Rectangle screen = this.RectangleToScreen(new Rectangle(-clientAreaMargin.Left, -clientAreaMargin.Top, this.Width, this.Height));
      Rectangle openingItemBounds = new Rectangle(Point.Empty, screen.Size);
      QRelativePositions openingItemRelativePosition = QRelativePositions.Above;
      QCommandDirections animateDirection = QCommandDirections.Down;
      this.m_oDropDownWindow.CalculateBounds(screen, openingItemBounds, ref openingItemRelativePosition, ref animateDirection);
      this.m_oDropDownWindow.Show(screen, openingItemBounds, openingItemRelativePosition, animateDirection);
      this.OnExpanded(new QInputBoxEventArgs());
    }

    private void SetBalloonToConfiguration()
    {
      if (this.m_oToolTipConfiguration != null && this.m_oToolTipConfiguration.Enabled)
      {
        this.SecureBalloon();
        this.m_oBalloon.Configuration = (QBalloonConfiguration) this.m_oToolTipConfiguration;
        this.m_oBalloon.ColorScheme = this.ColorScheme;
        this.m_oBalloon.SetMarkupText((Control) this, this.m_sToolTipText);
      }
      else
      {
        if (this.m_oBalloon == null)
          return;
        this.m_oBalloon.Dispose();
        this.m_oBalloon = (QBalloon) null;
      }
    }

    private void SecureBalloon()
    {
      if (this.m_oBalloon != null)
        return;
      this.m_oBalloon = new QBalloon();
      if (this.m_sToolTipText == null || this.m_sToolTipText.Length <= 0 || this.m_sToolTipText == null || !(this.m_sToolTipText != ""))
        return;
      this.m_oBalloon.AddListener((Control) this, this.m_sToolTipText);
    }

    private void UpdateScrollBarInfo()
    {
      NativeMethods.SCROLLBARINFO psbi1 = new NativeMethods.SCROLLBARINFO();
      psbi1.cbSize = Marshal.SizeOf((object) psbi1);
      NativeMethods.GetScrollBarInfo(this.Handle, 4294967291U, ref psbi1);
      this.m_oVScrollRect = (psbi1.rgstate[0] & 32768) != 32768 ? this.RectangleToClient(NativeHelper.CreateRectangle(psbi1.rcScrollBar)) : Rectangle.Empty;
      NativeMethods.SCROLLBARINFO psbi2 = new NativeMethods.SCROLLBARINFO();
      psbi2.cbSize = Marshal.SizeOf((object) psbi2);
      NativeMethods.GetScrollBarInfo(this.Handle, 4294967290U, ref psbi2);
      if ((psbi2.rgstate[0] & 32768) == 32768)
        this.m_oHScrollRect = Rectangle.Empty;
      else
        this.m_oHScrollRect = this.RectangleToClient(NativeHelper.CreateRectangle(psbi2.rcScrollBar));
    }

    private void UpdateFormattingRectangle()
    {
      if (!this.Multiline)
        return;
      NativeMethods.RECT structure = new NativeMethods.RECT();
      structure.left = this.Configuration.InputBoxTextPadding.Left;
      structure.top = this.Configuration.InputBoxTextPadding.Top;
      structure.right = this.ClientSize.Width - this.Configuration.InputBoxTextPadding.Right;
      structure.bottom = this.ClientSize.Height - this.Configuration.InputBoxTextPadding.Bottom;
      int num1 = this.FontHeight - (structure.bottom - structure.top);
      if (num1 > 0)
        structure.bottom += num1;
      int num2 = this.FontHeight - (structure.right - structure.left);
      if (num2 > 0)
        structure.right += num2;
      IntPtr num3 = Marshal.AllocHGlobal(Marshal.SizeOf((object) structure));
      Marshal.StructureToPtr((object) structure, num3, true);
      NativeMethods.SendMessage(this.Handle, 179, IntPtr.Zero, num3);
      Marshal.FreeHGlobal(num3);
    }

    private void TrackNonClientAreaMouse()
    {
      if (this.m_bTrackingNonClientAreaMouse)
        return;
      NativeMethods.TRACKMOUSEEVENT lpEventTrack = new NativeMethods.TRACKMOUSEEVENT();
      lpEventTrack.cbSize = (uint) Marshal.SizeOf((object) lpEventTrack);
      lpEventTrack.dwFlags = 18U;
      lpEventTrack.hwndTrack = this.Handle;
      this.m_bTrackingNonClientAreaMouse = NativeMethods.TrackMouseEvent(ref lpEventTrack);
    }

    private Point GetControlPointForMouseMessages(IntPtr param) => this.PointToControl(this.PointToClient(new Point(param.ToInt32())));

    private QNonClientAreaMouseEventArgs GetNonClientMouseEventArgs(
      Message m)
    {
      Point forMouseMessages = this.GetControlPointForMouseMessages(m.LParam);
      QSizingAction sizingAction = QSizingAction.None;
      QNonClientAreaLocation location = QNonClientAreaLocation.Nowhere;
      MouseButtons buttons = MouseButtons.None;
      QMouseAction mouseAction = QMouseAction.None;
      if (m.Msg == 161)
      {
        mouseAction = QMouseAction.Down;
        buttons = MouseButtons.Left;
      }
      else if (m.Msg == 164)
      {
        mouseAction = QMouseAction.Down;
        buttons = MouseButtons.Right;
      }
      else if (m.Msg == 171)
      {
        mouseAction = QMouseAction.Down;
        buttons = MouseButtons.XButton1;
      }
      else if (m.Msg == 167)
      {
        mouseAction = QMouseAction.Down;
        buttons = MouseButtons.Middle;
      }
      else if (m.Msg == 162)
      {
        mouseAction = QMouseAction.Up;
        buttons = MouseButtons.Left;
      }
      else if (m.Msg == 165)
      {
        mouseAction = QMouseAction.Up;
        buttons = MouseButtons.Right;
      }
      else if (m.Msg == 172)
      {
        mouseAction = QMouseAction.Up;
        buttons = MouseButtons.XButton1;
      }
      else if (m.Msg == 168)
      {
        mouseAction = QMouseAction.Up;
        buttons = MouseButtons.Middle;
      }
      else if (m.Msg == 163)
      {
        mouseAction = QMouseAction.DoubleClick;
        buttons = MouseButtons.Left;
      }
      else if (m.Msg == 166)
      {
        mouseAction = QMouseAction.DoubleClick;
        buttons = MouseButtons.Right;
      }
      else if (m.Msg == 173)
      {
        mouseAction = QMouseAction.DoubleClick;
        buttons = MouseButtons.XButton1;
      }
      else if (m.Msg == 169)
      {
        mouseAction = QMouseAction.DoubleClick;
        buttons = MouseButtons.Middle;
      }
      else if (m.Msg == 160)
      {
        mouseAction = QMouseAction.Move;
        buttons = Control.MouseButtons;
      }
      return new QNonClientAreaMouseEventArgs(mouseAction, buttons, 1, forMouseMessages.X, forMouseMessages.Y, location, sizingAction);
    }

    internal QMargin GetClientAreaMargin() => this.m_oPaintParams == null ? new QMargin() : this.m_oPaintParams.ClientAreaMargin;

    private void AdjustHeight()
    {
      if (this.Multiline || !this.AutoSize)
      {
        this.m_iCalculatedSize = -1;
      }
      else
      {
        this.m_iCalculatedSize = this.PreferredHeight;
        this.Height = this.m_iCalculatedSize;
      }
    }

    private void AddListener(Control control)
    {
      if (control == null)
        return;
      QControlListenerManager.Attach((IQControlListenerClient) this, control);
    }

    private void RemoveListener() => QControlListenerManager.Detach((IQControlListenerClient) this);

    private void ToolTip_ConfigurationChanged(object sender, EventArgs e) => this.SetBalloonToConfiguration();

    private void Appearance_AppearanceChanged(object sender, EventArgs e) => this.ConfigureNonClientArea();

    private void Configuration_ConfigurationChanged(object sender, EventArgs e)
    {
      if (!this.IsHandleCreated)
      {
        this.m_bConfigurationChangedDirty = true;
      }
      else
      {
        if (this.Painter != null)
          this.Painter.CloseWindowsXpTheme();
        bool flag = (NativeMethods.GetWindowLong(this.Handle, -16) & 2048) == 2048;
        if ((flag ? 1 : 0) != (this.ReadOnly ? 1 : (this.Configuration.InputStyle == QInputBoxStyle.DropDownList ? 1 : 0)))
          NativeMethods.SendMessage(this.Handle, 207, !flag ? new IntPtr(-1) : new IntPtr(0), IntPtr.Zero);
        uint num = 0;
        if (this.CanAutoComplete)
        {
          if ((this.Configuration.AutoCompleteMode & QAutoCompleteMode.Suggest) == QAutoCompleteMode.Suggest)
            num |= 268435456U;
          if ((this.Configuration.AutoCompleteMode & QAutoCompleteMode.Append) == QAutoCompleteMode.Append)
            num |= 1073741824U;
          if (num != 0U)
            num = (uint) ((QAutoCompleteSource) num | this.Configuration.AutoCompleteSource);
        }
        if ((int) this.m_iAutoCompleteFlagsSet != (int) num)
        {
          this.m_iAutoCompleteFlagsSet = num;
          this.UpdateAutoComplete();
        }
        if (!this.CanDataBind && this.DataSource != null)
          this.DataSource = (object) null;
        this.UpdateNumericValue();
        this.ConfigureNonClientArea();
      }
    }

    internal void UpdateAutoComplete()
    {
      if (this.Configuration.AutoCompleteSource == QAutoCompleteSource.ListItems)
      {
        if (this.m_oStringSource != null)
        {
          this.m_oStringSource.RefreshList(this.GetStringsForAutoComplete(), this.Configuration.AutoCompleteMode);
        }
        else
        {
          this.m_oStringSource = new QInputBox.QStringSource(this.GetStringsForAutoComplete(), this.Configuration.AutoCompleteMode);
          this.m_oStringSource.Bind(new HandleRef((object) this, this.Handle));
        }
      }
      else
      {
        if (this.m_oStringSource != null)
        {
          this.m_oStringSource.ReleaseAutoComplete();
          this.m_oStringSource = (QInputBox.QStringSource) null;
        }
        NativeMethods.SHAutoComplete(this.Handle, this.m_iAutoCompleteFlagsSet);
      }
    }

    private string[] GetStringsForAutoComplete()
    {
      string[] stringsForAutoComplete = new string[this.Items.Count];
      for (int index = 0; index < this.Items.Count; ++index)
        stringsForAutoComplete[index] = this.GetItemText(this.Items[index]);
      return stringsForAutoComplete;
    }

    private void Configuration_InputBoxTextPaddingChanged(object sender, EventArgs e) => this.UpdateFormattingRectangle();

    private void m_oDropDownWindow_ItemActivated(object sender, QCompositeEventArgs e)
    {
      if (e.Item == null || e.Item.SystemReference == null)
        return;
      this.SelectedItem = e.Item.SystemReference;
    }

    private void m_oDropDownWindow_Closing(object sender, CancelEventArgs e)
    {
      QInputBoxCancelEventArgs e1 = new QInputBoxCancelEventArgs();
      e1.Cancel = e.Cancel;
      this.OnCollapsing(e1);
      e.Cancel = e1.Cancel;
    }

    private void m_oDropDownWindow_Closed(object sender, EventArgs e)
    {
      if (!this.Bounds.Contains(this.PointToClient(Control.MousePosition)))
        this.SetHot(false, true);
      this.OnCollapsed(new QInputBoxEventArgs());
    }

    private void ColorScheme_ColorsChanged(object sender, EventArgs e)
    {
      this.OnColorsChanged(EventArgs.Empty);
      if (this.BackColorContainsTransparency)
        this.AddListener(this.Parent);
      else
        this.RemoveListener();
      this.RefreshNonClientArea();
      this.Refresh();
    }

    private void Button_ButtonStateChanged(object sender, QButtonAreaEventArgs e)
    {
      if ((sender == this.m_oUpButton || sender == this.m_oDownButton) && (e.FromState & QButtonState.Pressed) == QButtonState.Pressed && (e.ToState & QButtonState.Pressed) != QButtonState.Pressed)
        this.EndTimer();
      this.RefreshNonClientArea();
    }

    private void ConfigureNonClientArea()
    {
      if (!this.IsHandleCreated)
      {
        this.m_bConfigureNonClientAreaDirty = true;
      }
      else
      {
        if (this.Painter == null)
          return;
        if (this.m_oPaintParams == null)
          this.m_oPaintParams = new QInputBoxPaintParams();
        QMargin clientAreaMargin = this.m_oPaintParams.ClientAreaMargin;
        this.Painter.LayoutInputBox(this, this.m_oPaintParams);
        if (!clientAreaMargin.Equals((object) this.m_oPaintParams.ClientAreaMargin))
        {
          this.PerformNonClientAreaLayout();
        }
        else
        {
          this.AdjustHeight();
          this.RefreshNonClientArea();
        }
        this.m_oDropDownButton.Bounds = this.m_oPaintParams.DropDownButtonBounds;
        this.m_oDropDownButton.Visible = !this.m_oDropDownButton.Bounds.IsEmpty && (this.Configuration.InputStyle == QInputBoxStyle.DropDown || this.Configuration.InputStyle == QInputBoxStyle.DropDownList);
        this.m_oDownButton.Bounds = this.m_oPaintParams.DownButtonBounds;
        this.m_oDownButton.Visible = !this.m_oDownButton.Bounds.IsEmpty;
        this.m_oUpButton.Bounds = this.m_oPaintParams.UpButtonBounds;
        this.m_oUpButton.Visible = !this.m_oUpButton.Bounds.IsEmpty;
      }
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000101-0000-0000-C000-000000000046")]
    [ComImport]
    internal interface IQEnumString
    {
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr), Out] string[] rgelt, IntPtr pceltFetched);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int Skip(int celt);

      void Reset();

      void Clone(out QInputBox.IQEnumString ppenum);
    }

    [Guid("EAC04BC0-3791-11d2-BB95-0060977B464C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    internal interface IQAutoComplete2
    {
      int Init(
        [In] HandleRef hwndEdit,
        [In] QInputBox.IQEnumString punkACL,
        [In] string pwszRegKeyPath,
        [In] string pwszQuickComplete);

      void Enable([In] bool fEnable);

      int SetOptions([In] int dwFlag);

      void GetOptions([Out] IntPtr pdwFlag);
    }

    internal class QStringSource : QInputBox.IQEnumString
    {
      private static Guid m_oAutoCompleteClsid = new Guid("{00BB2763-6A77-11D0-A535-00C04FD7D062}");
      private QInputBox.IQAutoComplete2 m_oAutoCompleteObject2;
      private int m_iCurrent;
      private int m_iSize;
      private string[] m_aStrings;
      private QAutoCompleteMode m_eAutoCompleteMode;

      public QStringSource(string[] strings, QAutoCompleteMode mode)
      {
        Array.Clear((Array) strings, 0, this.m_iSize);
        if (strings != null)
          this.m_aStrings = strings;
        this.m_iCurrent = 0;
        this.m_iSize = strings == null ? 0 : strings.Length;
        Guid guid = typeof (QInputBox.IQAutoComplete2).GUID;
        this.m_oAutoCompleteObject2 = (QInputBox.IQAutoComplete2) NativeMethods.CoCreateInstance(ref QInputBox.QStringSource.m_oAutoCompleteClsid, (object) null, 1, ref guid);
        this.m_eAutoCompleteMode = mode;
      }

      public bool Bind(HandleRef edit)
      {
        if (this.m_oAutoCompleteObject2 == null)
          return false;
        try
        {
          this.m_oAutoCompleteObject2.SetOptions((int) this.m_eAutoCompleteMode);
          this.m_oAutoCompleteObject2.Init(edit, (QInputBox.IQEnumString) this, (string) null, (string) null);
          return true;
        }
        catch
        {
          return false;
        }
      }

      public void RefreshList(string[] newSource, QAutoCompleteMode mode)
      {
        Array.Clear((Array) this.m_aStrings, 0, this.m_iSize);
        if (newSource != null)
          this.m_aStrings = newSource;
        this.m_iCurrent = 0;
        this.m_iSize = this.m_aStrings == null ? 0 : this.m_aStrings.Length;
        if (mode.Equals((object) this.m_eAutoCompleteMode))
          return;
        this.m_eAutoCompleteMode = mode;
        this.m_oAutoCompleteObject2.SetOptions((int) this.m_eAutoCompleteMode);
      }

      public void ReleaseAutoComplete()
      {
        if (this.m_oAutoCompleteObject2 == null)
          return;
        Marshal.ReleaseComObject((object) this.m_oAutoCompleteObject2);
        this.m_oAutoCompleteObject2 = (QInputBox.IQAutoComplete2) null;
      }

      void QInputBox.IQEnumString.Clone(out QInputBox.IQEnumString ppenum) => ppenum = (QInputBox.IQEnumString) new QInputBox.QStringSource(this.m_aStrings, this.m_eAutoCompleteMode);

      int QInputBox.IQEnumString.Next(int celt, string[] rgelt, IntPtr pceltFetched)
      {
        if (celt < 0)
          return -2147024809;
        int val = 0;
        for (; this.m_iCurrent < this.m_iSize && celt > 0; --celt)
        {
          rgelt[val] = this.m_aStrings[this.m_iCurrent];
          ++this.m_iCurrent;
          ++val;
        }
        if (pceltFetched != IntPtr.Zero)
          Marshal.WriteInt32(pceltFetched, val);
        return celt != 0 ? 1 : 0;
      }

      void QInputBox.IQEnumString.Reset() => this.m_iCurrent = 0;

      int QInputBox.IQEnumString.Skip(int celt)
      {
        this.m_iCurrent += celt;
        return this.m_iCurrent >= this.m_iSize ? 1 : 0;
      }
    }
  }
}
