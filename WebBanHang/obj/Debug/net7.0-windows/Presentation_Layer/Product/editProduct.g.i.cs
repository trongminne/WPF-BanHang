﻿#pragma checksum "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D7B22D09D21D078549F90D56EDF35D289A3A789F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using WebBanHang.Presentation_Layer.Product;


namespace WebBanHang.Presentation_Layer.Product {
    
    
    /// <summary>
    /// editProduct
    /// </summary>
    public partial class editProduct : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 203 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EditProductName;
        
        #line default
        #line hidden
        
        
        #line 211 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox EditProductCategoryId;
        
        #line default
        #line hidden
        
        
        #line 221 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox EditProductBrandId;
        
        #line default
        #line hidden
        
        
        #line 232 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EditProductPrice;
        
        #line default
        #line hidden
        
        
        #line 240 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EditProductSale;
        
        #line default
        #line hidden
        
        
        #line 248 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EditProductQuantity;
        
        #line default
        #line hidden
        
        
        #line 255 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EditProductId;
        
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
            System.Uri resourceLocater = new System.Uri("/WebBanHang;component/presentation_layer/product/editproduct.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
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
            
            #line 187 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MoseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 197 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
            ((MaterialDesignThemes.Wpf.PackIcon)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Colse_MoseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.EditProductName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.EditProductCategoryId = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.EditProductBrandId = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.EditProductPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.EditProductSale = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.EditProductQuantity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.EditProductId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            
            #line 266 "..\..\..\..\..\Presentation_Layer\Product\editProduct.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

