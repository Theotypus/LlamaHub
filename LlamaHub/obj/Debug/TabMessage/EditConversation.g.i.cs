﻿#pragma checksum "..\..\..\TabMessage\EditConversation.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8F787479854AEFBD35C3961854ECDF24CAFB0FD0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Hub.TabMessage;
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


namespace Hub.TabMessage {
    
    
    /// <summary>
    /// EditConversation
    /// </summary>
    public partial class EditConversation : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\TabMessage\EditConversation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle Image;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\TabMessage\EditConversation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Header;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\TabMessage\EditConversation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\TabMessage\EditConversation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchBox;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\TabMessage\EditConversation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ResultBox;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\TabMessage\EditConversation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ParticipantsBox;
        
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
            System.Uri resourceLocater = new System.Uri("/LlamaHub;component/tabmessage/editconversation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TabMessage\EditConversation.xaml"
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
            this.Image = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 2:
            
            #line 36 "..\..\..\TabMessage\EditConversation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 42 "..\..\..\TabMessage\EditConversation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Ok_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Header = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.NameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 60 "..\..\..\TabMessage\EditConversation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Open_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SearchBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 75 "..\..\..\TabMessage\EditConversation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Search_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ResultBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 81 "..\..\..\TabMessage\EditConversation.xaml"
            this.ResultBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Results_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ParticipantsBox = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

