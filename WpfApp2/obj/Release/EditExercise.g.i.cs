﻿#pragma checksum "..\..\EditExercise.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7C0D234CB7583E7FCF492D16A405B724C43ACC69BF7353A7E8FA4AC3B63EDCCD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WpfApp2;


namespace WpfApp2 {
    
    
    /// <summary>
    /// EditExercise
    /// </summary>
    public partial class EditExercise : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\EditExercise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name_box;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\EditExercise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox sets_combo;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\EditExercise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox reps_box;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\EditExercise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RPE_combo;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\EditExercise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox fatique_combo;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\EditExercise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox fatique_protocol_combo;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\EditExercise.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox protocol_combo;
        
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
            System.Uri resourceLocater = new System.Uri("/RTS_program_maker;component/editexercise.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EditExercise.xaml"
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
            
            #line 9 "..\..\EditExercise.xaml"
            ((WpfApp2.EditExercise)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.CloseWindow);
            
            #line default
            #line hidden
            return;
            case 2:
            this.name_box = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.sets_combo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.reps_box = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.RPE_combo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.fatique_combo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.fatique_protocol_combo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.protocol_combo = ((System.Windows.Controls.ComboBox)(target));
            
            #line 57 "..\..\EditExercise.xaml"
            this.protocol_combo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.protocol_change);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 63 "..\..\EditExercise.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.delete_exercise);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

