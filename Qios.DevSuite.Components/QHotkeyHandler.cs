// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QHotkeyHandler
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QHotkeyHandler
  {
    private IQHotkeyHandlerHost m_oHost;
    private IQHotkeyItem m_oProcessingChildItem;
    private ArrayList m_aCurrentHotkeyItems;
    private Keys[] m_aPressedHotkeySequence;
    private QHotkeyHandlerState m_eState;
    private bool m_bShouldShowHotkeyWindows;
    private int m_iShowSuspended;
    private bool m_bHotkeyWindowsVisible;

    public QHotkeyHandler(IQHotkeyHandlerHost host) => this.m_oHost = host;

    public bool ShouldShowHotkeyWindows
    {
      get => this.m_bShouldShowHotkeyWindows;
      set
      {
        if (this.m_bShouldShowHotkeyWindows == value)
          return;
        if (this.IsProcessing)
        {
          if (this.m_bHotkeyWindowsVisible)
            this.HideHotkeyWindows();
          this.m_bShouldShowHotkeyWindows = value;
          if (!this.m_bShouldShowHotkeyWindows)
            return;
          this.ShowHotkeyWindows();
        }
        else
          this.m_bShouldShowHotkeyWindows = value;
      }
    }

    public void SuspendShow() => ++this.m_iShowSuspended;

    public void ResumeShow()
    {
      this.m_iShowSuspended = Math.Max(--this.m_iShowSuspended, 0);
      if (this.m_iShowSuspended != 0 || !this.IsProcessing || !this.m_bShouldShowHotkeyWindows || this.m_bHotkeyWindowsVisible)
        return;
      this.ShowHotkeyWindows();
    }

    public bool HotkeyWindowsVisible => this.m_bHotkeyWindowsVisible;

    private void ResetHotkeyItemArray()
    {
      if (this.m_aCurrentHotkeyItems == null)
        this.m_aCurrentHotkeyItems = new ArrayList();
      else
        this.m_aCurrentHotkeyItems.Clear();
    }

    internal void StartProcessing() => this.StartProcessing(true);

    internal void StartProcessing(bool selectItem)
    {
      if (this.m_eState == QHotkeyHandlerState.None)
      {
        this.m_eState = QHotkeyHandlerState.Processing;
        this.StartProcessingItem(selectItem, (IQHotkeyItem) null);
      }
      else
      {
        if (this.m_eState != QHotkeyHandlerState.Suspended)
          return;
        this.ResumeProcessing(selectItem);
      }
    }

    private void StartProcessingItem(bool selectItem, IQHotkeyItem item)
    {
      this.m_oProcessingChildItem = item;
      this.m_aPressedHotkeySequence = (Keys[]) null;
      this.HideHotkeyWindows();
      this.ResetHotkeyItemArray();
      if (item != null)
      {
        item.AddChildHotkeyItems((IList) this.m_aCurrentHotkeyItems);
        QHotkeyHelper.SetParentHotkeyItem(item, (IEnumerable) this.m_aCurrentHotkeyItems);
        if (selectItem)
          item.Select(true, QNavigationSelectionReason.OneOfMultipleItems, QNavigationActivationType.Keyboard);
      }
      else
      {
        this.m_oHost.AddHotkeyItems((IList) this.m_aCurrentHotkeyItems);
        QHotkeyHelper.SetParentHotkeyItem((IQHotkeyItem) null, (IEnumerable) this.m_aCurrentHotkeyItems);
        if (selectItem)
          QHotkeyHelper.GetNextHotkeyItem((IList) this.m_aCurrentHotkeyItems, (IQHotkeyItem) null, false)?.Select(true, QNavigationSelectionReason.OneOfMultipleItems, QNavigationActivationType.Keyboard);
      }
      if (!this.m_bShouldShowHotkeyWindows)
        return;
      this.ShowHotkeyWindows();
    }

    internal void SuspendProcessing()
    {
      if (this.m_eState != QHotkeyHandlerState.Processing)
        return;
      this.HideHotkeyWindows();
      this.m_eState = QHotkeyHandlerState.Suspended;
    }

    internal void ResumeProcessing() => this.ResumeProcessing(true);

    internal void ResumeProcessing(bool selectItem)
    {
      if (this.m_eState != QHotkeyHandlerState.Suspended)
        return;
      this.m_eState = QHotkeyHandlerState.Processing;
      this.StartProcessingItem(selectItem, this.m_oProcessingChildItem);
    }

    internal void StopProcessing() => this.StopProcessing(true);

    internal void StopProcessing(bool resetCurrentSelectedItem)
    {
      if (this.m_eState == QHotkeyHandlerState.None)
        return;
      this.m_eState = QHotkeyHandlerState.None;
      if (resetCurrentSelectedItem && this.CurrentSelectedItem != null)
        this.CurrentSelectedItem.Select(false, QNavigationSelectionReason.None, QNavigationActivationType.None);
      this.HideHotkeyWindows();
      this.m_aCurrentHotkeyItems.Clear();
      this.m_aPressedHotkeySequence = (Keys[]) null;
      this.m_oProcessingChildItem = (IQHotkeyItem) null;
    }

    internal bool IsProcessing => this.m_eState == QHotkeyHandlerState.Processing;

    internal bool IsSuspended => this.m_eState == QHotkeyHandlerState.Suspended;

    internal bool IsIdle => this.m_eState == QHotkeyHandlerState.None;

    internal IQHotkeyItem CurrentSelectedItem => this.m_oHost.SelectedItem;

    internal void ShowHotkeyWindows()
    {
      if (this.m_iShowSuspended > 0)
        return;
      for (int index = 0; index < this.m_aCurrentHotkeyItems.Count; ++index)
      {
        if (this.m_aCurrentHotkeyItems[index] is IQHotkeyItem currentHotkeyItem && currentHotkeyItem.Visible && currentHotkeyItem.HasHotkey)
        {
          this.m_oHost.ConfigureHotkeyWindow(currentHotkeyItem);
          currentHotkeyItem.ShowHotkeyWindow(true);
          this.m_bHotkeyWindowsVisible = true;
        }
      }
    }

    internal void HideHotkeyWindows()
    {
      if (this.m_bHotkeyWindowsVisible)
        QHotkeyHelper.HideHotkeyWindows((IEnumerable) this.m_aCurrentHotkeyItems);
      this.m_bHotkeyWindowsVisible = false;
    }

    internal bool HandleHotkey(Keys key)
    {
      bool flag = false;
      this.SuspendShow();
      try
      {
        if (!this.IsProcessing)
        {
          this.ShouldShowHotkeyWindows = false;
          this.StartProcessing(false);
        }
        Keys[] keysArray = QHotkeyHelper.AddPressedKey(this.m_aPressedHotkeySequence, key);
        switch (QHotkeyHelper.GetHotkeyItemCount(keysArray, false, (IList) this.m_aCurrentHotkeyItems))
        {
          case 0:
            flag = false;
            break;
          case 1:
            QHotkeyHelper.FilterHotkeyItems(keysArray, false, (IList) this.m_aCurrentHotkeyItems);
            IQHotkeyItem currentHotkeyItem = this.m_aCurrentHotkeyItems[0] as IQHotkeyItem;
            if (currentHotkeyItem.MatchesHotkeySequence(keysArray, true))
            {
              this.m_aPressedHotkeySequence = (Keys[]) null;
              this.ActivateHotkeyItem(currentHotkeyItem, QNavigationActivationReason.None, QNavigationActivationType.Hotkey);
            }
            else
              this.m_aPressedHotkeySequence = keysArray;
            flag = true;
            break;
          default:
            QHotkeyHelper.FilterHotkeyItems(keysArray, false, (IList) this.m_aCurrentHotkeyItems);
            if (QHotkeyHelper.AllItemsMatchesHotkeySequence(keysArray, (IEnumerable) this.m_aCurrentHotkeyItems, true))
              QHotkeyHelper.GetNextHotkeyItem((IList) this.m_aCurrentHotkeyItems, this.CurrentSelectedItem, true)?.Select(true, QNavigationSelectionReason.OneOfMultipleItems, QNavigationActivationType.Hotkey);
            else
              this.m_aPressedHotkeySequence = keysArray;
            flag = true;
            break;
        }
      }
      finally
      {
        this.ResumeShow();
      }
      return flag;
    }

    internal void ActivateHotkeyItem(
      IQHotkeyItem item,
      QNavigationActivationReason reason,
      QNavigationActivationType activationType)
    {
      if (item == null || !item.Enabled)
        return;
      this.HideHotkeyWindows();
      if (this.CurrentSelectedItem != null)
        this.CurrentSelectedItem.Select(false, QNavigationSelectionReason.None, activationType);
      item.Activate(true, reason, activationType);
      this.m_aPressedHotkeySequence = (Keys[]) null;
      if (item.HasChildHotkeyItems)
        this.StartProcessingItem(true, item);
      else
        this.SuspendProcessing();
    }

    internal bool HandleActivationKey()
    {
      if (this.CurrentSelectedItem == null)
        return false;
      this.ActivateHotkeyItem(this.CurrentSelectedItem, QNavigationActivationReason.None, QNavigationActivationType.Keyboard);
      return true;
    }

    internal bool HandleDeactivationKey()
    {
      if (!this.IsProcessing)
        return false;
      this.m_aPressedHotkeySequence = (Keys[]) null;
      if (this.m_oProcessingChildItem != null)
        this.StartProcessingItem(true, this.m_oProcessingChildItem.ParentHotkeyItem);
      else
        this.StopProcessing();
      return true;
    }
  }
}
