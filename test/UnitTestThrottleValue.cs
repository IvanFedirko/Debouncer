
using System.Diagnostics;
using System.Text;
using FIV.Debouncer;


namespace DebouncerTests;

[TestClass]
public class UnitTestThrottleValue
{


    [TestMethod]
    public void TestHandling()
    {

        //Arrange
        int testValInt = 0;
        ThrottleValue<int> throttleInt = new ThrottleValue<int>((v) => testValInt = v, TimeSpan.FromMilliseconds(100));

        //Act
        for (int i = 0; i < 10; i++) { throttleInt.Handling(i); }
        System.Threading.Thread.Sleep(1000);

        //Assert
        Assert.AreEqual(9, testValInt);


        //Arrange
        bool testValBool = false;
        ThrottleValue<bool> throttleBool = new ThrottleValue<bool>((v) => testValBool = v, TimeSpan.FromMilliseconds(100));

        //Act
        throttleBool.Handling(true);
        throttleBool.Handling(false);

        //Assert
        Assert.AreEqual(true, testValBool);

        System.Threading.Thread.Sleep(45);
        //Assert
        Assert.AreEqual(true, testValBool);

        System.Threading.Thread.Sleep(100);

        //Assert
        Assert.AreEqual(false, testValBool);


        //Act
        throttleBool.Handling(true);
        //Assert
        Assert.AreEqual(true, testValBool);

        System.Threading.Thread.Sleep(1000);
        Assert.AreEqual(true, testValBool);

    }
}