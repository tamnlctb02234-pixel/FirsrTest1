using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace FirstTests1;

public class Tests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
    }

    [Test]
    public void MoTrangChu_ThanhCong()
    {
        driver.Navigate().GoToUrl("https://fpt.edu.vn");
        Assert.That(driver.Title, Is.Not.Empty);
        Thread.Sleep(3000);

    }


    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}
