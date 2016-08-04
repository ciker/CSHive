using System;
using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;


/*

[30天快速上手TDD][Day 7]Unit Test - Stub, Mock, Fake 簡介
https://dotblogs.com.tw/hatelove/2012/11/29/learning-tdd-in-30-days-day7-unit-testing-stub-mock-and-fake-object-introduction

*/

namespace Tests.CS.MockDemos
{
    public interface ICheckInFee
    {
        decimal GetFee(Customer customer);
    }

    public class Customer
    {
        public bool IsMale { get; set; }

        public int Seq { get; set; }
    }

    public class Pub
    {
        private ICheckInFee _checkInFee;
        private decimal _inCome = 0;

        public Pub(ICheckInFee checkInFee)
        {
            this._checkInFee = checkInFee;
        }

        /// <summary>
        /// 用到Fake的例子
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public int CheckInForFake(List<Customer> customers)
        {
            var result = 0;

            foreach (var customer in customers)
            {
                var isFemale = !customer.IsMale;
                //for fake
                var isLadyNight = DateTime.Today.DayOfWeek == DayOfWeek.Friday;
                //禮拜五女生免費入場
                if (isLadyNight && isFemale)
                {
                    continue;
                }
                else
                {
                    //for stub, validate status: income value
                    //for mock, validate only male
                    //this._inCome += this._checkInFee.GetFee(customer);

                    result++;
                }
            }

            //for stub, validate return value
            return result;
        }

        /// <summary>
        /// 入場
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>收費的人數</returns>
        public int CheckIn(List<Customer> customers)
        {
            var result = 0;

            foreach (var customer in customers)
            {
                var isFemale = !customer.IsMale;

                //女生免費入場
                if (isFemale)
                {
                    continue;
                }
                else
                {
                    //for stub, validate status: income value
                    //for mock, validate only male
                    this._inCome += this._checkInFee.GetFee(customer);

                    result++;
                }
            }

            //for stub, validate return value
            return result;
        }

        public decimal GetInCome()
        {
            return this._inCome;
        }
    }


    /// <summary>
    /// 透過這兩個測試案例，其實實際要測試的部分是，CheckIn 的方法只針對男生收費這一段邏輯。不管實際 production code，門票一人收費多少，都不會影響到這一份商業邏輯。
    /// <remarks>
    /// 怎麼根據環境或顧客來進行計價，那是在 production code 中，實作 ICheckInFee 介面的子類，要自己進行測試的，與 Pub 物件無關。這樣一來，才能隔絕 ICheckInFee 背後的變化。
    /// </remarks>
    /// </summary>
    [TestClass]
    public class PubTests
    {
        [TestMethod]
        public void Test_Charge_Customer_Count()
        {

            //1. 透過 MockRepository.GenerateStub<T>()，來建立某一個 T 型別的 stub object，以上面例子來說，是建立 ICheckInFee 介面的實作子類。
            //arrange
            ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
            Pub target = new Pub(stubCheckInFee);//2. 把該 stub object 透過建構式，設定給測試目標物件。

            //3. 定義當呼叫到該 stub object 的哪一個方法時，若傳入的參數為何，則 stub 要回傳什麼。
            stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);
            //透過 Rhino.Mocks，就這麼簡單地透過 Lambda 的方式定義 stub object 的行為，取代了原本要自己建立一個實體類別，並實作 ICheckInFee 介面，定義 GetFee 要回傳的值。

            var customers = new List<Customer>
            {
                new Customer{ IsMale=true},
                new Customer{ IsMale=false},
                new Customer{ IsMale=false},
            };

            decimal expected = 1;

            //act
            var actual = target.CheckIn(customers);

            //assert  
            Assert.AreEqual(expected, actual);


            //上面的測試案例，是入場顧客人數3人，一男兩女，因為目前 Pub 的 CheckIn 方法，只針對男生收費，所以回傳收費人數應為1人。
        }


