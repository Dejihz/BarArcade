﻿#pragma checksum "..\..\..\WhackAMoleGame.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AF9F36A10690D0606D8428DABC0D3C59A9DD332D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Games;
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


namespace Games {
    
    
    /// <summary>
    /// WhackAMoleGame
    /// </summary>
    public partial class WhackAMoleGame : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\WhackAMoleGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Score;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\WhackAMoleGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas GameCanvas;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\WhackAMoleGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle LifeBar;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\WhackAMoleGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Life;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\WhackAMoleGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock endText;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\WhackAMoleGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button retry;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\WhackAMoleGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button toStore;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Games;component/whackamolegame.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WhackAMoleGame.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Score = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.GameCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 20 "..\..\..\WhackAMoleGame.xaml"
            this.GameCanvas.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.leftClicked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LifeBar = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 4:
            this.Life = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.endText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.retry = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\WhackAMoleGame.xaml"
            this.retry.Click += new System.Windows.RoutedEventHandler(this.RetryClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.toStore = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\WhackAMoleGame.xaml"
            this.toStore.Click += new System.Windows.RoutedEventHandler(this.toStoreClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

