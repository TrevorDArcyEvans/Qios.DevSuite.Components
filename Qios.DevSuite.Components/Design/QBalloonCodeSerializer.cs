// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QBalloonCodeSerializer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

namespace Qios.DevSuite.Components.Design
{
  public class QBalloonCodeSerializer : CodeDomSerializer
  {
    public override object Serialize(IDesignerSerializationManager manager, object value)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      QBalloon qballoon = (QBalloon) value;
      object obj = ((CodeDomSerializer) manager.GetSerializer(typeof (QBalloon).BaseType, typeof (CodeDomSerializer))).Serialize(manager, (object) qballoon);
      if (obj is CodeStatementCollection statementCollection)
      {
        CodeExpression expression1 = this.SerializeToExpression(manager, (object) qballoon);
        QBalloonExtender extenderProviderOnSite = QBalloonDesigner.GetExtenderProviderOnSite(qballoon.Site, false);
        if (extenderProviderOnSite != null)
        {
          QBalloonExtenderPair[] pairs = extenderProviderOnSite.GetPairs(qballoon);
          CodeFieldReferenceExpression referenceExpression = expression1 as CodeFieldReferenceExpression;
          if (pairs.Length > 0 && statementCollection.Count == 1 && referenceExpression != null)
          {
            statementCollection.Add((CodeStatement) new CodeCommentStatement(string.Empty));
            statementCollection.Add((CodeStatement) new CodeCommentStatement(referenceExpression.FieldName));
            statementCollection.Add((CodeStatement) new CodeCommentStatement(string.Empty));
          }
          for (int index = 0; index < pairs.Length; ++index)
          {
            bool flag = false;
            IDesignerHost service = (IDesignerHost) manager.GetService(typeof (IDesignerHost));
            if (service != null)
            {
              PropertyDescriptor property = TypeDescriptor.GetProperties((object) service.RootComponent)["Localizable"];
              if (property != null && property.PropertyType == typeof (bool) && (bool) property.GetValue((object) service.RootComponent))
                flag = true;
            }
            CodeExpression expression2 = this.SerializeToExpression(manager, (object) pairs[index].Component);
            CodeExpression codeExpression = (CodeExpression) null;
            ExpressionContext context = new ExpressionContext(expression2, typeof (CodeExpression), (object) qballoon);
            manager.Context.Push((object) context);
            try
            {
              codeExpression = flag ? this.SerializeToResourceExpression(manager, (object) pairs[index].Text) : this.SerializeToExpression(manager, (object) pairs[index].Text);
            }
            finally
            {
              manager.Context.Pop();
            }
            statementCollection.Add((CodeExpression) new CodeMethodInvokeExpression(expression1, "AddListener", new CodeExpression[2]
            {
              expression2,
              codeExpression
            }));
          }
        }
      }
      return obj;
    }

    public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      return ((CodeDomSerializer) manager.GetSerializer(typeof (QBalloon).BaseType, typeof (CodeDomSerializer))).Deserialize(manager, codeObject);
    }
  }
}
