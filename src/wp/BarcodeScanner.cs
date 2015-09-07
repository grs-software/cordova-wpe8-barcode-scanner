using System;
using System.Text;

using Windows.Devices.PointOfService;
using Windows.Storage.Streams;

namespace WPCordovaClassLib.Cordova.Commands
{
    public class BarcodeScanner : BaseCommand
    {
        Windows.Devices.PointOfService.BarcodeScanner barcodeScanner;
        ClaimedBarcodeScanner claimedBarcodeScanner;

        async public void Enable(string options)
        {
            PluginResult result;

            if (barcodeScanner == null)
            {
                barcodeScanner = await Windows.Devices.PointOfService.BarcodeScanner.GetDefaultAsync();

                if (claimedBarcodeScanner == null)
                {
                    claimedBarcodeScanner = await barcodeScanner.ClaimScannerAsync();

                    if (claimedBarcodeScanner != null)
                    {
                        await claimedBarcodeScanner.EnableAsync();
                        claimedBarcodeScanner.DataReceived += DataReceived;

                        result = new PluginResult(PluginResult.Status.NO_RESULT);
                        result.KeepCallback = true;
                    }
                    else
                    {
                        result = new PluginResult(PluginResult.Status.ERROR, "Barcode Scanner could not get claimed");
                    }
                }
                else
                {
                    result = new PluginResult(PluginResult.Status.ERROR, "Claimed Barcode Scanner Object already there");
                }
            }
            else
            {
                result = new PluginResult(PluginResult.Status.ERROR, "Barcode Scanner Object already there");
            }

            DispatchCommandResult(result);
        }

        async public void Reclaim(string options)
        {
            PluginResult result;

            if (barcodeScanner != null)
            {
                claimedBarcodeScanner = await barcodeScanner.ClaimScannerAsync();

                if (claimedBarcodeScanner != null)
                {
                    await claimedBarcodeScanner.EnableAsync();
                    claimedBarcodeScanner.DataReceived += DataReceived;

                    result = new PluginResult(PluginResult.Status.NO_RESULT);
                    result.KeepCallback = true;
                }
                else
                {
                    result = new PluginResult(PluginResult.Status.ERROR, "Barcode Scanner could not get claimed");
                }
            }
            else
            {
                result = new PluginResult(PluginResult.Status.ERROR, "Barcode Scanner Object not exists");
            }

            DispatchCommandResult(result);
        }

        void DataReceived(ClaimedBarcodeScanner sender, BarcodeScannerDataReceivedEventArgs args)
        {
            PluginResult result = new PluginResult(PluginResult.Status.OK, this.GetDataString(args.Report.ScanData));
            result.KeepCallback = true;
            DispatchCommandResult(result);
        }

        string GetDataString(IBuffer data)
        {
            String result = "";

            if (data == null)
            {
                result = "";
            }
            else
            {
                DataReader reader = DataReader.FromBuffer(data);
                byte[] dataBytes = new byte[data.Length];
                reader.ReadBytes(dataBytes);
                result = Encoding.UTF8.GetString(dataBytes, 0, dataBytes.Length);
            }

            return result.ToString();
        }
    }
}
