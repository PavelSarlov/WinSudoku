﻿#pragma checksum "..\..\DifficultyDialogBox.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E66C5A62EF07A268B4DC8BD7D3A2FB7EEEF344756BF2693634C64FD65A7D007D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Sudoku;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Sudoku {
    
    
    /// <summary>
    /// DifficultyDialogBox
    /// </summary>
    public partial class DifficultyDialogBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\DifficultyDialogBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbtnEasy;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\DifficultyDialogBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbtnMedium;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\DifficultyDialogBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbtnHard;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\DifficultyDialogBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnContinue;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\DifficultyDialogBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Sudoku;component/difficultydialogbox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DifficultyDialogBox.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.rbtnEasy = ((System.Windows.Controls.RadioButton)(target));
            
            #line 25 "..\..\DifficultyDialogBox.xaml"
            this.rbtnEasy.Checked += new System.Windows.RoutedEventHandler(this.OptionChosen);
            
            #line default
            #line hidden
            return;
            case 2:
            this.rbtnMedium = ((System.Windows.Controls.RadioButton)(target));
            
            #line 26 "..\..\DifficultyDialogBox.xaml"
            this.rbtnMedium.Checked += new System.Windows.RoutedEventHandler(this.OptionChosen);
            
            #line default
            #line hidden
            return;
            case 3:
            this.rbtnHard = ((System.Windows.Controls.RadioButton)(target));
            
            #line 27 "..\..\DifficultyDialogBox.xaml"
            this.rbtnHard.Checked += new System.Windows.RoutedEventHandler(this.OptionChosen);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnContinue = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\DifficultyDialogBox.xaml"
            this.btnContinue.Click += new System.Windows.RoutedEventHandler(this.BtnContinue_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\DifficultyDialogBox.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.BtnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