        /// <summary>
        /// 第二個測試，則是驗證收費的總數，是否符合預期。測試案例一樣是一男兩女，而透過 stub object 模擬每一人收費為100元，所以預期結果門票收入總數為100。
        /// </summary>
        [TestMethod]
        public void Test_Income()
        {
            //arrange
            ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
            Pub target = new Pub(stubCheckInFee);

            stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

            var customers = new List<Customer>
            {
                new Customer{ IsMale=true},
                new Customer{ IsMale=false},
                new Customer{ IsMale=false},
            };

            var inComeBeforeCheckIn = target.GetInCome();
            Assert.AreEqual(0, inComeBeforeCheckIn);

            decimal expectedIncome = 100;

            //act
            var chargeCustomerCount = target.CheckIn(customers);

            var actualIncome = target.GetInCome();

            //assert
            Assert.AreEqual(expectedIncome, actualIncome);

            //可以看到這邊有兩個 Assert，因為我們這裡是驗證狀態的改變，期望在呼叫目標物件的 CheckIn 方法之前，取得的門票收入應為0。而呼叫之後，依照這個測試案例，門票收入應為100。
        }


        /// <summary>
        /// 我們想驗證的是：在2男1女的測試案例中，是否只呼叫 ICheckInFee 介面兩次。
        /// </summary>
        [TestMethod]
        public void Test_CheckIn_Charge_Only_Male()
        {
            //arrange mock
            var customers = new List<Customer>();

            //2男1女
            var customer1 = new Customer { IsMale = true };
            var customer2 = new Customer { IsMale = true };
            var customer3 = new Customer { IsMale = false };

            customers.Add(customer1);
            customers.Add(customer2);
            customers.Add(customer3);

            MockRepository mock = new MockRepository();
            ICheckInFee stubCheckInFee = mock.StrictMock<ICheckInFee>();

            using (mock.Record())
            {
                //期望呼叫ICheckInFee的GetFee()次數為2次
                stubCheckInFee.GetFee(customer1);

                LastCall
                    .IgnoreArguments()
                    .Return((decimal)100)
                    .Repeat.Times(2);
            }

            using (mock.Playback())
            {
                var target = new Pub(stubCheckInFee);

                var count = target.CheckIn(customers);
            }
        }

        /*
        Mock 使用時機：

        Mock 的驗證比起 stub 要複雜許多，變動性通常也會大一點，但往往在驗證一些 void 的行為會使用到，例如：在某個條件發生時，要記錄 Log。這種情境，用 stub 就很難驗證，因為對目標物件來說，沒有回傳值，也沒有狀態變化，就只能透過 mock object 來驗證，目標物件是否正確的與Log 介面進行互動。


        Fake 使用時機:
        當目標物件使用到靜態方法，或 .net framework 本身的物件，甚至於針對一般直接相依的物件，我們都可以透過 fake object 的方式，直接模擬相依物件的行為。
        
        以這例子來說，假設 CheckIn 的需求改變，從原本的「女生免費入場」變成「只有當天為星期五，女生才免費入場」，修改原CheckIn为CheckInForFacke

            碰到 DateTime.Today 這類東西，測試案例就會卡住。總不能每次測試都去改測試機上面的日期，或是只有星期五或非星期五才執行某些測試吧。
所以，我們得透過 Isolation framework 來輔助，針對使用到的組件，建立 fake object。
首先，因為這個例子建立的 fake object，是針對 System.DateTime，所以在測試專案上，針對 System.dll 來新增 Fake 組件
可以看到增加了一個 Fakes 的 folder，其中會針對要 fake 的 dll，產生對應的程式碼，以便我們進行攔截與改寫。

        */

