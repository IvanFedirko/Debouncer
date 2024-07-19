
using System.Diagnostics;
using System.Text;
using FIV.Debouncer;


namespace DebouncerTests;

[TestClass]
public class UnitTestDebouncer
{


    [TestMethod]
    public void TestHandlingBtnPressing()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        //Arrange
        int testVal = 0;
        ButtonDebouncer<int> debouncer = new ButtonDebouncer<int>((v) => testVal = v, TimeSpan.FromMilliseconds(100));

        //Act
        for (int i = 0; i < 10; i++)   { debouncer.HandlingBtnPressing(i); System.Threading.Thread.Sleep(1); }
        System.Threading.Thread.Sleep(1000); 

        //Assert
        Assert.AreEqual(9, testVal);

    }
}