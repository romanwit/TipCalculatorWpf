## **Tip Calculator**

This application is developed for splitting tips when you got non-separated bill for the whole company.

Each record in the model represents each guest with his or her amount of bill and tip. Total sum for each guest and for whole company will be summarized automatically.

The application uses pattern MVVM. 

1. Model is TipRecord class with two properties that can be set (Amount and Tip%%), and third property calculated (Total).
2. View is XAML (MainWindow.xaml) that displays content of these records (using two-way binding) and also there is a feature of adding a new record by using an instance of RelayCommand. 
3. ViewModel (MainViewModel.cs) is connecting Model and View. Records are stored in the property Records of ViewModel. It is adding new record when AddRecordCommand is being executed, and recalculates total sum for all guests.

View is using Materialize. It means MaterialDesignThemes nuget and adding two references to the App.xaml, in the ResourceDictionary section.

Validation of the user inputs (only numbers are allowed) is being executed in code-behind MainWindow.xaml.cs, that is called by PreviewTextInput events from MainWindow.xaml.

Also there are set of unit tests: for MainViewModel, for TipRecord and for MainWindow. So, we use three nugets additionally: NUnit, NUnit3TestAdapter and Microsoft.NET.Test.Sdk.  

MainWindowTests are most complicated, because app is being loaded here for the test. So it was necessary to use multi-threading here. It means that: 

1. Because tests are accessing visual tree, they need to wait until it will be loaded. So ManualResetEvent is waiting until Set() will be called in the _mainWindow_Loaded handler. 
2. All interactions with UI can be made only in the same STA thread. So this thread is being started at Setup of the tests, and each test uses Dispatcher to access exactly same thread in async/await manner.
3. Initialization of GUI for tests ignores usual project settings, so references for Materialize are added manually.
