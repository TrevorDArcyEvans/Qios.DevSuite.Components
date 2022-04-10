// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCloneBehaviorAttribute
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [AttributeUsage(AttributeTargets.Field)]
  internal sealed class QCloneBehaviorAttribute : Attribute
  {
    private QCloneBehaviorType m_eBehaviorType;

    public QCloneBehaviorAttribute(QCloneBehaviorType behaviorType) => this.m_eBehaviorType = behaviorType;

    public QCloneBehaviorType BehaviorType => this.m_eBehaviorType;
  }
}
