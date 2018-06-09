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

* Nethereum.Wallet generic interfaces, summary of balances, accounts, token registry, token transfer, Transaction history (based on transfers done in the wallet)
* Mock repository layer
* Start separation of concerns between Transactions and Reading.
* Validated usage for common methods, contract, eth.
* Generic UI, upgraded now to MVVMCross 6.0
* Nethereum working and tests on UWP (Windows 10, Windows 10 Phone, Xbox, Raspberry Pi 2), Android and iOS.

### Todo
* Ether Transfer example
* Load account from KeyStorage, Private key and HDWallet
* ViewModels to use ReactiveUI, Validation 
* Akavache
* Secured storage integration sample
* Connect to BlockchainStorage / Etherscan
* Improve UI / UX
* Mac, Linux, WPF, TV, Watch samples
* Continue testing all the platforms 
* Integrage with the DappHybrid
* IPFS to load from Dapp application using DappHybrid
* IPFS / DappHybrid Blazor

### Other UI samples

* Avalonia Desktop https://github.com/Nethereum/Nethereum.UI.Desktop
* WinForms Dekstop https://github.com/Nethereum/Nethereum.SimpleWindowsWallet
