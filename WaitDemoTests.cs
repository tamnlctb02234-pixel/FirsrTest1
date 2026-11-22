using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;

namespace FirstTests1;

public class WaitDemoTests
{
    private IWebDriver driver;
    private WebDriverWait wait;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

    }

    [Test]
    public void CoWait_SePass()
    {
        driver.FindElement(By.CssSelector("#start > button")).Click();
        var el = wait.Until(d => d.FindElement(By.Id("finish")));

        Assert.That(el.Text, Does.Contain("Hello World!"));
    }

    [TearDown]
    public void TearDown()
    {

        var ctx = TestContext.CurrentContext;
        if(ctx.Result.Outcome.Status == TestStatus.Failed)
        {
            var anh = ((ITakesScreenshot)driver).GetScreenshot();
            var path = Path.Combine(ctx.WorkDirectory, $"{ctx.Test.Name}.png");
            anh.SaveAsFile(path);
            TestContext.AddTestAttachment(path); //Đính vào báo cáo
        }

        driver.Quit(); driver.Dispose();
    }

}