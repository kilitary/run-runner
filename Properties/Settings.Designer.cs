//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace run_runner.Properties {
    
    
    [CompilerGenerated()]
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.1.0.0")]
    internal sealed partial class Settings : ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [ApplicationScopedSetting()]
        [DebuggerNonUserCode()]
        [DefaultSettingValue("1")]
        public char Setting {
            get {
                return ((char)(this["Setting"]));
            }
        }
        
        [UserScopedSetting()]
        [DebuggerNonUserCode()]
        [DefaultSettingValue("2")]
        public string Setting1 {
            get {
                return ((string)(this["Setting1"]));
            }
            set {
                this["Setting1"] = value;
            }
        }
    }
}
