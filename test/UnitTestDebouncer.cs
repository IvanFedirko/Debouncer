
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

        //Arrange
        int testValInt = 0;
        ButtonDebouncer<int> debouncerInt = new ButtonDebouncer<int>((v) => testValInt = v, TimeSpan.FromMilliseconds(100));

        //Act
        for (int i = 0; i < 10; i++) { debouncerInt.HandlingBtnPressing(i);  }
        System.Threading.Thread.Sleep(1000);

        //Assert
        Assert.AreEqual(9, testValInt);


        //Arrange
        bool testValBool = false;
        ButtonDebouncer<bool> debouncerBool = new ButtonDebouncer<bool>((v) => testValBool = v, TimeSpan.FromMilliseconds(100));

        //Act
        debouncerBool.HandlingBtnPressing(true); 
        debouncerBool.HandlingBtnPressing(false); 
        System.Threading.Thread.Sleep(1000);

        //Assert
        Assert.AreEqual(false, testValBool);


        //Act
        debouncerBool.HandlingBtnPressing(true); 
        //Assert
        Assert.AreEqual(true, testValBool);

    }
}