        [TestMethod]
        public void Test_Friday_Charge_Customer_Count()
        {
            //1. 在 using (ShimsContext.Create()){} 的範圍中，會使用 Fake 組件。
            using (ShimsContext.Create())
            {
                //當在 fake context 環境下，呼叫到 System.DateTime.Today 時，會轉呼叫 System.Fakes.ShimDateTime.TodayGet，並定義其回傳值為「2012/10/19」，因為這一天是星期五。
                System.Fakes.ShimDateTime.TodayGet = () => new DateTime(2012, 10, 19);

                //arrange
                ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
                Pub target = new Pub(stubCheckInFee);

                stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

                var customers = new List<Customer>
                {
                    new Customer{ IsMale=true},
                    new Customer{ IsMale=false},
                    new Customer{ IsMale=false},
                };

                decimal expected = 1;

                //act
                var actual = target.CheckInForFake(customers);

                //assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Test_Saturday_Charge_Customer_Count()
        {

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.TodayGet = () =>
                {
                    //2012/10/20為Saturday
                    return new DateTime(2012, 10, 20);
                };

                //arrange
                ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
                Pub target = new Pub(stubCheckInFee);

                stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

                var customers = new List<Customer>
                {
                    new Customer{ IsMale=true},
                    new Customer{ IsMale=false},
                    new Customer{ IsMale=false},
                };

                decimal expected = 3;

                //act
                var actual = target.CheckInForFake(customers);

                //assert
                Assert.AreEqual(expected, actual);
            }
        }


        /*
        ps:
        連 System.dll 都可以進行 fake object 模擬了，所以即使是我們自訂的 class，直接相依，也可以透過這種方式來模擬。

這樣一來，即便是直接相依的物件，也可以進行獨立測試了。

但強烈建議，針對自訂物件的部分，這是黑魔法類型的作法，如果沒有包袱，建議物件設計還是要採 IoC 方式設計。如果是 legacy code，想要進行重構，擺脫直接相依的問題，則可先透過 fake object 來建立單元測試，接下來進行重構，重構後當物件不直接相依時，再改用上面的 stub/mock 方式來進行測試。

可以參考這篇在 Martin Fowler 網站上的文章：Modern Mocking Tools and Black Magic

        */


        /*
        
        結論

今天這篇文章介紹了 stub, mock 與 fake 的用法，但依筆者實際經驗，使用 stub 的比例大概是8～9成，使用mock的比例大概僅1～2成。而 fake 的方式，則用在特例，例如靜態方法跟 .net framework 原生組件。

也請讀者朋友務必記得幾個基本原則：

1. 同一測試案例中，請避免 stub 與 mock 在同一個案例一起驗證。原因就如同一直在強調的單元測試準則，一次只驗證一件事。而 stub 與 mock 的用途本就不同，stub 是用來輔助驗證回傳值或目標物件狀態，而 mock 是用來驗證目標物件與相依物件互動的情況是否符合預期。既然八竿子打不著，又怎麼會在同一個測試案例中，驗證這兩個完全不同的情況呢？
2. Mock 的驗證可以相當複雜，但越複雜代表維護成本越高，代表越容易因為需求異動而改變。所以，請謹慎使用 mock，更甚至於當發生問題時，針對問題的測試案例才增加 mock 的測試，筆者都認為是合情合理的。
3. 當要測試一個目標物件，要 stub/mock/fake 的 object 太多時，請務必思考目標物件的設計是否出現問題，是否與太多細節耦合，是否可將這些細節職責合併。
4. 當測試程式寫的一狗票落落長時，請確認目標物件的職責是否太肥，或是方法內容太長。這都是因為目標物件設計不良，導致測試程式不容易撰寫或維護的情況。問題根源在目標物件的設計品質。
5. 請將測試程式當作 production code 的一部份，production code 中不該出現的壞味道，一樣不該出現在測試程式中，尤其是重複的程式碼。所以測試程式，基本上也需要進行重構。但也請務必提醒自己，測試程式基本上不會包含邏輯，因為包含了邏輯，您就應該再寫一段測試程式，來測這個測試程式是否符合預期。
        
        */

    }
}