﻿#pragma checksum "..\..\..\QuizGame.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2C5B63024D4690D3600243A90610FEDAEE393FFF"
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
    /// QuizGame
    /// </summary>
    public partial class QuizGame : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\QuizGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Score;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\QuizGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Timer;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\QuizGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Question;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\QuizGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button choice1;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\QuizGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button choice2;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\QuizGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button choice3;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\QuizGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button choice4;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\QuizGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock endText;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\QuizGame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button retry;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\QuizGame.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Games;component/quizgame.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\QuizGame.xaml"
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
            this.Timer = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Question = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.choice1 = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\QuizGame.xaml"
            this.choice1.Click += new System.Windows.RoutedEventHandler(this.choice1click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.choice2 = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\QuizGame.xaml"
            this.choice2.Click += new System.Windows.RoutedEventHandler(this.choice2click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.choice3 = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\QuizGame.xaml"
            this.choice3.Click += new System.Windows.RoutedEventHandler(this.choice3click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.choice4 = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\QuizGame.xaml"
            this.choice4.Click += new System.Windows.RoutedEventHandler(this.choice4click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.endText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.retry = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\QuizGame.xaml"
            this.retry.Click += new System.Windows.RoutedEventHandler(this.RetryClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.toStore = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\QuizGame.xaml"
            this.toStore.Click += new System.Windows.RoutedEventHandler(this.toStoreClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

