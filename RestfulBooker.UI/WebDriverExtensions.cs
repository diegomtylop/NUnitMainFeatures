using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RestfulBooker.UI
{
	public static class WebDriverExtensions
	{

		//Extention method to mark a checkbox based on a boolean value
		public static void SetCheckBoxDiego( this IWebElement element, bool value)
		{
			if( element.Selected != value)
			{
				element.Click();
			}
		}
	}
}

