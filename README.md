# Nethereum.UI.Wallet.Sample

This is a cross platform wallet example using Nethereum, Xamarin.Forms and MvvmCross, targetting all main mobile platforms Android, iOS, Windows, Dektop (windows 10 uwp), IoT with the Raspberry PI and Xbox. 

Work in progress.

## Screenshots from Morden

<img src="screenshots/wp10Accounts.png" width="200" alt="Nethereum UWP Windows 10 Phone Ethereum example">
<img src="screenshots/AndroidBalance.png" width="200" alt="Nethereum Android Phone Ethereum example">
<img src="screenshots/AndroidHamburger.png" width="200" alt="Nethereum Android Phone Ethereum example">
<img src="screenshots/w10Balance.png" width="400" alt="Nethereum UWP Windows 10 Desktop Ethereum example">

Demo on the xbox: https://www.youtube.com/embed/WuRFmlcWFaA

Demo on the Raspberry PI: https://www.youtube.com/watch?v=bGZhq9oW3Mo (The data is cached on the demo, it is very slow retrieving the data at the moment)

### Done

* Nethereum.Wallet generic interfaces, summary of balances, accounts, token registry.
* Validated usage for common methods, contract, eth.
* Generic MvxFormsShellPagePresenter for hambuger, slider navigation.
* Generic UI
* Nethereum working and tests on UWP (Windows 10, Windows 10 Phone, Xbox, Raspberry Pi 2), Android and iOS.
 Note: Xbox mouse mode is not implemented yet for uwp in the current xbox dev preview release.
Note: Pi is rather slow connecting to geth

### Todo
* Transfer example.
* Registry of assets, sql lite storage
* Screen configuration
* Transactions storage and watch for registered tokens / eth.
* Continue testing all the platforms 
