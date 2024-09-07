﻿using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using WebScraper.Drivers;

var webScraper = new WebScrapper();

var laptops = webScraper.GetData("https://webscraper.io/test-sites/e-commerce/static/computers/laptops");

webScraper.CreateExcelFile(laptops, "laptops");
