using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstTests1;

public class LoginTests
{
    private IWebDriver driver;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
    }

    private void DangNhap(string tk = "standard_user", string mk = "secret_sauce")
    {
        driver.FindElement(By.Id("user-name")).SendKeys(tk);
        driver.FindElement(By.Id("password")).SendKeys(mk);
        driver.FindElement(By.Id("login-button")).Click();
    }

    [Test]
    public void DangNhap_ThanhCong()
    {
        DangNhap();

        Assert.That(driver.Url, Does.Contain("inventory.html"));
    }

    [Test]
    public void ThuThapDanhSachSanPham()
    {
        DangNhap();

        var items = driver.FindElements(By.ClassName("inventory_item"));

        foreach(var item in items)
        {
            var name = item.FindElement(By.ClassName("inventory_item_name")).Text;
            var price = item.FindElement(By.ClassName("inventory_item_price")).Text;
            Console.WriteLine($"{name} - {price}");
        }
        Assert.That(items.Count, Is.EqualTo(6));

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
            TestContext.AddTestAttachment(path);
        }

        driver.Quit(); driver.Dispose();
    }


}