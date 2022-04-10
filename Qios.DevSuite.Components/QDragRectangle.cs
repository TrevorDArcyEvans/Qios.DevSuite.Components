// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDragRectangle
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QDragRectangle
  {
    private static int m_iReversibleRectangleIndent = 5;
    private static QDragRectangle.QDragForm m_oDragForm;
    private static QDragRectangleType m_eLastDrawnRectangleType = QDragRectangleType.TransparentForm;
    private static Rectangle m_oLastDrawnReversibleRectangle;

    public static void Draw(
      Rectangle rectangle,
      string text,
      QDragRectangleType rectangleType,
      Form owner,
      QDockControl dockControl)
    {
      switch (rectangleType)
      {
        case QDragRectangleType.TransparentForm:
          if (QDragRectangle.m_eLastDrawnRectangleType == QDragRectangleType.ReversibleRectangle)
            QDragRectangle.ClearRectangle();
          QDragRectangle.m_eLastDrawnRectangleType = QDragRectangleType.TransparentForm;
          if (QDragRectangle.m_oDragForm == null || QDragRectangle.m_oDragForm.IsDisposed)
            QDragRectangle.m_oDragForm = new QDragRectangle.QDragForm(owner);
          QDragRectangle.QDragForm oDragForm = QDragRectangle.m_oDragForm;
          string str;
          switch (text)
          {
            case "":
            case null:
              str = " ";
              break;
            default:
              str = text;
              break;
          }
          oDragForm.Text = str;
          QDockingWindow currentWindow = dockControl.CurrentWindow;
          if (currentWindow != null)
          {
            if (QDragRectangle.m_oDragForm.FormBorderStyle != currentWindow.FormBorderStyleUndocked)
              QDragRectangle.m_oDragForm.FormBorderStyle = currentWindow.FormBorderStyleUndocked;
          }
          else if (QDragRectangle.m_oDragForm.FormBorderStyle != FormBorderStyle.SizableToolWindow)
            QDragRectangle.m_oDragForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
          NativeMethods.SetWindowPos(QDragRectangle.m_oDragForm.Handle, IntPtr.Zero, rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, 4U);
          QDragRectangle.m_oDragForm.Visible = true;
          break;
        case QDragRectangleType.ReversibleRectangle:
          QDragRectangle.ClearRectangle();
          QDragRectangle.m_eLastDrawnRectangleType = QDragRectangleType.ReversibleRectangle;
          QDragRectangle.m_oLastDrawnReversibleRectangle = rectangle;
          NativeHelper.DrawDragRectangle(rectangle, QDragRectangle.m_iReversibleRectangleIndent);
          break;
      }
    }

    public static void ClearRectangle()
    {
      if (QDragRectangle.m_eLastDrawnRectangleType == QDragRectangleType.TransparentForm && QDragRectangle.m_oDragForm != null)
      {
        if (!QDragRectangle.m_oDragForm.Visible)
          return;
        QDragRectangle.m_oDragForm.Hide();
      }
      else
      {
        if (QDragRectangle.m_eLastDrawnRectangleType != QDragRectangleType.ReversibleRectangle || !(QDragRectangle.m_oLastDrawnReversibleRectangle != Rectangle.Empty))
          return;
        NativeHelper.DrawDragRectangle(QDragRectangle.m_oLastDrawnReversibleRectangle, QDragRectangle.m_iReversibleRectangleIndent);
        QDragRectangle.m_oLastDrawnReversibleRectangle = Rectangle.Empty;
      }
    }

    internal class QDragForm : Form
    {
      public QDragForm(Form owner)
      {
        this.Owner = owner;
        this.ShowInTaskbar = false;
        this.ControlBox = false;
        this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        this.Opacity = 0.5;
      }

      protected override CreateParams CreateParams
      {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
        {
          CreateParams createParams = base.CreateParams;
          if (this.Owner != null)
            createParams.Parent = QControlHelper.GetUndisposedHandle((IWin32Window) this.Owner);
          return createParams;
        }
      }
    }
  }
}
