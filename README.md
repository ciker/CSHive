## CSHive ##

**CSHive** 是C#版的常用库，比如类型的快速转换，常用辅助工具类，性能调试 等

----------

### 开发约定
- .Net最低版本4.0
- IDE环境：VS2013 或 VS2015
- 采用Nuget进行统一的库管理，当前项目中尽量不引用任何非官方库
- 除非特殊情况，都采用UTF-8进行编码（如遇特殊情况，也必须提供UTF8的接口）
- Extension文件夹采用命字空间命名，方便扩展方法的使用。TODO：一些相关方法需要修正与重命名

----------

## log4net的引入 ##
**LogHelper** 是通过LogManager创建Log实例的辅助方法，在该类所有的程序集的**AssemblyInfo.cs**中加入如下两行内容（配置示例在[doc]/log4net.config）

```C#
//外置log4net配置
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
```

----------

## 常用的Nuget ##

### .Net ###
- PM> Install-Package log4net
- PM> Install-Package EntityFramework
- PM> Install-Package Newtonsoft.Json
- PM> Install-Package NUnit -Version 2.6.4



### jQuery ###
- PM> Install-Package jQuery
- PM> Install-Package jQuery.UI.Combined
- PM> Install-Package jQuery.Validation


----------


### 关于作者 ###

[艺风在线](http://max.cszi.com)

----------

##### 我要站在巨人们的肩膀上 #####