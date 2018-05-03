[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT) ![Built With C#](https://img.shields.io/badge/Built_with-C%23-green.svg)

# ChilliSource.Mobile.UI #

This project is part of the ChilliSource framework developed by [BlueChilli](https://github.com/BlueChilli).

## Summary ##

```ChilliSource.Mobile.UI``` is a collection of reusable UI components, extensions, and abstractions that are shared across all .NET based ChilliSource frameworks. 

## ChilliSource.Mobile.UI Usage ##

### Controls ###

The main feature of ```ChilliSource.Mobile.UI``` is the collection of reusable controls it provides, divided into extensions of existing Xamarin Forms controls, and custom controls.

**Extended Xamarin Forms Controls**

| Control | Customizations |
|---|---|
| ExtendedButton | font, pressed and disabled states, content and image alignments, long press |
| ExtendedEditor | font, dynamic sizing, border, placeholder, max length, toolbar |
| ExtendedEntry | font, placeholder and error styles, background color, border, tool bar, max length, indentation and padding, keyboard theme and return type |
| ExtendedLabel | font, style text parts, number of lines, font size adjustment,  |
| ExtendedListView | selection allowed, corner radius  |
| ExtendedSearchBar | tint, search style, bar style |
| ExtendedSwitch | tint color |
| ExtendedWebView | is transparent |

**Custom Controls**

| Control | Features |
|---|---|
| AdvancedActionSheet | fully customizable popup control that displays an iOS like action sheet |
| AdvancedAlertView | fully customizable popup control that displays an iOS like alert view |
| CarouselView | carousel control allowing linear scolling of child views |
| CircledImage | renders an image in the shape of a circle |
| BannerView | allows the display of Android-style toast banners |
| DateTimePickerView | iOS style date and time picker view |
| PickerView | iOS-style generic picker view |
| Repeater | Extends ```StackLayout``` to provide item source and template binding for child elements |
| SegmentedControl | custom iOS-style segmented control |
| Separator | renders a dashed, dotted, or solid line |
| ValidationErrorsView | ```RepeaterView``` that lists validation errors |
| ImageButton | button control with both text and image |
| ImageButtonView | creates an image that acts like a button |
| ResponseParsingWebView | ```WebView``` that can be used to retrieve a specific Html element from the response |
| ShapeView | control that renders as a circle, rectangle, or triangle |


### Animations ###

The framework provides bahaviors for the following animation types: bounce, fade, quick-entrance, rotation, scale, shake, and vibrate. E.g.

```csharp
var image = new Image();
var animation = new BounceAnimation { Duration = 800, NumberOfBounces = 3 };
image.Behaviors.Add(animation);
animation.PerformAnimation();
...
animation.CancelAnimation();
```

### Effects ###

The framework provides the following effects: border, entry line, button long press, image blur, and rounded corner, and shadow.

```csharp
var image = new Image();
image.Effects.Add(new ImageBlurEffect { Radius = 5 } );
```
### Services ###

**Device Service**

Use to retrieve information about the device but also the platform and the currently executing app, e.g. device name, device, version, screen size, free space, app version, app installation ID, operating system name, operating system version, time zone. etc.
```csharp
var deviceService = DependencyService.Get<IDeviceService>();
Console.WriteLine(deviceService.GetAppVersion());
```

**Alert Service**

Use to display alerts, prompts, action sheets, and sharing sheets by leveraging native functionality. 
```csharp
var alertService = DependencyService.Get<IAlertService>();
alertService.DisplayAlert("My App", "Welcome!");
```

**Clipboard Service**

Use to copy text to the clipboard.
```csharp
var clipboardService = DependencyService.Get<IClipboardService>();
clipboardService.CopyToClipboard(text);
```

**Keyboard Service**

Use to determine when the keyboard is about to be shown or hidden:
```csharp
var keyboardService = DependencyService.Get<IKeyboardService>();
keyboardService.KeyboardVisibilityChanged += (sender, args) =>
{
    if (args.IsVisible)
    {
        //adjust scroll view content position
        var contentInsets = new UIEdgeInsets(0.0f, 0.0f, args.KeyboardHeight, 0.0f);
		scrollView.ContentInset = contentInsets;
		scrollView.ScrollIndicatorInsets = contentInsets;
    }
    else
    {
        scrollView.ContentInset = UIEdgeInsets.Zero;
		scrollView.ScrollIndicatorInsets = UIEdgeInsets.Zero;
    }
};
```

**Local Notification Service**

Use to schedule local notifications:
```csharp
var localNotificationService = DependencyService.Get<ILocalNotificationService>();
localNotificationService.RegisterForNotifications(showAlert: true, showBadge: true, playSound: true);

localNotificationService.ScheduleNotification(uniqueNotificationId, title, body, scheduledTime, tag);
```

### Validations ###

Validations help to simplify the process of verifying user inputs. For example, to validate the user's
email address follow these steps:

Create a ```ValidatableObject``` and add validations:
```csharp
var email = new ValidatableObject<string>();
email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email should not be empty" });
email.Validations.Add(new EmailRule<string> { ValidationMessage = "Invalid Email" });
```

Bind an entry to ```Email.Value```:
```csharp
<Entry Text="{Binding Email.Value, Mode=TwoWay}"/>

public ValidatableObject<string> Email => email;
```

Then perform the validation and retrieve the validation message:

```csharp
if (!email.Validate())
{
    Console.WriteLine(email.ValidationMessage);
} 
```


### Navigation ###

To use ViewModel-first navigation follow these steps:

First create a mapping between your pages and their corresponding view models:
```csharp
NavigationLocator.Register<HomePageViewModel, HomePage>();
```

Set the root page:
```csharp
NavigationService.SetRoot(new LoginPageViewModel(), wrapInNavigation: false);
```

Or for Master/Detail pages:
```csharp
Master = NavigationService.BuildPage(new MenuPageViewModel(), wrapInNavigation:false);
Detail = NavigationService.BuildPage(new HomePageViewModel(), wrapInNavigation:true);
```

Then perform the navigation from anywhere in your code, including your view models:
```csharp
await NavigationService.PushAsync(new HomePageViewModel());
```

## ChilliSource.Mobile.UI.ReactiveUI Usage ##

### Summary ###

```ChilliSource.Mobile.UI.ReactiveUI``` is a collection of Reactive version of ChilliSource.Mobile.UI components, extensions, helper classes for [ReactiveUI](https://github.com/reactiveui/ReactiveUI). . 

### ViewModel Navigation ###

for host screen viewmodel implement IHostScreen

```csharp

public class MainViewModel : IHostScreen
{
    private NavigationState _router;

    public HostViewModel() {
        _router = new NavigationState();
        _router.NavigationStack.Add(new FirstPageViewModel());
    }

    public NavigationState Router => _router;
};


var mainPageViewModel = new MainViewModel();
App.MainPage = new ReactiveRoutedViewHost(mainPageViewModel.Router);
```


for navigatable view model implement INavigatableViewModel

```csharp
public class FirstPageViewModel : INavigatableViewModel
{
    private IHostScreen _screen;

    public FirstPageViewModel(IHostScreen screen) {
        _screen = scree;
    }

    public async Task OpenModal() 
    {
        await _screen.PushModal.Execute(new ModalPageViewModel());
    }

    public string UrlPathSegment => "First Page";
    public IHostScreen HostScreen => _screen;
};
```

for modal view implement IModalViewModel

```csharp
public class ModalPageViewModel : IModalViewModel, IHostScreen
{
   private NavigationState _router;
 
    public ModalPageViewModel() {
        _router = new NavigationState();
    }

    public Color? NavBarColor => Color.Red;
    public Color? NavBarTextColor => Color.Green;

    public string UrlPathSegment => "Modal Page";
    public bool Animated => true;
    public bool WithNavigationBar => true;
    public NavigationState Router => _router;

    public IHostScreen HostScreen => this;

    public async Task CloseModal() {
       await _router.PopModal();
    }
};
```

To Push the modal
```csharp
var firstPage = new FirstPageViewModel();
await firstPage.OpenModal();
```

To Pop the modal
```csharp
var modalPage = new ModalPageViewModel();
await modalPage.PopModal();
```

See the example app for more detail example coming soon!


### Validations ###

Validations help to simplify the process of verifying user inputs. For example, to validate the user's
email address follow these steps:

Create a ```ReactiveValidatableObject``` and add validations:
```csharp
var email = new ReactiveValidatableObject<string>();
email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email should not be empty" });
email.Validations.Add(new EmailRule<string> { ValidationMessage = "Invalid Email" });
```

Bind an entry to ```email.Value```:
```csharp

this.Bind(ViewModel, this.Email.Value, this.Entry.Text)

public ReactiveValidatableObject<string> Email => email;
```

### Reactive Collections ###

```ChilliSource.Mobile.UI.ReactiveUI``` uses [DynamicData](https://github.com/RolandPheasant/DynamicData) as reactive collections

You can use ObservableRangeCollection as ObservableCollection as backing collection when you want to bind the collection to ListView.

```csharp
var _sourceList = new SourceList<TestViewModel>();
var items = new ObservableRangeCollectionExtended<TestViewModel>();

_sourceList.Connect()
    .Sort(SortExpressionComparer<TestViewModel>.Descending(t => t.SentDate))
    .ObserveOn(RxApp.MainThreadScheduler)
    .Bind(_items)
    .Subscribe()
    .DisposeWith(Disposables);
```

then bind the **observable collection** to your listview

```csharp
this.WhenActivated(d =>
{
    this.OneWayBind(ViewModel, v => v.ItemSource, c => c.AlertList.ItemsSource).DisposeWith(d);
});
```

see [DynamicData](https://github.com/RolandPheasant/DynamicData) for more details

## Installation ##

The libraries are available via NuGet:
* [ChilliSource.Mobile.UI](https://www.nuget.org/packages/ChilliSource.Mobile.UI/)
* [ChilliSource.Mobile.UI.ReactiveUI](https://www.nuget.org/packages/ChilliSource.Mobile.UI.ReactiveUI/)

## Releases ##

See the [releases](https://github.com/BlueChilli/ChilliSource.Mobile.UI/releases).

## Contribution ##

Please see the [Contribution Guide](.github/CONTRIBUTING.md).

## License ##

ChilliSource.Mobile is licensed under the [MIT license](LICENSE).

## Feedback and Contact ##

For questions or feedback, please contact [chillisource@bluechilli.com](mailto:chillisource@bluechilli.com).


