﻿#pragma checksum "..\..\..\ChangeParameterWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8242065A1D6DA23E16BB0B72CAF2F37D8E0792F8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Don_tKnowHowToNameThis;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Don_tKnowHowToNameThis {
    
    
    /// <summary>
    /// ChangeParameterWindow
    /// </summary>
    public partial class ChangeParameterWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\ChangeParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ID_parameterComboBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\ChangeParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ChangeNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\ChangeParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ChangeSymbolTextBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\ChangeParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ChangeUnitTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Don`tKnowHowToNameThis;component/changeparameterwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ChangeParameterWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ID_parameterComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 15 "..\..\..\ChangeParameterWindow.xaml"
            this.ID_parameterComboBox.DropDownClosed += new System.EventHandler(this.ID_parameterComboBox_OnDropDownClosed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ChangeNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ChangeSymbolTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ChangeUnitTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 26 "..\..\..\ChangeParameterWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonRefresh_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

