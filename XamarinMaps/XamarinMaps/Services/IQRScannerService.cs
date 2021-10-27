using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinMaps.Services
{
    public interface IQRScannerService
    {
        Task<string> ScanQrCodeAsync();
    }
}
