﻿#pragma checksum "..\..\..\..\Calibration\CalibrationView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CB2567C708581052C34B79ABBA25C514"
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
using System.Windows.Controls.DataVisualization;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.DataVisualization.Charting.Primitives;
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
using WpfGS;


namespace WpfGS {
    
    
    /// <summary>
    /// CalibrationView
    /// </summary>
    public partial class CalibrationView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\Calibration\CalibrationView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView treeview;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Calibration\CalibrationView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem treeOpen;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Calibration\CalibrationView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem treeNew;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Calibration\CalibrationView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Calibration\CalibrationView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid2;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Calibration\CalibrationView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataVisualization.Charting.Chart lineChart;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfGS;component/calibration/calibrationview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Calibration\CalibrationView.xaml"
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
            this.treeview = ((System.Windows.Controls.TreeView)(target));
            
            #line 19 "..\..\..\..\Calibration\CalibrationView.xaml"
            this.treeview.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.treeOpen_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.treeOpen = ((System.Windows.Controls.MenuItem)(target));
            
            #line 31 "..\..\..\..\Calibration\CalibrationView.xaml"
            this.treeOpen.Click += new System.Windows.RoutedEventHandler(this.treeOpen_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.treeNew = ((System.Windows.Controls.MenuItem)(target));
            
            #line 32 "..\..\..\..\Calibration\CalibrationView.xaml"
            this.treeNew.Click += new System.Windows.RoutedEventHandler(this.treeNew_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.datagrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 39 "..\..\..\..\Calibration\CalibrationView.xaml"
            this.datagrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.datagrid_Selected);
            
            #line default
            #line hidden
            return;
            case 5:
            this.datagrid2 = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.lineChart = ((System.Windows.Controls.DataVisualization.Charting.Chart)(target));
            return;
            case 7:
            
            #line 71 "..\..\..\..\Calibration\CalibrationView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDelete_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 73 "..\..\..\..\Calibration\CalibrationView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAdd_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 75 "..\..\..\..\Calibration\CalibrationView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDelete2_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 77 "..\..\..\..\Calibration\CalibrationView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAdd2_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 80 "..\..\..\..\Calibration\CalibrationView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonTest_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 82 "..\..\..\..\Calibration\CalibrationView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddFile_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 84 "..\..\..\..\Calibration\CalibrationView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveFile_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

