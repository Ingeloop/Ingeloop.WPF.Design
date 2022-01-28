# Ingeloop.WPF.Design
Design Library for WPF (Controls, Colors, Styles)

## Quick guide:

### 1) Namespace:

Add the namespace as follow:

```xaml
xmlns:design="clr-namespace:Ingeloop.WPF.Design;assembly=Ingeloop.WPF.Design"
```

### 2) Resource:

Load the resource to your UI, as follow:

```xaml
<ResourceDictionary Source="pack://application:,,,/Ingeloop.WPF.Design;component/Resources/Design.xaml"/>
```

### 3) Theme colors:

You can override the default theme colors:

```xaml
<design:AppTheme PrimaryColor="SteelBlue" SecondaryColor="LightSteelBlue"></design:AppTheme>
```

### 4) Controls:

The default styles applied to controls are defined [here](https://github.com/Ingeloop/Ingeloop.WPF.Design/tree/master/Ingeloop.WPF.Design/Resources/Design.xaml).

To visualize the exhaustive list of all the Controls, launch the Demo Project.

## Demo:

[Demo Project](https://github.com/Ingeloop/Ingeloop.WPF.Design/tree/master/Ingeloop.WPF.Design.Demo)
