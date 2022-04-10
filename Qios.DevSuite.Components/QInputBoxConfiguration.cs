// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  public class QInputBoxConfiguration : QFastPropertyBagHost, IDisposable
  {
    protected const int PropButtonAppearance = 0;
    protected const int PropButtonAppearanceHot = 1;
    protected const int PropButtonAppearancePressed = 2;
    protected const int PropInputBoxTextPadding = 3;
    protected const int PropInputBoxPadding = 4;
    protected const int PropInputBoxStyle = 5;
    protected const int PropInputStyle = 6;
    protected const int PropButtonMask = 7;
    protected const int PropButtonPadding = 8;
    protected const int PropButtonMargin = 9;
    protected const int PropInputBoxButtonDrawNormal = 10;
    protected const int PropInputBoxButtonDrawHot = 11;
    protected const int PropInputBoxButtonDrawFocused = 12;
    protected const int PropButtonMaskAlign = 13;
    protected const int PropButtonAlign = 14;
    protected const int PropAutoCompleteMode = 15;
    protected const int PropAutoCompleteSource = 16;
    protected const int PropKeyBoardBehaviour = 17;
    protected const int CurrentPropertyCount = 18;
    protected const int TotalPropertyCount = 18;
    private EventHandler m_oButtonAppearanceChangedEventHandler;
    private static Image m_oDefaultButtonMask;
    private Image m_oButtonMaskReverse;
    private QWeakDelegate m_oConfigurationChangedDelegate;
    private QWeakDelegate m_oInputBoxTextPaddingChanged;

    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    internal event EventHandler InputBoxTextPaddingChanged
    {
      add => this.m_oInputBoxTextPaddingChanged = QWeakDelegate.Combine(this.m_oInputBoxTextPaddingChanged, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oInputBoxTextPaddingChanged = QWeakDelegate.Remove(this.m_oInputBoxTextPaddingChanged, (Delegate) value);
    }

    public QInputBoxConfiguration()
    {
      this.m_oButtonAppearanceChangedEventHandler = new EventHandler(this.ButtonAppearance_AppearanceChanged);
      if (QInputBoxConfiguration.m_oDefaultButtonMask == null)
        QInputBoxConfiguration.m_oDefaultButtonMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.ComboBoxMask.png"));
      this.Properties.DefineResettableProperty(0, (IQResettableValue) this.CreateButtonAppearanceInstance());
      this.Properties.DefineResettableProperty(1, (IQResettableValue) this.CreateButtonAppearanceHotInstance());
      this.Properties.DefineResettableProperty(2, (IQResettableValue) this.CreateButtonAppearancePressedInstance());
      this.ButtonAppearance.AppearanceChanged += this.m_oButtonAppearanceChangedEventHandler;
      this.ButtonAppearanceHot.AppearanceChanged += this.m_oButtonAppearanceChangedEventHandler;
      this.ButtonAppearancePressed.AppearanceChanged += this.m_oButtonAppearanceChangedEventHandler;
      this.ButtonAppearanceHot.Properties.BaseProperties = this.ButtonAppearance.Properties;
      this.ButtonAppearancePressed.Properties.BaseProperties = this.ButtonAppearance.Properties;
      this.Properties.DefineProperty(3, (object) new QPadding(2, 2, 2, 2));
      this.Properties.DefineProperty(4, (object) new QPadding(0, 0, 0, 0));
      this.Properties.DefineProperty(5, (object) QButtonStyle.Custom);
      this.Properties.DefineProperty(6, (object) QInputBoxStyle.TextBox);
      this.Properties.DefineProperty(7, (object) null);
      this.Properties.DefineProperty(8, (object) new QPadding(4, 3, 3, 4));
      this.Properties.DefineProperty(9, (object) new QMargin(0, -1, -1, -2));
      this.Properties.DefineProperty(10, (object) QInputBoxButtonDrawType.DontDrawButtonBackground);
      this.Properties.DefineProperty(11, (object) QInputBoxButtonDrawType.DrawButtonNormal);
      this.Properties.DefineProperty(12, (object) QInputBoxButtonDrawType.DrawButtonHot);
      this.Properties.DefineProperty(13, (object) QImageAlign.Centered);
      this.Properties.DefineProperty(14, (object) QInputBoxButtonAlign.Right);
      this.Properties.DefineProperty(15, (object) QAutoCompleteMode.None);
      this.Properties.DefineProperty(16, (object) QAutoCompleteSource.None);
      this.Properties.DefineProperty(17, (object) (QInputBoxKeyboardBehaviour.ExpandKeys | QInputBoxKeyboardBehaviour.CollapseKeys | QInputBoxKeyboardBehaviour.NavigationKeys | QInputBoxKeyboardBehaviour.PagingKeys));
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
    }

    protected override int GetRequestedCount() => 18;

    ~QInputBoxConfiguration() => this.Dispose(false);

    [Description("Gets or sets the keyboard behaviour")]
    [Category("QAppearance")]
    [QPropertyIndex(17)]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    public QInputBoxKeyboardBehaviour KeyboardBehaviour
    {
      get => (QInputBoxKeyboardBehaviour) this.Properties.GetPropertyAsValueType(17);
      set => this.Properties.SetProperty(17, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets the auto complete source")]
    [QPropertyIndex(16)]
    public virtual QAutoCompleteSource AutoCompleteSource
    {
      get => (QAutoCompleteSource) this.Properties.GetPropertyAsValueType(16);
      set => this.Properties.SetProperty(16, (object) value);
    }

    [QPropertyIndex(15)]
    [Description("Gets or sets the auto complete mode")]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    [Category("QAppearance")]
    public virtual QAutoCompleteMode AutoCompleteMode
    {
      get => (QAutoCompleteMode) this.Properties.GetPropertyAsValueType(15);
      set => this.Properties.SetProperty(15, (object) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(0)]
    [Category("QAppearance")]
    [Description("Gets or sets the QAppearance for the button of the QInputBox.")]
    public virtual QInputBoxButtonShapeAppearance ButtonAppearance
    {
      get => this.Properties.GetProperty(0) as QInputBoxButtonShapeAppearance;
      set
      {
        if (this.ButtonAppearance == value)
          return;
        if (this.ButtonAppearance != null)
          this.ButtonAppearance.AppearanceChanged -= this.m_oButtonAppearanceChangedEventHandler;
        this.Properties.SetProperty(0, (object) value);
        if (this.ButtonAppearance == null)
          return;
        this.ButtonAppearance.AppearanceChanged += this.m_oButtonAppearanceChangedEventHandler;
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the QAppearance for the button of the QInputBox when the mouse is over the button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(1)]
    public virtual QInputBoxButtonShapeAppearance ButtonAppearanceHot
    {
      get => this.Properties.GetProperty(1) as QInputBoxButtonShapeAppearance;
      set
      {
        if (this.ButtonAppearanceHot == value)
          return;
        if (this.ButtonAppearanceHot != null)
          this.ButtonAppearanceHot.AppearanceChanged -= this.m_oButtonAppearanceChangedEventHandler;
        this.Properties.SetProperty(1, (object) value);
        if (this.ButtonAppearanceHot == null)
          return;
        this.ButtonAppearanceHot.AppearanceChanged += this.m_oButtonAppearanceChangedEventHandler;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(2)]
    [Description("Gets or sets the QAppearance for the button of the QInputBox when the button is pressed.")]
    [Category("QAppearance")]
    public virtual QInputBoxButtonShapeAppearance ButtonAppearancePressed
    {
      get => this.Properties.GetProperty(2) as QInputBoxButtonShapeAppearance;
      set
      {
        if (this.ButtonAppearancePressed == value)
          return;
        if (this.ButtonAppearancePressed != null)
          this.ButtonAppearancePressed.AppearanceChanged -= this.m_oButtonAppearanceChangedEventHandler;
        this.Properties.SetProperty(2, (object) value);
        if (this.ButtonAppearancePressed == null)
          return;
        this.ButtonAppearancePressed.AppearanceChanged += this.m_oButtonAppearanceChangedEventHandler;
      }
    }

    [Description("Gets or sets the alignment of the buttons of a QInputBox")]
    [QPropertyIndex(14)]
    [Category("QAppearance")]
    public virtual QInputBoxButtonAlign ButtonAlign
    {
      get => (QInputBoxButtonAlign) this.Properties.GetPropertyAsValueType(14);
      set
      {
        this.Properties.SuspendChangeNotification();
        switch (value)
        {
          case QInputBoxButtonAlign.Left:
            this.ButtonAppearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.InputBoxRoundedDropDownButtonLeft2]);
            this.Properties.DefineProperty(9, (object) new QMargin(-2, -1, -1, 0));
            break;
          case QInputBoxButtonAlign.LeftOutside:
            this.ButtonAppearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.InputBoxRoundedDropDownButtonLeft2]);
            this.Properties.DefineProperty(9, (object) new QMargin(0, 0, 0, 1));
            break;
          case QInputBoxButtonAlign.Right:
            this.ButtonAppearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.InputBoxRoundedDropDownButton2]);
            this.Properties.DefineProperty(9, (object) new QMargin(0, -1, -1, -2));
            break;
          case QInputBoxButtonAlign.RightOutside:
            this.ButtonAppearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.InputBoxRoundedDropDownButton2]);
            this.Properties.DefineProperty(9, (object) new QMargin(1, 0, 0, 0));
            break;
        }
        this.Properties.SetProperty(14, (object) value);
        this.Properties.ResumeChangeNotification(true);
      }
    }

    [Description("Gets or sets the drawing style of the input box")]
    [Category("QAppearance")]
    [QPropertyIndex(5)]
    public virtual QButtonStyle InputBoxStyle
    {
      get => (QButtonStyle) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [QPropertyIndex(10)]
    [Description("Gets or sets the draw type of the button when the InputBox is in the normal state.")]
    [Category("QAppearance")]
    public virtual QInputBoxButtonDrawType InputBoxButtonDrawNormal
    {
      get => (QInputBoxButtonDrawType) this.Properties.GetPropertyAsValueType(10);
      set => this.Properties.SetProperty(10, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets the draw type of the button when the InputBox is in the hot state.")]
    [QPropertyIndex(11)]
    public virtual QInputBoxButtonDrawType InputBoxButtonDrawHot
    {
      get => (QInputBoxButtonDrawType) this.Properties.GetPropertyAsValueType(11);
      set => this.Properties.SetProperty(11, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets the draw type of the button when the InputBox is in the focused state.")]
    [QPropertyIndex(12)]
    public virtual QInputBoxButtonDrawType InputBoxButtonDrawFocused
    {
      get => (QInputBoxButtonDrawType) this.Properties.GetPropertyAsValueType(12);
      set => this.Properties.SetProperty(12, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(3)]
    [Description("Gets or sets the padding of the text of the QInputBox")]
    public QPadding InputBoxTextPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(3);
      set
      {
        this.Properties.SetProperty(3, (object) value);
        this.OnInputBoxTextPaddingChanged(EventArgs.Empty);
      }
    }

    [QPropertyIndex(4)]
    [Category("QAppearance")]
    [Description("Gets or sets the padding of the QInputBox")]
    public QPadding InputBoxPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [Description("Gets or sets the style of the QInputBox")]
    [Category("QAppearance")]
    [QPropertyIndex(6)]
    public virtual QInputBoxStyle InputStyle
    {
      get => (QInputBoxStyle) this.Properties.GetPropertyAsValueType(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [QPropertyIndex(8)]
    [Category("QAppearance")]
    [Description("Gets or sets the button padding of the QInputBox")]
    public virtual QPadding ButtonPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(8);
      set => this.Properties.SetProperty(8, (object) value);
    }

    [Description("Gets or sets the button margin of the QInputBox")]
    [Category("QAppearance")]
    [QPropertyIndex(9)]
    public virtual QMargin ButtonMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(9);
      set => this.Properties.SetProperty(9, (object) value);
    }

    [Browsable(false)]
    public virtual Image UsedButtonMask => this.ButtonMask != null ? this.ButtonMask : QInputBoxConfiguration.m_oDefaultButtonMask;

    [Browsable(false)]
    public virtual Image UsedButtonMaskReverse
    {
      get
      {
        if (this.m_oButtonMaskReverse == null)
        {
          this.m_oButtonMaskReverse = (Image) this.UsedButtonMask.Clone();
          this.m_oButtonMaskReverse.RotateFlip(RotateFlipType.Rotate180FlipX);
        }
        return this.m_oButtonMaskReverse;
      }
    }

    [Category("QAppearance")]
    [Description("Contains the base image that is used to for the buttons. In the Mask the Color Red is replaced by the TextColor.")]
    [QPropertyIndex(7)]
    public virtual Image ButtonMask
    {
      get => this.Properties.GetProperty(7) as Image;
      set
      {
        this.Properties.SetProperty(7, (object) value);
        this.m_oButtonMaskReverse = (Image) null;
      }
    }

    [Description("Gets or sets the alignment of the button mask.")]
    [QPropertyIndex(13)]
    [Category("QAppearance")]
    public virtual QImageAlign ButtonMaskAlign
    {
      get => (QImageAlign) this.Properties.GetProperty(13);
      set => this.Properties.SetProperty(13, (object) value);
    }

    protected virtual QInputBoxButtonShapeAppearance CreateButtonAppearanceInstance() => new QInputBoxButtonShapeAppearance();

    protected virtual QInputBoxButtonShapeAppearance CreateButtonAppearanceHotInstance() => new QInputBoxButtonShapeAppearance();

    protected virtual QInputBoxButtonShapeAppearance CreateButtonAppearancePressedInstance() => new QInputBoxButtonShapeAppearance();

    internal virtual void OnInputBoxTextPaddingChanged(EventArgs e) => this.m_oInputBoxTextPaddingChanged = QWeakDelegate.InvokeDelegate(this.m_oInputBoxTextPaddingChanged, (object) this, (object) e);

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    private void ButtonAppearance_AppearanceChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      int num = disposing ? 1 : 0;
    }
  }
}
