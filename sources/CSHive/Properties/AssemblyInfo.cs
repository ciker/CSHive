using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("CSHive")]
[assembly: AssemblyDescription("CSWare开源类库蜂巢-常用扩展与相关辅助如类型的快速转换，辅助工具类，性能调试 等。")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ChaosStudio")]
[assembly: AssemblyProduct("CSHive")]
[assembly: AssemblyCopyright("Copyright © cszi.com 2016")]
[assembly: AssemblyTrademark("CSWare")]
[assembly: AssemblyCulture("")]

//将 ComVisible 设置为 false 将使此程序集中的类型
//对 COM 组件不可见。  如果需要从 COM 访问此程序集中的类型，
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

[assembly: InternalsVisibleTo("CSHiveTests")]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("f647b933-64ca-473f-945e-6ac0df657e22")]

// 程序集的版本信息由下列四个值组成: 
//
//      主版本 ： 具有相同名称但不同主版本号的程序集不可互换。
//      次版本 ： 如果两个程序集的名称和主版本号相同，而次版本号不同，这指示显著增强，但照顾到了向后兼容性。
//      修订号 ： 一般是Bug 的修复或是一些小的变动或是一些功能的扩充，要经常发布    修订版，修复一个严重 Bug 即可发布一个修订版
//      生成号 ： 每次生成时不同
//      附加说明：base、alpha、beta 、RC 、 release
//
//可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，
// 方法是按如下所示使用“*”: :
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("3.2.0.21")] //已取消CI脚本的版本号替换功能，该版本号将发布至NuGet上。Note:版本号不变化时将不会发布至Nuget上
//[assembly: AssemblyFileVersion("4.0.0.0")]


/*

v3.2.0.21
------------------------
. 增加一个将数N尽可能分配到数组M中的算法
. 增加一个辅助类，将一个普通Function执行异步调用

V3.2
-----------------------
. 在GitHub上使用Appveyor进行编译，生成，打包与发布（通过Tag号来触发发布）
. 增加自定义的KeyValueSectionHandler配置节，可以获取丰富的文本内容

V3.1
----2015-11-05--------------
. 加入Email发送功能
. EnumExt扩展加上Order属性，以便对枚举结果进行排序

V3.0

----2015-11-04--------------
. 增加报表导出
. 增加时间扩展，本月第一天，本月最后一天
. 增加DataContract的Json扩展


*/
