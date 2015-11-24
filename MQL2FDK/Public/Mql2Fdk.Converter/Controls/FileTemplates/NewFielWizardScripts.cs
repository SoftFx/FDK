using System;

namespace Mql2Fdk.Converter.Controls.FileTemplates
{
    static class NewFielWizardScripts
    {
        public const string StandardFile = @"
";
        public const string NewLine = @"
";
        public const string ScriptFile = @"
//+------------------------------------------------------------------+
//|                                                       script.mq4 |
//+------------------------------------------------------------------+"+
                                         NewLine +
                                         "#property copyright \"\"" +
                                         NewLine +
                                         "#property link      \"\"" +
                                         NewLine +
                                         @"
//+------------------------------------------------------------------+
//| script program start function                                    |
//+------------------------------------------------------------------+
int start()
  {
//----
   
//----
   return(0);
  }
//+------------------------------------------------------------------+
";
    }
}