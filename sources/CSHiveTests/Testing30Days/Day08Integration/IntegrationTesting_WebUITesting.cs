namespace Tests.CS.Testing30Days.Day08Integration
{
    /*
    https://dotblogs.com.tw/hatelove/2012/12/10/learning-tdd-in-30-days-day8-integration-testing-and-web-ui-testing-by-selenium-and-webdriver


Integration Testing 定義

整合測試，針對的其實就是各物件之間的互動，或是模組運作是否正常。

整合測試所在的環境，應該是模擬的測試環境，基本上正式環境中該有服務、資源、資料庫等等，在測試環境也應有相對應的一份。（通常可能稱之為 alpha, beta 等等環境）。

如果單元測試的定義，是要獨立的測試目標物件上的行為，那麼整合測試就是不獨立的測試目標物件。在測試環境中，仿真地黑箱測試每一個物件的行為，並驗證是否如同預期。

若單元測試為白箱測試，則整合測試偏黑箱測試，針對流程、模組、物件每一個重要的入口點，進行 input/output 的驗證，以了解在實際環境中， production code 是否能如預期般正常運作。


    Integration Testing 進入點

筆者通常測試的進入點有三個：

Business Logic Layer 的 public 入口。
Data Access Layer 上， Dao 的開發。（可能為存取檔案或資料庫）
重要的 domain object 行為驗證。



    Integration Testing 注意事項

讀者需要注意一點，即使在測試環境進行整合測試，仍有可能有一些外部資源或服務，是無法模擬的，例如：每次查詢要花錢的服務，或是跟銀行交換資料之類的服務。

這時候，還是得針對這一類的相依服務，進行 stub/fake object 的設計來模擬。但絕大部分應該都不需要再透過 stub 等機制來模擬，因為要測試的就是各物件之間合作是否正常。

還有，整合測試的另一個重點：該如何重複執行而不會發生錯誤。

舉例來說，測試一個 Dao ，新增資料至DB中，如果測試案例不變，而資料表的PK也不是自動增加的，那麼執行第二次整合測試程式時，勢必就會出現主索引重複的錯誤。然而，測試程式基本上是不帶有邏輯的，所以也不建議寫一堆程式碼來避開這類的錯誤。

這類的問題，該如何解決呢？

簡單的建議是： snapshot ！

每一次開始進行整合測試時，都將環境還原為 snapshot 的情況後，才進行測試。測試案例執行完畢後，再還原一次。

這樣的動作，相當花費時間，所以當 developer 在抱怨自己的單元測試跑很久時，他的測試程式大概八九不離十是整合測試，而非單元測試。

單元測試強調獨立且在任何環境應該都能跑出一樣的結果，整合測試則強調在模擬環境下，各物件模組功能應如同預期。也因為相當花費時間，所以如果有 CI server 的話，建議這個執行整合測試的動作，可以交由 CI daily build 的時候，在 server 離峰時間，或不影響到大家作業時，在測試機上進行整合測試。

有關 CI 的資訊，請參考小弟去年的文章：[軟體工程]持續整合 (Continuous integration, CI) 簡介





    Web UI Testing – Selenium

基本上測試的粒度越大，測試花費的成本越小，但效益也越小，異動也越多。

廢話不多說，網頁的測試，首推免費的 Firefox Add-on :  Selenium。
推薦原因：免費、簡單、擴充性高。

免費：不管公司大小，誰都可以下載來玩。
簡單：連 PM、工讀生都可以輕鬆上手。
擴充性高：支援將錄製的腳本轉成多種語言，轉成語言後，即可透過 CI 或執行測試，啟動 Web Auto Testing 的腳本。
 

Selenium 安裝

請先到 Selenium 官網下載 Selenium IDE 。如下圖所示：

    
    */
}