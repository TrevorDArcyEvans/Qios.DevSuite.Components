// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupLabel
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QMarkupLabel), "Resources.ControlImages.QMarkupLabel.bmp")]
  [ToolboxItem(true)]
  [DefaultEvent("ElementLinkClick")]
  public class QMarkupLabel : QControl
  {
    private QMarkupText m_oMarkupTextObject;
    private QWeakDelegate m_oElementMouseEnterDelegate;
    private QWeakDelegate m_oElementMouseLeaveDelegate;
    private QWeakDelegate m_oElementMouseDownDelegate;
    private QWeakDelegate m_oElementMouseUpDelegate;
    private QWeakDelegate m_oElementMouseClickDelegate;
    private QWeakDelegate m_oElementLinkClickDelegate;
    private QWeakDelegate m_oElementGotFocus;
    private QWeakDelegate m_oElementLostFocus;

    public QMarkupLabel()
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.m_oMarkupTextObject = new QMarkupText(this.ColorScheme, this.Font, (Color) this.ColorScheme.MarkupText);
      this.m_oMarkupTextObject.UpdateRequested += new QCommandUIRequestEventHandler(this.MarkupText_UpdateRequested);
      this.m_oMarkupTextObject.Root.MouseEnter += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseEnter);
      this.m_oMarkupTextObject.Root.MouseLeave += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseLeave);
      this.m_oMarkupTextObject.Root.MouseDown += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseDown);
      this.m_oMarkupTextObject.Root.MouseUp += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseUp);
      this.m_oMarkupTextObject.Root.MouseClick += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseClick);
      this.m_oMarkupTextObject.Root.LinkClick += new QMarkupTextElementEventHandler(this.MarkupTextRoot_LinkClick);
      this.m_oMarkupTextObject.Root.GotFocus += new QMarkupTextElementEventHandler(this.MarkupTextRoot_GotFocus);
      this.m_oMarkupTextObject.Root.LostFocus += new QMarkupTextElementEventHandler(this.MarkupTextRoot_LostFocus);
      this.ResumeLayout(false);
    }

    [QWeakEvent]
    [Description("Occurs when the mouse enters an element.")]
    [Category("QEvents")]
    public event QMarkupTextElementEventHandler ElementMouseEnter
    {
      add => this.m_oElementMouseEnterDelegate = QWeakDelegate.Combine(this.m_oElementMouseEnterDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseEnterDelegate = QWeakDelegate.Remove(this.m_oElementMouseEnterDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Occurs when the mouse leaves this element.")]
    public event QMarkupTextElementEventHandler ElementMouseLeave
    {
      add => this.m_oElementMouseLeaveDelegate = QWeakDelegate.Combine(this.m_oElementMouseLeaveDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseLeaveDelegate = QWeakDelegate.Remove(this.m_oElementMouseLeaveDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Occurs when the user presses the mousebutton on this element.")]
    [QWeakEvent]
    public event QMarkupTextElementEventHandler ElementMouseDown
    {
      add => this.m_oElementMouseDownDelegate = QWeakDelegate.Combine(this.m_oElementMouseDownDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseDownDelegate = QWeakDelegate.Remove(this.m_oElementMouseDownDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user releases the mousebutton on this element.")]
    public event QMarkupTextElementEventHandler ElementMouseUp
    {
      add => this.m_oElementMouseUpDelegate = QWeakDelegate.Combine(this.m_oElementMouseUpDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseUpDelegate = QWeakDelegate.Remove(this.m_oElementMouseUpDelegate, (Delegate) value);
    }

    [Description("Occurs when the user clicks on this element.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QMarkupTextElementEventHandler ElementMouseClick
    {
      add => this.m_oElementMouseClickDelegate = QWeakDelegate.Combine(this.m_oElementMouseClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseClickDelegate = QWeakDelegate.Remove(this.m_oElementMouseClickDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Occurs when the user clicks on a link.")]
    [Category("QEvents")]
    public event QMarkupTextElementEventHandler ElementLinkClick
    {
      add => this.m_oElementLinkClickDelegate = QWeakDelegate.Combine(this.m_oElementLinkClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementLinkClickDelegate = QWeakDelegate.Remove(this.m_oElementLinkClickDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when an element gets the focus")]
    public event QMarkupTextElementEventHandler ElementGotFocus
    {
      add => this.m_oElementGotFocus = QWeakDelegate.Combine(this.m_oElementGotFocus, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementGotFocus = QWeakDelegate.Remove(this.m_oElementGotFocus, (Delegate) value);
    }

    [Description("Occurs when an element lost the focus")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QMarkupTextElementEventHandler ElementLostFocus
    {
      add => this.m_oElementLostFocus = QWeakDelegate.Combine(this.m_oElementLostFocus, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementLostFocus = QWeakDelegate.Remove(this.m_oElementLostFocus, (Delegate) value);
    }

    [Description("Gets the QAppearance.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QMarkupLabelAppearance Appearance => (QMarkupLabelAppearance) base.Appearance;

    public bool ShouldSerializeConfiguration() => this.m_oMarkupTextObject.ShouldSerializeConfiguration();

    public void ResetConfiguration() => this.m_oMarkupTextObject.ResetConfiguration();

    [Category("QAppearance")]
    [Description("Gets the configuration for the QMarkupLabel.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QMarkupTextConfiguration Configuration => this.m_oMarkupTextObject.Configuration;

    [Description("Gets or sets the text for this QMarkupLabel. This property does not apply any formatting. To format the text, use MarkupText")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public override string Text
    {
      get => base.Text;
      set
      {
        base.Text = value;
        this.m_oMarkupTextObject.MarkupText = QMarkupText.ReplaceEnterWithBR(HttpUtility.HtmlEncode(QMarkupText.RemoveTags(value)));
      }
    }

    [Localizable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue("")]
    [Description("Gets or sets the Markup text for this QMarkupLabel. The formatting happens in XML.")]
    [Category("QAppearance")]
    public string MarkupText
    {
      get => this.m_oMarkupTextObject.MarkupText;
      set
      {
        this.m_oMarkupTextObject.MarkupText = value != null ? value : string.Empty;
        base.Text = QMarkupText.RemoveTags(HttpUtility.HtmlDecode(QMarkupText.ReplaceBRWithEnter(this.m_oMarkupTextObject.Root.MarkupNode.InnerXml)));
      }
    }

    public override Font Font
    {
      get => base.Font;
      set
      {
        base.Font = value;
        this.PerformLayout();
        this.Refresh();
      }
    }

    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the collection with custom styles. This can be used to handle custom tags or handle default tags different than the regular behavior.")]
    public QMarkupTextStyleCollection CustomStyles => this.m_oMarkupTextObject.CustomStyles;

    [Browsable(false)]
    public QMarkupText MarkupTextObject => this.m_oMarkupTextObject;

    [Browsable(false)]
    public QMarkupTextElement Root => (QMarkupTextElement) this.m_oMarkupTextObject.Root;

    protected override string BackColorPropertyName => "MarkupLabelBackground1";

    protected override string BackColor2PropertyName => "MarkupLabelBackground2";

    protected override string BorderColorPropertyName => "MarkupLabelBorder";

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QMarkupLabelAppearance();

    protected override bool ProcessDialogKey(Keys keyData) => this.HandleKeyDown(keyData) || base.ProcessDialogKey(keyData);

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.Enabled)
        return;
      this.m_oMarkupTextObject.HandleMouseMove(e);
      if (this.m_oMarkupTextObject.HotElement != null && this.m_oMarkupTextObject.HotElement.IsOrHasParentOfType(typeof (QMarkupTextElementAnchor)))
        this.Cursor = Cursors.Hand;
      else
        this.Cursor = Cursors.Default;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.Enabled)
        return;
      this.m_oMarkupTextObject.HandleMouseDown(e);
      if (!this.CanFocus || !this.CanSelect || this.Focused || this.m_oMarkupTextObject.FocusedElement != null)
        return;
      this.Focus();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.Enabled)
        return;
      this.m_oMarkupTextObject.HandleMouseUp(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.m_oMarkupTextObject.HandleMouseLeave();
      this.Cursor = Cursors.Default;
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.m_oMarkupTextObject.HandleGotFocus();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.m_oMarkupTextObject.HandleLostFocus();
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      this.m_oMarkupTextObject.Enabled = this.Enabled;
    }

    private void RenderMarkupLabel()
    {
      QMarkupTextParams markupParams = new QMarkupTextParams(this.CreateGraphics());
      this.m_oMarkupTextObject.RenderMarkupText(markupParams);
      markupParams.Graphics.Dispose();
      this.SetStyle(ControlStyles.Selectable, this.m_oMarkupTextObject.ShouldReceiveFocus());
    }

    private bool HandleKeyDown(Keys keys) => this.Focused && !this.DesignMode && (this.HandleFocusKeys(keys) || this.HandleActivationKeys(keys));

    private bool HandleFocusKeys(Keys keys)
    {
      if (this.m_oMarkupTextObject.FocusedElement == null)
        return false;
      bool forward;
      bool loop;
      switch (keys)
      {
        case Keys.Tab:
          forward = true;
          loop = false;
          break;
        case Keys.Left:
        case Keys.Up:
          forward = false;
          loop = true;
          break;
        case Keys.Right:
        case Keys.Down:
          forward = true;
          loop = true;
          break;
        case Keys.Tab | Keys.Shift:
          forward = false;
          loop = false;
          break;
        default:
          return false;
      }
      this.m_oMarkupTextObject.HandleFocus(forward, loop);
      return this.m_oMarkupTextObject.FocusedElement != null;
    }

    private bool HandleActivationKeys(Keys keys)
    {
      if (keys != Keys.Return || this.m_oMarkupTextObject.FocusedElement == null)
        return false;
      this.m_oMarkupTextObject.FocusedElement.HandleMouseClick((QMarkupTextElement) null);
      return true;
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.m_oMarkupTextObject.PutBounds(this.ClientRectangle);
      this.RenderMarkupLabel();
      if (this.m_oMarkupTextObject.Lines.Count <= 0)
        return;
      BoundsSpecified specified = BoundsSpecified.None;
      if (this.Configuration.AdjustWidthToTextSize)
        specified |= BoundsSpecified.Width;
      if (this.Configuration.AdjustHeightToTextSize)
        specified |= BoundsSpecified.Height;
      this.SetBounds(0, 0, this.m_oMarkupTextObject.Width, this.m_oMarkupTextObject.Height, specified);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.m_oMarkupTextObject.Draw(e.Graphics);
    }

    protected override void OnFontChanged(EventArgs e)
    {
      base.OnFontChanged(e);
      if (this.m_oMarkupTextObject == null)
        return;
      this.m_oMarkupTextObject.SetProperties(this.Font, this.ColorScheme, (Color) this.ColorScheme.TextColor);
    }

    protected override void OnColorsChanged(EventArgs e)
    {
      base.OnColorsChanged(e);
      if (this.m_oMarkupTextObject == null)
        return;
      this.m_oMarkupTextObject.SetProperties(this.Font, this.ColorScheme, (Color) this.ColorScheme.MarkupText);
    }

    protected virtual void OnElementMouseEnter(QMarkupTextElementEventArgs e) => this.m_oElementMouseEnterDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseEnterDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseLeave(QMarkupTextElementEventArgs e) => this.m_oElementMouseLeaveDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseLeaveDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseDown(QMarkupTextElementEventArgs e) => this.m_oElementMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseDownDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseUp(QMarkupTextElementEventArgs e) => this.m_oElementMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseUpDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseClick(QMarkupTextElementEventArgs e) => this.m_oElementMouseClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseClickDelegate, (object) this, (object) e);

    protected virtual void OnElementLinkClick(QMarkupTextElementEventArgs e) => this.m_oElementLinkClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementLinkClickDelegate, (object) this, (object) e);

    protected virtual void OnElementGotFocus(QMarkupTextElementEventArgs e) => this.m_oElementGotFocus = QWeakDelegate.InvokeDelegate(this.m_oElementGotFocus, (object) this, (object) e);

    protected virtual void OnElementLostFocus(QMarkupTextElementEventArgs e) => this.m_oElementLostFocus = QWeakDelegate.InvokeDelegate(this.m_oElementLostFocus, (object) this, (object) e);

    private void Configuration_ConfigurationChanged(object sender, EventArgs e) => this.PerformLayout();

    private void MarkupText_UpdateRequested(object sender, QCommandUIRequestEventArgs e)
    {
      if (e.Request == QCommandUIRequest.Redraw)
      {
        if (!this.Visible)
          return;
        this.Invalidate();
      }
      else
      {
        if (e.Request != QCommandUIRequest.PerformLayout)
          return;
        this.PerformLayout();
        if (!this.Visible)
          return;
        this.Invalidate();
      }
    }

    private void MarkupTextRoot_MouseEnter(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseEnter(e);

    private void MarkupTextRoot_MouseLeave(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseLeave(e);

    private void MarkupTextRoot_MouseDown(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseDown(e);

    private void MarkupTextRoot_MouseUp(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseUp(e);

    private void MarkupTextRoot_MouseClick(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseClick(e);

    private void MarkupTextRoot_LinkClick(object sender, QMarkupTextElementEventArgs e) => this.OnElementLinkClick(e);

    private void MarkupTextRoot_GotFocus(object sender, QMarkupTextElementEventArgs e) => this.OnElementGotFocus(e);

    private void MarkupTextRoot_LostFocus(object sender, QMarkupTextElementEventArgs e) => this.OnElementLostFocus(e);
  }
}
