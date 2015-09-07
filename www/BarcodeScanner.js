var BarcodeScanner = {
    Enable: function (successCallback, errorCallback) {
        cordova.exec(successCallback, errorCallback, "BarcodeScanner", "Enable");
    },
    Reclaim: function (successCallback, errorCallback) {
        cordova.exec(successCallback, errorCallback, "BarcodeScanner", "Reclaim");
    }
}

module.exports = BarcodeScanner;
