# cordova-wpe8-barcode-scanner
Windows Phone 8.1 Embedded Handheld - Barcode Scanner implementation for Apache Cordova

## Installation

git clone https://github.com/grs-software/cordova-wpe8-barcode-scanner

## Usage

App init handler:

```
document.addEventListener('deviceready', function () {
	if (typeof BarcodeScanner !== 'undefined') {
		BarcodeScanner.Enable(function(data) { console.log(data); }, function(data) { console.log(data); });
	}
}
```

App resume handler:

```
document.addEventListener("resume", function() {
	if (typeof BarcodeScanner !== 'undefined') {
		BarcodeScanner.Reclaim(function(data) { console.log(data); }, function(data) { console.log(data); });
	}
}, false);
```