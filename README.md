### ButtonDebouncer  v 1.0.0

Debouncing prevents function calls from executing too rapidly or too many times by button pressing.

In this repo, we have a ButtonDebouncer class that provides a method HandlingBtnPressing({VALUE}) to check if the button is pressed while applying a debounce logic. 


### How it works

First, the first value that arrives is executed, then over time the tracking window is moved to front to a specified interval. If the value has not changed, then nothing happens. If it has changed, the last one is returned.

### Generic version

Could use Int, bool etc.


```csharp
 if(_firstValue.Equals(_lastValue))
```


### Install

dotnet add package FIV.Debouncer --version 1.1.0


#### Usage Button Debouncer

```csharp

using FIV.Debouncer;
public class Program{
    public static void Main()
    {
       ButtonDebouncer<int> debouncer = new ButtonDebouncer<int>((v) => Console.WriteLine(v), TimeSpan.FromMilliseconds(1000) );
       //Logic to set different values to HandlingBtnPressing
       for(int i = 0; i< 100; i++)
       {
            debouncer.HandlingBtnPressing(i);
       }
       
    }
}
```


#### Usage Throttle Value

```csharp
using FIV.Debouncer;

public class Program{
    public static void Main()
    {
       ThrottleValue<int> throttle = new ThrottleValue<int>((v) => Console.WriteLine(v), TimeSpan.FromMilliseconds(1000) );
       //Logic to set different values to Handling
       for(int i = 0; i< 100; i++)
       {
            throttle.Handling(i);
       }
       
    }
}
```