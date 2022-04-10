// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockPointCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QDockPointCollection : CollectionBase
  {
    private int m_iMousePositionMarginForDock = 10;
    private int m_iDockOnDockContainerMargin = 10;

    public void Add(QDockPoint point) => this.List.Add((object) point);

    public QDockPoint this[int index] => (QDockPoint) this.List[index];

    private void AddDockPointsForWindow(QDockingWindow window, QDockControl control)
    {
      if (window.IsInSlideMode)
        return;
      QDockControl controlToPlaceOnContainer = (QDockControl) window;
      Control parent = window.Parent;
      Rectangle screen1 = window.RectangleToScreen(window.NonClientRectangleToClient(window.CaptionArea));
      Rectangle screen2 = window.RectangleToScreen(new Rectangle(0, 0, window.ClientRectangle.Width, this.m_iDockOnDockContainerMargin));
      Rectangle screen3 = window.RectangleToScreen(new Rectangle(0, window.ClientRectangle.Height - this.m_iDockOnDockContainerMargin, window.ClientRectangle.Width, this.m_iDockOnDockContainerMargin));
      Rectangle screen4 = window.RectangleToScreen(new Rectangle(0, 0, this.m_iDockOnDockContainerMargin, window.ClientRectangle.Height));
      Rectangle screen5 = window.RectangleToScreen(new Rectangle(window.ClientRectangle.Width - this.m_iDockOnDockContainerMargin, 0, this.m_iDockOnDockContainerMargin, window.ClientRectangle.Height));
      bool flag = parent == null || !(parent.Parent is QDockContainer);
      int controlIndex = controlToPlaceOnContainer.ControlIndex;
      if (controlToPlaceOnContainer.DockContainer != null && controlToPlaceOnContainer.DockContainer.IsTabbed)
        controlIndex = controlToPlaceOnContainer.DockContainer.ControlIndex;
      if (control.CanDockOnOtherControlTabbed)
        this.Add(new QDockPoint(screen1, window.DockPosition, window.Parent, 0, window.DockBar, QDockOrientation.Tabbed, (QDockControl) window));
      if (control.CanDockOnOtherControlTop)
        this.Add(new QDockPoint(screen2, window.DockPosition, parent, flag ? 0 : controlIndex, window.DockBar, QDockOrientation.Vertical, controlToPlaceOnContainer));
      if (control.CanDockOnOtherControlBottom)
        this.Add(new QDockPoint(screen3, window.DockPosition, parent, controlToPlaceOnContainer.ControlIndex + 1, window.DockBar, QDockOrientation.Vertical, controlToPlaceOnContainer));
      if (control.CanDockOnOtherControlLeft)
        this.Add(new QDockPoint(screen4, window.DockPosition, parent, flag ? 0 : controlIndex, window.DockBar, QDockOrientation.Horizontal, controlToPlaceOnContainer));
      if (!control.CanDockOnOtherControlRight)
        return;
      this.Add(new QDockPoint(screen5, window.DockPosition, parent, controlToPlaceOnContainer.ControlIndex + 1, window.DockBar, QDockOrientation.Horizontal, controlToPlaceOnContainer));
    }

    private void AddDockPointsForDockContainer(
      QDockContainer dockContainer,
      Form owningForm,
      QDockControl control)
    {
      if (dockContainer.IsTabbed)
      {
        QDockingWindow currentWindow = dockContainer.CurrentWindow;
        this.AddDockPointsForWindow(currentWindow, control);
        if (!control.CanDockOnOtherControlTabbed)
          return;
        this.Add(new QDockPoint(dockContainer.RectangleToScreen(new Rectangle(0, dockContainer.ClientRectangle.Height - dockContainer.TabStrip.Bounds.Height, dockContainer.ClientRectangle.Width, dockContainer.TabStrip.Bounds.Height)), dockContainer.DockPosition, (Control) dockContainer, 0, dockContainer.DockBar, QDockOrientation.Tabbed, (QDockControl) currentWindow));
      }
      else
      {
        for (int index = 0; index < dockContainer.Controls.Count; ++index)
        {
          if (dockContainer.Controls[index] != control && dockContainer.Controls[index].Visible)
          {
            if (dockContainer.Controls[index] is QDockContainer)
              this.AddDockPointsForDockContainer((QDockContainer) dockContainer.Controls[index], owningForm, control);
            else if (dockContainer.Controls[index] is QDockingWindow)
              this.AddDockPointsForWindow((QDockingWindow) dockContainer.Controls[index], control);
          }
        }
      }
    }

    private void AddDockPointsForDockForm(QDockForm form, Form owningForm, QDockControl control)
    {
      if (form.Controls.Count > 0 && form.Controls[0] != control && form.Controls[0].Visible && form.Controls[0] is QDockContainer && (form.Controls[0].Controls.Count == 1 || ((QDockContainer) form.Controls[0]).IsTabbed) && control.CanDockOnOtherControlTabbed)
      {
        QDockContainer control1 = (QDockContainer) form.Controls[0];
        this.Add(new QDockPoint(form.RectangleToScreen(new Rectangle(0, -SystemInformation.ToolWindowCaptionHeight, form.Width, SystemInformation.ToolWindowCaptionHeight)), control1.DockPosition, (Control) control1, -1, (QDockBar) null, QDockOrientation.Tabbed, (QDockControl) control1.CurrentWindow));
      }
      for (int index = 0; index < form.Controls.Count; ++index)
      {
        if (form.Controls[index] != control && form.Controls[index] is QDockContainer && form.Controls[index].Visible)
          this.AddDockPointsForDockContainer((QDockContainer) form.Controls[index], owningForm, control);
      }
    }

    public void CalculateDockPoints(Form owningForm, QDockControl control)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      Rectangle empty = Rectangle.Empty;
      Rectangle screen;
      for (int index = 0; index < owningForm.Controls.Count; ++index)
      {
        Control control1 = owningForm.Controls[index];
        int insertIndex = owningForm.Controls.IndexOf(control1);
        QDockBar dockBar = control1 as QDockBar;
        QDockContainer dockContainer = control1 as QDockContainer;
        QDockControl qdockControl = control1 as QDockControl;
        if (control1 != control && control1.Visible && (!control.CanDockOnlyNearDockBar || qdockControl != null || dockBar != null))
        {
          screen = owningForm.RectangleToScreen(control1.Bounds);
          if (dockBar == null && dockContainer != null)
          {
            dockBar = dockContainer.DockBar;
            this.AddDockPointsForDockContainer(dockContainer, owningForm, control);
          }
          switch (control1.Dock)
          {
            case DockStyle.Top:
              if (control.CanDockTop)
                this.Add(new QDockPoint(screen.Left, screen.Bottom - this.m_iMousePositionMarginForDock, screen.Width, this.m_iMousePositionMarginForDock * 2, QDockPosition.Top, (Control) owningForm, insertIndex, dockBar, QDockOrientation.None, (QDockControl) null));
              num3 = Math.Max(num3, insertIndex + 1);
              continue;
            case DockStyle.Bottom:
              if (control.CanDockBottom)
                this.Add(new QDockPoint(screen.Left, screen.Top - this.m_iMousePositionMarginForDock, screen.Width, this.m_iMousePositionMarginForDock * 2, QDockPosition.Bottom, (Control) owningForm, insertIndex, dockBar, QDockOrientation.None, (QDockControl) null));
              num4 = Math.Max(num4, insertIndex + 1);
              continue;
            case DockStyle.Left:
              if (control.CanDockLeft)
                this.Add(new QDockPoint(screen.Right - this.m_iMousePositionMarginForDock, screen.Top, this.m_iMousePositionMarginForDock * 2, screen.Height, QDockPosition.Left, (Control) owningForm, insertIndex, dockBar, QDockOrientation.None, (QDockControl) null));
              num1 = Math.Max(num1, insertIndex + 1);
              continue;
            case DockStyle.Right:
              if (control.CanDockRight)
                this.Add(new QDockPoint(screen.Left - this.m_iMousePositionMarginForDock, screen.Top, this.m_iMousePositionMarginForDock * 2, screen.Height, QDockPosition.Right, (Control) owningForm, insertIndex, dockBar, QDockOrientation.None, (QDockControl) null));
              num2 = Math.Max(num2, insertIndex + 1);
              continue;
            default:
              continue;
          }
        }
      }
      for (int index = 0; index < owningForm.OwnedForms.Length; ++index)
      {
        if (owningForm.OwnedForms[index] is QDockForm && owningForm.OwnedForms[index].Visible)
          this.AddDockPointsForDockForm((QDockForm) owningForm.OwnedForms[index], owningForm, control);
      }
      if (!control.CanDockOnFormBorder)
        return;
      screen = owningForm.RectangleToScreen(owningForm.ClientRectangle);
      if (control.CanDockLeft)
        this.Add(new QDockPoint(screen.Left - this.m_iMousePositionMarginForDock, screen.Top, this.m_iMousePositionMarginForDock * 2, screen.Height, QDockPosition.Left, (Control) owningForm, num1, (QDockBar) null, QDockOrientation.None, (QDockControl) null));
      if (control.CanDockRight)
        this.Add(new QDockPoint(screen.Right - this.m_iMousePositionMarginForDock, screen.Top, this.m_iMousePositionMarginForDock * 2, screen.Height, QDockPosition.Right, (Control) owningForm, num2, (QDockBar) null, QDockOrientation.None, (QDockControl) null));
      if (control.CanDockTop)
        this.Add(new QDockPoint(screen.Left, screen.Top - this.m_iMousePositionMarginForDock, screen.Width, this.m_iMousePositionMarginForDock * 2, QDockPosition.Top, (Control) owningForm, num3, (QDockBar) null, QDockOrientation.None, (QDockControl) null));
      if (!control.CanDockBottom)
        return;
      this.Add(new QDockPoint(screen.Left, screen.Bottom - this.m_iMousePositionMarginForDock, screen.Width, this.m_iMousePositionMarginForDock * 2, QDockPosition.Bottom, (Control) owningForm, num4, (QDockBar) null, QDockOrientation.None, (QDockControl) null));
    }
  }
}
