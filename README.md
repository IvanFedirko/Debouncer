### ButtonDebouncer  v 1.0.1

Debouncing prevents function calls from executing too rapidly or too many times by button pressing.

In this repo, we have a ButtonDebouncer class that provides a method HandlingBtnPressing({VALUE}) to check if the button is pressed while applying a debounce logic. 


### How it works

First, the first value that arrives is executed, then over time the tracking window is moved to front to a specified interval. If the value has not changed, then nothing happens. If it has changed, the last one is returned.



### Install

dotnet add package FIV.Debouncer --version 0.0.1


#### Usage
```
using FIV.Debouncer;

{
    public void Main()
    {
       ButtonDebouncer debouncer = new ButtonDebouncer((v) => Console.WriteLine(v), TimeSpan.FromMilliseconds(1000) );
       
       //Logic to set different values to HandlingBtnPressing
       debouncer.HandlingBtnPressing(BtnValue);
    }
}
```