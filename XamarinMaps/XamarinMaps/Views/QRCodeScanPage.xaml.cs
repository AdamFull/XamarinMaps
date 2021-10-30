using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMaps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRCodeScanPage : ContentPage
    {
        public QRCodeScanPage()
        {
            InitializeComponent();
        }
    }
}