// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QControlHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QControlHelper
  {
    private QControlHelper()
    {
    }

    public static void UpdateControlRoot(Control control) => (typeof (Control).GetMethod("UpdateRoot", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ?? throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "GetMethod(\"UpdateRoot\")"))).Invoke((object) control, (object[]) null);

    public static bool PatchActiveControlRemoval(ContainerControl containerControl)
    {
      if (Environment.Version.Major >= 2 || containerControl == null || containerControl.ActiveControl != null || containerControl.Parent == null)
        return false;
      Control parent = containerControl.Parent;
      IContainerControl containerControl1;
      for (containerControl1 = parent as IContainerControl; parent != null && containerControl1 == null; containerControl1 = (IContainerControl) (parent as ContainerControl))
        parent = parent.Parent;
      if (containerControl1 == null || containerControl1.ActiveControl != containerControl)
        return false;
      containerControl1.ActiveControl = (Control) null;
      return true;
    }

    public static MdiClient GetMdiClient(Form form)
    {
      if (form != null && form.IsMdiContainer)
      {
        for (int index = 0; index < form.Controls.Count; ++index)
        {
          if (form.Controls[index] is MdiClient control)
            return control;
        }
      }
      return (MdiClient) null;
    }

    public static Control GetActiveMaximizedMdiChild(MdiClient mdiClient)
    {
      if (mdiClient == null)
        return (Control) null;
      Control activeMdiChild = NativeHelper.GetActiveMdiChild(mdiClient);
      FormWindowState currentFormState = NativeHelper.GetCurrentFormState(activeMdiChild as Form);
      return activeMdiChild != null && currentFormState == FormWindowState.Maximized ? activeMdiChild : (Control) null;
    }

    public static QMargin GetDefaultNonClientAreaMargin(
      CreateParams createParams,
      MainMenu menu)
    {
      Rectangle rectangle = new Rectangle(0, 0, 200, 200);
      NativeMethods.RECT rect = NativeHelper.CreateRECT(rectangle);
      bool bMenu = menu != null && menu.MenuItems.Count > 0;
      NativeMethods.AdjustWindowRectEx(ref rect, createParams.Style, bMenu, createParams.ExStyle);
      return new QMargin(rectangle.Left - rect.left, rectangle.Top - rect.top, rect.right - rectangle.Right, rect.bottom - rectangle.Bottom);
    }

    public static Control GetParentOrOwner(Control control) => control.Parent == null ? QControlHelper.GetFirstControlFromHandle(NativeMethods.GetParent(control.Handle)) : control.Parent;

    public static IntPtr GetUndisposedHandle(IWin32Window window)
    {
      if (window is Control control && !control.IsDisposed)
        return control.Handle;
      return control == null && window != null ? window.Handle : IntPtr.Zero;
    }

    public static Control GetFirstControlFromHandle(IntPtr handle)
    {
      Control controlFromHandle = Control.FromHandle(handle);
      for (IntPtr index = handle; controlFromHandle == null && index != IntPtr.Zero; controlFromHandle = Control.FromHandle(index))
        index = NativeMethods.GetParent(index);
      return controlFromHandle;
    }

    public static bool ControlContainsOrIsWindowOnPoint(Control parentControl, Point point) => QControlHelper.ControlContainsOrIsWindowOnPoint(parentControl, point, out IntPtr _);

    public static bool ControlContainsOrIsWindowOnPoint(
      Control parentControl,
      Point point,
      out IntPtr windowHandle)
    {
      windowHandle = IntPtr.Zero;
      Point client = parentControl.PointToClient(point);
      if (!parentControl.ClientRectangle.Contains(client))
        return false;
      NativeMethods.POINT Point = new NativeMethods.POINT(client.X, client.Y);
      windowHandle = NativeMethods.ChildWindowFromPoint(parentControl.Handle, Point);
      IntPtr hWnd = windowHandle;
      while (hWnd != IntPtr.Zero && hWnd != parentControl.Handle)
        hWnd = NativeMethods.GetAncestor(hWnd, 1U);
      return hWnd != IntPtr.Zero;
    }

    public static void ForceControlInternalVisibilityProperty(Control control, bool value) => QControlHelper.ForceControlInternalStateProperty(control, 2, value);

    public static bool GetControlInternalVisibilityProperty(Control control) => QControlHelper.GetControlInternalStateProperty(control, 2);

    public static void ForceControlInternalStateProperty(Control control, int state, bool value) => (typeof (Control).GetMethod("SetState", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new System.Type[2]
    {
      typeof (int),
      typeof (bool)
    }, (ParameterModifier[]) null) ?? throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "GetMethod(\"SetState(int, bool)\")"))).Invoke((object) control, new object[2]
    {
      (object) state,
      (object) value
    });

    public static bool GetControlInternalStateProperty(Control control, int state) => (bool) (typeof (Control).GetMethod("GetState", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new System.Type[1]
    {
      typeof (int)
    }, (ParameterModifier[]) null) ?? throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "GetMethod(\"GetState(int)\")"))).Invoke((object) control, new object[1]
    {
      (object) state
    });

    public static void SecureAllControlHandles(Control control, bool recursive) => QControlHelper.SecureAllControlHandles(control, recursive, typeof (Control).GetMethod("CreateControl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new System.Type[1]
    {
      typeof (bool)
    }, (ParameterModifier[]) null) ?? throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "GetMethod(\"CreateControl(bool)\")")));

    private static void SecureAllControlHandles(
      Control control,
      bool recursive,
      MethodInfo methodInfo)
    {
      if (control == null || control.IsDisposed)
        return;
      if (!control.IsHandleCreated)
        methodInfo.Invoke((object) control, new object[1]
        {
          (object) true
        });
      if (!recursive)
        return;
      for (int index = 0; index < control.Controls.Count; ++index)
        QControlHelper.SecureAllControlHandles(control.Controls[index], true, methodInfo);
    }

    internal static byte GetLayoutSuspendCount(Control control) => (byte) (typeof (Control).GetField("layoutSuspendCount", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "GetField(\"layoutSuspendCount\")"))).GetValue((object) control);
  }
}
