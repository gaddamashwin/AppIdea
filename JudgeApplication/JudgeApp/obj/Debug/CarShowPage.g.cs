﻿

#pragma checksum "C:\Users\asgaddam\Documents\GitHub\AppIdea\JudgeApplication\JudgeApp\CarShowPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B3728D1FDA8E58ED1983722BC604C917"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JudgeApp
{
    partial class CarShowPage : global::JudgeApp.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 44 "..\..\CarShowPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.itemGridView_ItemClick_1;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 74 "..\..\CarShowPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.itemGridView_ItemClick_1;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 101 "..\..\CarShowPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 103 "..\..\CarShowPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.backSync_Click_1;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


