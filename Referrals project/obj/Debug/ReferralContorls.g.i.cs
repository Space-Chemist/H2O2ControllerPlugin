﻿#pragma checksum "..\..\ReferralContorls.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "741D15FF8DADDB12EFCEF19E46BEE2D464EAA7318C9F2FF74ABFA4642ABF57BF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Referrals_project;
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


namespace Referrals_project {
    
    
    /// <summary>
    /// ReferralControls
    /// </summary>
    public partial class ReferralControls : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\ReferralContorls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ReferralRewardsEnabled;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ReferralContorls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox PromotionRewardEnabled;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ReferralContorls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ServerReferralCode;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\ReferralContorls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PromotionRewardsCode;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\ReferralContorls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup ServerReferralRewardsConfig;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\ReferralContorls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup UserReferralRewardsConfig;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\ReferralContorls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup PromotionCodeRewardsConfig;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\ReferralContorls.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Info;
        
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
            System.Uri resourceLocater = new System.Uri("/Referrals project;component/referralcontorls.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ReferralContorls.xaml"
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
            this.ReferralRewardsEnabled = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 2:
            this.PromotionRewardEnabled = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 3:
            this.ServerReferralCode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.PromotionRewardsCode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 46 "..\..\ReferralContorls.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ServerReferralRewardConfigButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 47 "..\..\ReferralContorls.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UserReferralRewardConfigButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 48 "..\..\ReferralContorls.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PromotionCodeRewardConfigButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ServerReferralRewardsConfig = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 9:
            
            #line 62 "..\..\ReferralContorls.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ServerReferralRewardConfigCloseButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.UserReferralRewardsConfig = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 11:
            
            #line 79 "..\..\ReferralContorls.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UserReferralRewardConfigCloseButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 12:
            this.PromotionCodeRewardsConfig = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 13:
            
            #line 96 "..\..\ReferralContorls.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PromotionCodeRewardConfigCloseButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 14:
            this.Info = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

